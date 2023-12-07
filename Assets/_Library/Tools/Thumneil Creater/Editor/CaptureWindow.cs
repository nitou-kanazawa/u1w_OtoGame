#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

// [�Q�l]
//  qitta: 3D�I�u�W�F�N�g�̃T���l�C���摜���ꊇ�ō��Editor�g�� https://qiita.com/kuuki_yomenaio/items/16577f32792da9eac17b
//  qitta: UnityEditor���StartContinue���ۂ��̂𓮂��� https://qiita.com/k_yanase/items/686fc3134c363ffc5239
//  �˂������V�e�B: �X�N���[���V���b�g��ۑ����� https://nekojara.city/unity-screenshot
//  �R�K�l�u���O: �Q�[����ʂ�w�i���߂ŃX�N���[���V���b�g�ł���֐� https://baba-s.hatenablog.com/entry/2023/04/25/170848

namespace nitou.Tools {

    public class CaptureWindow : OdinEditorWindow {

        // Group��`
        private const string DIRECTORY_INFO_GROUPE = "Directory Info";
        private const string MODEL_SETTINGS_GROUPE = "Model Settings";
        private const string PICTURE_SETTINGS_GROUPE = "Picture Settings";


        /// ----------------------------------------------------------------------------
        // Directory Settings

        [Title(DIRECTORY_INFO_GROUPE)]
        [InfoBox("Search Directory�ȉ��̑SPrefab���L���v�`�����܂�.")]

        // �Ώۃf�B���N�g���i�������ȉ��̑Sprefab���L���v�`������j
        [Indent]
        [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
        [SerializeField] private Object _searchDirectory;

        // �o�̓f�B���N�g��
        [Indent]
        [SerializeField] private string _exportPath = "Assets/Thumneil Creater/Captures"; // �o�͐�f�B���N�g��(Assets/Captures/�ȉ��ɏo�͂���܂�)     


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
        // ���X�y�[�X
        [OnInspectorGUI] private void Space4() { GUILayout.Space(20); }


        // ���������p
        private List<GameObject> _objList = new();     // ����������prefab���X�g
        private EditorCoroutine.Coroutine _coroutine;   // �L���v�`�����s�R���[�`��


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        /// <summary>
        /// �E�C���h�E���J��
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

            // �o�͐�f�B���N�g���𐶐�
            if (!File.Exists(_exportPath)) {
                Directory.CreateDirectory(_exportPath);
            }

            _objList.Clear();

            // �w��f�B���N�g������prefab��S�Ď��o����List�ɓ����
            string replaceDirectoryPath = AssetDatabase.GetAssetPath(_searchDirectory);
            string[] filePaths = Directory.GetFiles(replaceDirectoryPath, "*.*");
            foreach (string filePath in filePaths) {
                GameObject obj = AssetDatabase.LoadAssetAtPath(filePath, typeof(GameObject)) as GameObject;
                if (obj != null) {
                    _objList.Add(obj);
                }
            }

            // ���s
            _coroutine = EditorCoroutine.Start(ProsessPrefabListCoroutine(_objList));
        }

        /// <summary>
        /// �Ώ�Prefab�����ɏ�������R���[�`�� 
        /// </summary>
        private IEnumerator ProsessPrefabListCoroutine(List<GameObject> objList) {
            foreach (GameObject obj in objList) {

                // Instantiate���Č����𒲐����Ď��₷���ʒu��
                GameObject unit = Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
                unit.transform.position = _position;
                unit.transform.eulerAngles = _eulerAngle;

                yield return new EditorCoroutine.WaitForSeconds(0.5f);

                // 
                CaptureScreenShot(Camera.main, obj.name);

                // �L���v�`��������͍폜
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

            // �����߉摜�ɂ������ꍇ�͈ȉ��̃J�����ݒ���s��


            // RenderTexture
            var renderTexture = new RenderTexture(_width, _height, 24);
            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;

            // Texture2D
            var texture2D = new Texture2D(_width, _height, TextureFormat.ARGB32, false);
            texture2D.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
            texture2D.Apply();

            // texture��byte���t�@�C���ɏo��
            byte[] bytes = texture2D.EncodeToPNG();
            File.WriteAllBytes($"{_exportPath}/{fileName}.png", bytes);

            // �㏈��
            camera.targetTexture = null;
            RenderTexture.active = null;
            DestroyImmediate(texture2D);
            renderTexture.Release();
        }


    }


}
#endif