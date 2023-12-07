#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

/*

namespace nitou.Tools {

    /// <summary>
    /// Animatorのインスペクター拡張
    /// </summary>
    [CustomEditor(typeof(Animator))]
    public sealed class AnimatorInspector : Editor {

        // オリジナルの拡張クラス
        private static readonly Type BASE_EDITOR_TYPE = typeof(Editor)
            .Assembly
            .GetType("UnityEditor.AnimatorInspector");


        /// <summary>
        /// インスペクタ描画
        /// </summary>
        public override void OnInspectorGUI() {

            // -------------
            // 拡張分のインスペクター表示

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
            // オリジナルのインスペクター表示

            var editor = CreateEditor(animator, BASE_EDITOR_TYPE);

            editor.OnInspectorGUI();
        }
    }
}
*/
#endif