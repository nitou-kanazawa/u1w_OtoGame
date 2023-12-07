using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

// [参考]
//  github: Kogane Check Box Window https://github.com/baba-s/Kogane.CheckBoxWindow

namespace nitou.Tools {

    /// <summary>
    /// チェックボックスのリストを表示するウインドウ
    /// </summary>
    internal sealed class CheckBoxWindowInstance : EditorWindow {

        //
        private const int ROW_HEIGHT = 18;

        /// <summary>
        /// リストデータ
        /// </summary>
        private IReadOnlyList<ICheckBoxWindowData> _dataList = Array.Empty<CheckBoxWindowData>();
        
        /// <summary>
        /// 
        /// </summary>
        private Action<IReadOnlyList<ICheckBoxWindowData>> _onOk;

        private SearchField _searchField;
        private GUIStyle _hoverStyle;
        private GUIStyle[] _alternatingRowStyleArray;
        private string _filteringText = string.Empty;
        private Vector2 _scrollPosition;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// ウインドウを開く
        /// </summary>
        public void Open(
            string title,
            IReadOnlyList<ICheckBoxWindowData> dataList,
            Action<IReadOnlyList<ICheckBoxWindowData>> onOk
        ) {
            titleContent = new(title);
            wantsMouseMove = true;
            _dataList = dataList;
            _onOk = onOk;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 
        /// </summary>
        private void OnGUI() {
            _searchField ??= new();
            _hoverStyle ??= CreateGUIStyle(new Color32(44, 93, 135, 255));

            _alternatingRowStyleArray ??= new[] {
                CreateGUIStyle( new Color32( 63, 63, 63, 255 ) ),
                CreateGUIStyle( new Color32( 56, 56, 56, 255 ) ),
            };

            // 
            using (new EditorGUILayout.HorizontalScope()) {
                if (GUILayout.Button("Select all", GUILayout.Width(80))) {
                    foreach (var x in _dataList) {
                        x.IsChecked = true;
                    }
                }

                if (GUILayout.Button("Deselect all", GUILayout.Width(80))) {
                    foreach (var x in _dataList) {
                        x.IsChecked = false;
                    }
                }

                _filteringText = _searchField.OnToolbarGUI(_filteringText);
            }

            // 
            using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(_scrollPosition)) {
                var mousePosition = Event.current.mousePosition;
                var y = 0;

                for (var i = 0; i < _dataList.Count; i++) {
                    var data = _dataList[i];
                    var name = data.Name;

                    if (!name.Contains(_filteringText, StringComparison.OrdinalIgnoreCase)) continue;

                    var isHover = ((int)mousePosition.y) / ROW_HEIGHT == y;
                    var style = isHover ? _hoverStyle : _alternatingRowStyleArray[i % 2];

                    using var hs = new EditorGUILayout.HorizontalScope(style);
                    using var vs = new EditorGUILayout.VerticalScope();

                    GUILayout.FlexibleSpace();
                    data.IsChecked = EditorGUILayout.ToggleLeft(name, data.IsChecked);
                    GUILayout.FlexibleSpace();

                    y++;
                }

                _scrollPosition = scrollViewScope.scrollPosition;
            }

            // 
            using (new EditorGUILayout.HorizontalScope()) {
                if (GUILayout.Button("OK")) {
                    _onOk(_dataList);
                    Close();
                }

                if (GUILayout.Button("Cancel")) {
                    Close();
                }
            }
        }

        private static GUIStyle CreateGUIStyle(Color color) {
            var background = new Texture2D(1, 1);
            background.SetPixel(0, 0, color);
            background.Apply();

            var style = new GUIStyle();
            style.normal.background = background;
            style.fixedHeight = ROW_HEIGHT;

            return style;
        }
    }
}