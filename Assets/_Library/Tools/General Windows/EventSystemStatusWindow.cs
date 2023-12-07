#if UNITY_EDITOR
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.EventSystems;

// [参考]
//  コガネブログ: EventSystem の情報をいつでも見られるようにする EditorWindow https://baba-s.hatenablog.com/entry/2022/10/18/090000

namespace nitou.Tools {

    /// <summary>
    /// EventSystemの情報を表示するウインドウ
    /// </summary>
    public class EventSystemStatusWindow : EditorWindow {

        // ウインドウ描画用
        private SearchField _searchField;
        private string _filteringText;
        private Vector2 _scrollPosition;
        private GUIStyle _textAreaStyleCache;

        private GUIStyle TextAreaStyle =>
            _textAreaStyleCache ??= new("PreOverlayLabel") {
                richText = true,
                alignment = TextAnchor.UpperLeft,
                fontStyle = FontStyle.Normal
            };


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        [MenuItem("Window/Nitou/UI/Event System Status")]
        private static void Open() => GetWindow<EventSystemStatusWindow>("Event System");

        private void OnGUI() {
            if (!Application.isPlaying) return;

            _searchField ??= new();

            var eventSystem = EventSystem.current;

            if (eventSystem == null) return;

            var status = eventSystem.ToString();

            _filteringText = _searchField.OnToolbarGUI(_filteringText);

            if (GUILayout.Button("Copy", EditorStyles.toolbarButton)) {
                EditorGUIUtility.systemCopyBuffer = Regex.Replace(status, "<.*?>", string.Empty);
            }

            using var scrollViewScope = new EditorGUILayout.ScrollViewScope(_scrollPosition);

            var texts = string.IsNullOrWhiteSpace(_filteringText)
                    ? status
                    : string.Join("\n", status.Split('\n').Where(x => x.Contains(_filteringText, StringComparison.OrdinalIgnoreCase)))
                ;

            EditorGUILayout.TextArea(texts, TextAreaStyle);

            _scrollPosition = scrollViewScope.scrollPosition;
        }

        private void Update() {
            Repaint();
        }

    }

}
#endif