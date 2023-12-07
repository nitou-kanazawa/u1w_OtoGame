#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

// [�Q�l]
//  �V�[�����ȒP�ɐ؂�ւ���G�f�B�^�g�� https://tyfkda.github.io/blog/2021/07/15/unity-scene-switcher.html
//  �V�[����؂�ւ���{�^����\������G�f�B�^�g�� https://kyoro-s.com/unity-13/
//  Unity���f�[�^��ۑ����邽�߂Ɏg���p�X�ɂ��� https://light11.hatenadiary.com/entry/2019/10/07/031405

namespace nitou.Tools {

    /// <summary>
    /// �ҏW���̃V�[���؂�ւ���e�Ղɂ��邽�߂̃E�C���h�E
    /// </summary>
    public class SceneSwitcherWindow : EditorWindow {

        // �E�C���h�E�`��p
        private Vector2 scrollPos = Vector2.zero;
        
        // 
        private List<SceneAsset> scenes;

        // �f�[�^�ۑ���
        private static string FilePath => $"{Application.persistentDataPath}/_sceneLauncher.sav";


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        [MenuItem("Window/Nitou/Scene/Scene Switcher")]
        static void Open() => GetWindow<SceneSwitcherWindow>("Scene Switcher");

        private void OnEnable() {
            if(scenes == null) {
                scenes = new List<SceneAsset>();
                Load();
            }
        }

        private void OnGUI() {

            // �V�[���ǉ���
            EditorGUILayout.BeginHorizontal();
            var sceneAsset = EditorGUILayout.ObjectField(null, typeof(SceneAsset), false) as SceneAsset;
            if (sceneAsset != null && scenes.IndexOf(sceneAsset) < 0) {
                scenes.Add(sceneAsset);
                Save();
            }

            if (GUILayout.Button("Add current scene")) {
                var scene = EditorSceneManager.GetActiveScene();
                if (scene != null && scene.path != null &&
                    scenes.Find(s => AssetDatabase.GetAssetPath(s) == scene.path) == null) {
                    var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                    if (asset != null && scenes.IndexOf(asset) < 0) {
                        scenes.Add(asset);
                        Save();
                    }
                }
            }
            EditorGUILayout.EndHorizontal();


            GuiLine();

            this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos);
            for (var i = 0; i < scenes.Count; ++i) {
                var scene = scenes[i];
                EditorGUILayout.BeginHorizontal();
                var path = AssetDatabase.GetAssetPath(scene);
                if (GUILayout.Button("X", GUILayout.Width(20))) {
                    scenes.Remove(scene);
                    Save();
                    --i;
                } else {
                    if (GUILayout.Button("O", GUILayout.Width(20))) {
                        EditorGUIUtility.PingObject(scene);
                    }
                    if (GUILayout.Button(i > 0 ? "��" : "�@", GUILayout.Width(20)) && i > 0) {
                        scenes[i] = scenes[i - 1];
                        scenes[i - 1] = scene;
                        Save();
                    }

                    // �V�[���{�^��
                    EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
                    if (GUILayout.Button(Path.GetFileNameWithoutExtension(path))) {
                        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {  // ���ύX���������ꍇ�ɕۑ����邩�̊m�F�p
                            EditorSceneManager.OpenScene(path);
                        }
                    }
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �ݒ�f�[�^�̕ۑ�
        /// </summary>
        private void Save() {
            var guids = new List<string>();
            foreach (var scene in scenes) {
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(scene, out string guid, out long _)) {
                    guids.Add(guid);
                }
            }

            var content = string.Join("\n", guids.ToArray());
            File.WriteAllText(FilePath, content);
        }

        /// <summary>
        /// �ݒ�f�[�^�̓ǂݍ���
        /// </summary>
        private void Load() {
            scenes.Clear();
            if (File.Exists(FilePath)) {
                string content = File.ReadAllText(FilePath);
                foreach (var guid in content.Split(new char[] { '\n' })) {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
                    if (scene != null)
                        scenes.Add(scene);
                }
            }
        }


        /// ----------------------------------------------------------------------------
        // Util Method

        private static void GuiLine(int height = 1) {
            GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(height) });
        }


    }

}
#endif