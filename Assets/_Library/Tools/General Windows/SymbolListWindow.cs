#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

// [�Q�l]
//  �R�K�l�u���O: #define�Œ�`����Ă���V���{�����ꗗ�ŕ\������E�B���h�E https://baba-s.hatenablog.com/entry/2014/07/25/105332

namespace nitou.Tools {

    /// <summary>
    /// #define �Œ�`����Ă���V���{�����ꗗ�ŕ\������E�B���h�E�g��
    /// </summary>
    public sealed class SymbolListWindow : EditorWindow {

        // �E�C���h�E�`��p
        private Vector2 _scrollPos; // �X�N���[���̍��W


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        [MenuItem("Window/Nitou/Sybol/Symbol List")]
        private static void Open() => GetWindow<SymbolListWindow>("Symbol List");

        private void OnGUI() {

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(position.height));

            // ��`�V���{���̎擾
            var defines = EditorUserBuildSettings.activeScriptCompilationDefines;

            // �V���{���𖼑O���Ń\�[�g
            Array.Sort(defines);

            // ��`�V���{���̈ꗗ��\��
            foreach (var define in defines) {
                EditorGUILayout.BeginHorizontal(GUILayout.Height(20));

                // Copy �{�^���������ꂽ�ꍇ
                if (GUILayout.Button("Copy", GUILayout.Width(50), GUILayout.Height(20))) {
                    // �N���b�v�{�[�h�ɃV���{������o�^
                    EditorGUIUtility.systemCopyBuffer = define;
                }

                // �I���\�ȃ��x�����g�p���ăV���{������\��
                EditorGUILayout.SelectableLabel(define, GUILayout.Height(20));

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }

        
        /// ----------------------------------------------------------------------------
        // Private Method

    }

}
#endif