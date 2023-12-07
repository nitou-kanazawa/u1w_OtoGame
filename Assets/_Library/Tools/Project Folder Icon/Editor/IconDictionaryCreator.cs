using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// [参考]
//  Docswell: AssetPostprocessor完全に理解した https://www.docswell.com/s/henjiganai/5714J5-AssetPostprocessor#p8
//  qiita: エディター拡張で、読み込むアセットのパスをハードコードしないために https://qiita.com/tsukimi_neko/items/3d57e3808acb88e11c39
//  　→ （※AssetPostprocessorはUnity.Object？を親に持たないため，シリアライズ対象外みたい）

namespace nitou.Tools {

    /// <summary>
    /// フォルダアイコン画像を管理するDictionayを生成する
    /// </summary>
    public class IconDictionaryCreator : AssetPostprocessor {

        // リソース情報
        private const string AssetsPath = "_Library/Tools/Project Folder Icon/Icons";
        internal static Dictionary<string, Texture> IconDictionary;


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// アセットをインポートした後の処理
        /// </summary>
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
            if (!ContainsIconAsset(importedAssets) &&
                !ContainsIconAsset(deletedAssets) &&
                !ContainsIconAsset(movedAssets) &&
                !ContainsIconAsset(movedFromAssetPaths)) {
                return;
            }

            BuildDictionary();
        }

        /// <summary>
        /// ファイル名の検証
        /// </summary>
        private static bool ContainsIconAsset(string[] assets) {
            foreach (string str in assets) {
                if (ReplaceSeparatorChar(Path.GetDirectoryName(str)) == "Assets/" + AssetsPath) {
                    return true;
                }
            }
            return false;
        }

        private static string ReplaceSeparatorChar(string path) {
            return path.Replace("\\", "/");
        }

        /// <summary>
        /// Dictionaryの生成
        /// </summary>
        internal static void BuildDictionary() {
            var dictionary = new Dictionary<string, Texture>();

            var dir = new DirectoryInfo(Application.dataPath + "/" + AssetsPath);
            FileInfo[] info = dir.GetFiles("*.png");
            foreach (FileInfo f in info) {
                var texture = (Texture)AssetDatabase.LoadAssetAtPath($"Assets/{AssetsPath}/{f.Name}", typeof(Texture2D));
                dictionary.Add(Path.GetFileNameWithoutExtension(f.Name), texture);
            }

            IconDictionary = dictionary;
        }

        /// <summary>
        /// 指定したキーに対応するアイコン画像を取得する
        /// </summary>
        public static (bool isExist, Texture texture) GetIconTexture(string fileNameKey) {

            // ファイル名が完全一致の場合
            if (IconDictionary.ContainsKey(fileNameKey)) {
                return (true, IconDictionary[fileNameKey]);
            }

            // 正規表現対応 (※とりあえず決め打ちの実装)
            if (fileNameKey[0] == '_' && fileNameKey.Length > 1) {
                fileNameKey = fileNameKey.Substring(1);
                if (IconDictionary.ContainsKey(fileNameKey))
                    return (true, IconDictionary[fileNameKey]);
            }

            return (false, null);
        }
    }
}
