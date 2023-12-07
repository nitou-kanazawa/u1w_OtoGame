#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

// [�Q�l]
//  �R�K�l�u���O: PlayableDirector��Inspector��"Play�EPause"�Ȃǂ̃{�^����ǉ�����G�f�B�^�g�� https://baba-s.hatenablog.com/entry/2022/02/21/090000

namespace nitou.Tools {

    /// <summary>
    /// PlayableDirector�̃C���X�y�N�^�[�g��
    /// </summary>
    [CustomEditor(typeof(PlayableDirector))]
    public sealed class PlayableDirectorInspector : Editor {

        // �I���W�i���̊g���N���X
        private static readonly Type BASE_EDITOR_TYPE = typeof(Editor)
            .Assembly
            .GetType("UnityEditor.DirectorEditor");


        /// <summary>
        /// �C���X�y�N�^�`��
        /// </summary>
        public override void OnInspectorGUI() {

            // -------------
            // �g�����̃C���X�y�N�^�[�\��

            var playableDirector = (PlayableDirector)target;

            using (new EditorGUILayout.HorizontalScope())
            using (new GUIColorScope(MyEditorConfig.BasicButtonColor)) {

                // �Đ�
                if (GUILayout.Button("Play")) {
                    playableDirector.Play();
                }

                // ��~
                if (GUILayout.Button("Stop")) {
                    playableDirector.Stop();
                }

                // �|�[�Y
                if (GUILayout.Button("Pause")) {
                    playableDirector.Pause();
                }

                // 
                if (GUILayout.Button("Resume")) {
                    playableDirector.Resume();
                }
            }

            // -------------
            // �I���W�i���̃C���X�y�N�^�[�\��

            var editor = CreateEditor(playableDirector, BASE_EDITOR_TYPE);
            editor.OnInspectorGUI();
        }
    }
}
#endif