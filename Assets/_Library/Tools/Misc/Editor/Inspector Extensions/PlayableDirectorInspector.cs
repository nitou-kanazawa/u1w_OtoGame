#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

// [参考]
//  コガネブログ: PlayableDirectorのInspectorに"Play・Pause"などのボタンを追加するエディタ拡張 https://baba-s.hatenablog.com/entry/2022/02/21/090000

namespace nitou.Tools {

    /// <summary>
    /// PlayableDirectorのインスペクター拡張
    /// </summary>
    [CustomEditor(typeof(PlayableDirector))]
    public sealed class PlayableDirectorInspector : Editor {

        // オリジナルの拡張クラス
        private static readonly Type BASE_EDITOR_TYPE = typeof(Editor)
            .Assembly
            .GetType("UnityEditor.DirectorEditor");


        /// <summary>
        /// インスペクタ描画
        /// </summary>
        public override void OnInspectorGUI() {

            // -------------
            // 拡張分のインスペクター表示

            var playableDirector = (PlayableDirector)target;

            using (new EditorGUILayout.HorizontalScope())
            using (new GUIColorScope(MyEditorConfig.BasicButtonColor)) {

                // 再生
                if (GUILayout.Button("Play")) {
                    playableDirector.Play();
                }

                // 停止
                if (GUILayout.Button("Stop")) {
                    playableDirector.Stop();
                }

                // ポーズ
                if (GUILayout.Button("Pause")) {
                    playableDirector.Pause();
                }

                // 
                if (GUILayout.Button("Resume")) {
                    playableDirector.Resume();
                }
            }

            // -------------
            // オリジナルのインスペクター表示

            var editor = CreateEditor(playableDirector, BASE_EDITOR_TYPE);
            editor.OnInspectorGUI();
        }
    }
}
#endif