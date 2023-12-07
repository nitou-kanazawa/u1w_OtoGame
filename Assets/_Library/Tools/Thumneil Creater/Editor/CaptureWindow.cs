#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

// [参考]
//  qitta: 3Dオブジェクトのサムネイル画像を一括で作るEditor拡張 https://qiita.com/kuuki_yomenaio/items/16577f32792da9eac17b
//  qitta: UnityEditor上でStartContinueっぽいのを動かす https://qiita.com/k_yanase/items/686fc3134c363ffc5239
//  ねこじゃらシティ: スクリーンショットを保存する https://nekojara.city/unity-screenshot
//  コガネブログ: ゲーム画面を背景透過でスクリーンショットできる関数 https://baba-s.hatenablog.com/entry/2023/04/25/170848

namespace nitou.Tools {

    public class CaptureWindow : OdinEditorWindow {

        // Group定義
        private const string DIRECTORY_INFO_GROUPE = "Directory Info";
        private const string MODEL_SETTINGS_GROUPE = "Model Settings";
        private const string PICTURE_SETTINGS_GROUPE = "Picture Settings";


        /// ----------------------------------------------------------------------------
        // Directory Settings

        [Title(DIRECTORY_INFO_GROUPE)]
        [InfoBox("Search Directory以下の全Prefabをキャプチャします.")]

        // 対象ディレクトリ（※ここ以下の全prefabをキャプチャする）
        [Indent]
        [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
        [SerializeField] private Object _searchDirectory;

        // 出力ディレクトリ
        [Indent]
        [SerializeField] private string _exportPath = "Assets/Thumneil Creater/Captures"; // 出力先ディレクトリ(Assets/Captures/以下に出力されます)     


        /// ----------------------------------------------------------------------------
        // Model Settings

        [Title(MODEL_SETTINGS_GROUPE)]


        [Indent]
        [SerializeField] private Vector3 _position;

        [Indent]
        [SerializeField] private Vector3 _eulerAngle;



        /// ----------------------------------------------------------------------------
        // Capture Settings

        [Title(PICTURE_SETTINGS_GROUPE)]

        [Indent]
        [Min(10)]
        [SerializeField] private int _width = 200;

        [Indent]
        [Min(10)]
        [SerializeField] private int _height = 200;


        /// ----------------------------------------------------------------------------
        // ※スペース
        [OnInspectorGUI] private void Space4() { GUILayout.Space(20); }


        // 内部処理用
        private List<GameObject> _objList = new();     // ※処理中のprefabリスト
        private EditorCoroutine.Coroutine _coroutine;   // キャプチャ実行コルーチン


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        /// <summary>
        /// ウインドウを開く
        /// </summary>
        [MenuItem("Tools/Nitou/Capture/CaptureCreater")]
        static void Open() => EditorWindow.GetWindow(typeof(CaptureWindow));


        /// ----------------------------------------------------------------------------
        // Editor Method

        /// <summary>
        /// 
        /// </summary>
        [DisableIf("@this._searchDirectory == null")]
        [Button("Capture"), GUIColor(.2f, 1f, .2f)]
        private void OnCaputerButtonClicked() {
            if (_coroutine != null && _coroutine.isActive) return;
            if (_searchDirectory == null) return;

            // 出力先ディレクトリを生成
            if (!File.Exists(_exportPath)) {
                Directory.CreateDirectory(_exportPath);
            }

            _objList.Clear();

            // 指定ディレクトリ内のprefabを全て取り出してListに入れる
            string replaceDirectoryPath = AssetDatabase.GetAssetPath(_searchDirectory);
            string[] filePaths = Directory.GetFiles(replaceDirectoryPath, "*.*");
            foreach (string filePath in filePaths) {
                GameObject obj = AssetDatabase.LoadAssetAtPath(filePath, typeof(GameObject)) as GameObject;
                if (obj != null) {
                    _objList.Add(obj);
                }
            }

            // 実行
            _coroutine = EditorCoroutine.Start(ProsessPrefabListCoroutine(_objList));
        }

        /// <summary>
        /// 対象Prefabを順に処理するコルーチン 
        /// </summary>
        private IEnumerator ProsessPrefabListCoroutine(List<GameObject> objList) {
            foreach (GameObject obj in objList) {

                // Instantiateして向きを調整して取りやすい位置に
                GameObject unit = Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
                unit.transform.position = _position;
                unit.transform.eulerAngles = _eulerAngle;

                yield return new EditorCoroutine.WaitForSeconds(0.5f);

                // 
                CaptureScreenShot(Camera.main, obj.name);

                // キャプチャした後は削除
                DestroyImmediate(unit);
            }

            // 
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }


        /// <summary>
        /// 
        /// </summary>
        private void CaptureScreenShot(Camera camera, string fileName) {

            // ※透過画像にしたい場合は以下のカメラ設定を行う


            // RenderTexture
            var renderTexture = new RenderTexture(_width, _height, 24);
            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;

            // Texture2D
            var texture2D = new Texture2D(_width, _height, TextureFormat.ARGB32, false);
            texture2D.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
            texture2D.Apply();

            // textureのbyteをファイルに出力
            byte[] bytes = texture2D.EncodeToPNG();
            File.WriteAllBytes($"{_exportPath}/{fileName}.png", bytes);

            // 後処理
            camera.targetTexture = null;
            RenderTexture.active = null;
            DestroyImmediate(texture2D);
            renderTexture.Release();
        }


    }


}
#endif