#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

// [�Q�l]
//  �R�K�l�u���O: ParticleSystem��Inspector��"Play�EPause"�Ȃǂ̃{�^����ǉ�����G�f�B�^�g�� https://baba-s.hatenablog.com/entry/2022/02/22/090000

namespace nitou.Tools {

    /// <summary>
    /// ParticleSystem�̃C���X�y�N�^�[�g��
    /// </summary>
    [CustomEditor(typeof(ParticleSystem))]
    public sealed class ParticleSystemInspector : Editor {
        
        // �I���W�i���̊g���N���X
        private static readonly Type BASE_EDITOR_TYPE = typeof(Editor)
            .Assembly
            .GetType("UnityEditor.ParticleSystemInspector");

        /// <summary>
        /// �C���X�y�N�^�`��
        /// </summary>
        public override void OnInspectorGUI() {

            // -------------
            // �g�����̃C���X�y�N�^�[�\��

            var particleSystem = (ParticleSystem)target;

            using (new EditorGUILayout.HorizontalScope())
            using (new GUIColorScope(MyEditorConfig.BasicButtonColor)) {

                // �Đ�
                if (GUILayout.Button("Play")) {
                    particleSystem.Play();
                }

                // ��~
                if (GUILayout.Button("Stop")) {
                    particleSystem.Stop();
                }

                // �|�[�Y
                if (GUILayout.Button("Pause")) {
                    particleSystem.Pause();
                }

                // �N���A
                if (GUILayout.Button("Clear")) {
                    particleSystem.Clear();
                }
            }


            // -------------
            // �I���W�i���̃C���X�y�N�^�[�\��

            var editor = CreateEditor(particleSystem, BASE_EDITOR_TYPE);
            editor.OnInspectorGUI();
        }
    }
}
#endif