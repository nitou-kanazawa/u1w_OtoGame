#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

/*

namespace nitou.Tools {

    /// <summary>
    /// Animator�̃C���X�y�N�^�[�g��
    /// </summary>
    [CustomEditor(typeof(Animator))]
    public sealed class AnimatorInspector : Editor {

        // �I���W�i���̊g���N���X
        private static readonly Type BASE_EDITOR_TYPE = typeof(Editor)
            .Assembly
            .GetType("UnityEditor.AnimatorInspector");


        /// <summary>
        /// �C���X�y�N�^�`��
        /// </summary>
        public override void OnInspectorGUI() {

            // -------------
            // �g�����̃C���X�y�N�^�[�\��

            var animator = (Animator)target;

            using (new EditorGUILayout.HorizontalScope()) {
                if (GUILayout.Button("Open Animator Window")) {
                    EditorApplication.ExecuteMenuItem("Window/Animation/Animator");
                }
                if (GUILayout.Button("Open Animation Window")) {
                    EditorApplication.ExecuteMenuItem("Window/Animation/Animation");
                }
            }

            // -------------
            // �I���W�i���̃C���X�y�N�^�[�\��

            var editor = CreateEditor(animator, BASE_EDITOR_TYPE);

            editor.OnInspectorGUI();
        }
    }
}
*/
#endif