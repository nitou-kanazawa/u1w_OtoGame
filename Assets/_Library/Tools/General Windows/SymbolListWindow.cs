#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

// [参考]
//  コガネブログ: #defineで定義されているシンボルを一覧で表示するウィンドウ https://baba-s.hatenablog.com/entry/2014/07/25/105332

namespace nitou.Tools {

    /// <summary>
    /// #define で定義されているシンボルを一覧で表示するウィンドウ拡張
    /// </summary>
    public sealed class SymbolListWindow : EditorWindow {

        // ウインドウ描画用
        private Vector2 _scrollPos; // スクロールの座標


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        [MenuItem("Window/Nitou/Sybol/Symbol List")]
        private static void Open() => GetWindow<SymbolListWindow>("Symbol List");

        private void OnGUI() {

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(position.height));

            // 定義シンボルの取得
            var defines = EditorUserBuildSettings.activeScriptCompilationDefines;

            // シンボルを名前順でソート
            Array.Sort(defines);

            // 定義シンボルの一覧を表示
            foreach (var define in defines) {
                EditorGUILayout.BeginHorizontal(GUILayout.Height(20));

                // Copy ボタンが押された場合
                if (GUILayout.Button("Copy", GUILayout.Width(50), GUILayout.Height(20))) {
                    // クリップボードにシンボル名を登録
                    EditorGUIUtility.systemCopyBuffer = define;
                }

                // 選択可能なラベルを使用してシンボル名を表示
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