using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// [�Q�l]
//  Docswell: AssetPostprocessor���S�ɗ������� https://www.docswell.com/s/henjiganai/5714J5-AssetPostprocessor#p8
//  qiita: �G�f�B�^�[�g���ŁA�ǂݍ��ރA�Z�b�g�̃p�X���n�[�h�R�[�h���Ȃ����߂� https://qiita.com/tsukimi_neko/items/3d57e3808acb88e11c39
//  �@�� �i��AssetPostprocessor��Unity.Object�H��e�Ɏ����Ȃ����߁C�V���A���C�Y�ΏۊO�݂����j

namespace nitou.Tools {

    /// <summary>
    /// �t�H���_�A�C�R���摜���Ǘ�����Dictionay�𐶐�����
    /// </summary>
    public class IconDictionaryCreator : AssetPostprocessor {

        // ���\�[�X���
        private const string AssetsPath = "_Library/Tools/Project Folder Icon/Icons";
        internal static Dictionary<string, Texture> IconDictionary;


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �A�Z�b�g���C���|�[�g������̏���
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
        /// �t�@�C�����̌���
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
        /// Dictionary�̐���
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
        /// �w�肵���L�[�ɑΉ�����A�C�R���摜���擾����
        /// </summary>
        public static (bool isExist, Texture texture) GetIconTexture(string fileNameKey) {

            // �t�@�C���������S��v�̏ꍇ
            if (IconDictionary.ContainsKey(fileNameKey)) {
                return (true, IconDictionary[fileNameKey]);
            }

            // ���K�\���Ή� (���Ƃ肠�������ߑł��̎���)
            if (fileNameKey[0] == '_' && fileNameKey.Length > 1) {
                fileNameKey = fileNameKey.Substring(1);
                if (IconDictionary.ContainsKey(fileNameKey))
                    return (true, IconDictionary[fileNameKey]);
            }

            return (false, null);
        }
    }
}
