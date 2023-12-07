#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

// [参考]
//  Odin community tool: Toggle Buttons Attribute https://odininspector.com/community-tools/5AB/toggle-buttons-attribute

namespace nitou {

    public class ToggleButtonsAttributeDrawer : OdinAttributeDrawer<ToggleButtonsAttribute> {

        private static readonly bool DO_MANUAL_COLORING = UnityVersion.IsVersionOrGreater(2019, 3);
        private static readonly Color ACTIVE_COLOR = EditorGUIUtility.isProSkin ? Color.white : new Color(0.802f, 0.802f, 0.802f, 1f);
        private static readonly Color INACTIVE_COLOR = EditorGUIUtility.isProSkin ? new Color(0.75f, 0.75f, 0.75f, 1f) : Color.white;

        private Color?[] _selectionColors;
        private Color? _color;

        private ValueResolver<string>[] _nameGetters;
        private ValueResolver<string>[] _tooltipGetters;
        private ValueResolver<Texture>[] _iconGetters;
        private ValueResolver<Color>[] _colorGetters;

        private GUIContent[] _buttonContents;
        private float[] _nameSizes;
        private int _rows = 1;
        private float _previousControlRectWidth;

        private bool _needUpdate = false;
        private float _totalNamesSize = 0f;


        /// ----------------------------------------------------------------------------
        // 

        public override bool CanDrawTypeFilter(Type type) {
            return type == typeof(bool);
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            _nameGetters = new[] {
                ValueResolver.GetForString(Property, Attribute._trueText),
                ValueResolver.GetForString(Property, Attribute._falseText)
            };

            _tooltipGetters = new[] {
                ValueResolver.GetForString(Property, Attribute._trueTooltip),
                ValueResolver.GetForString(Property, Attribute._falseTooltip)
            };

            _iconGetters = new[] {
                ValueResolver.Get(Property, Attribute._trueIcon, (Texture)null),
                ValueResolver.Get(Property, Attribute._falseIcon, (Texture)null)
            };

            _colorGetters = new[]{
                ValueResolver.Get(Property, Attribute._trueColor, Color.white),
                ValueResolver.Get(Property, Attribute._falseColor, Color.white)
            };

            _buttonContents = new GUIContent[2];

            for (int i = 0; i < 2; i++)
                _buttonContents[i] = new GUIContent(_nameGetters[i].GetValue(), _iconGetters[i].GetValue(),
                    _tooltipGetters[i].GetValue());

            _nameSizes = _buttonContents.Select(x => SirenixGUIStyles.MiniButtonMid.CalcSize(x).x).ToArray();

            _rows = 1;

            GUIHelper.RequestRepaint();

            if (!DO_MANUAL_COLORING)
                return;

            _selectionColors = new Color?[2];
            _color = new Color?();
        }

        /// <summary>
        /// 表示名の更新
        /// </summary>
        private void UpdateNames() {
            UpdateName(0);
            UpdateName(1);

            // Add extra padding to smaller button
            if (_nameSizes[0] > _nameSizes[1])
                _nameSizes[1] = Mathf.Lerp(_nameSizes[1], _nameSizes[0], Attribute._sizeCompensation);
            else
                _nameSizes[0] = Mathf.Lerp(_nameSizes[0], _nameSizes[1], Attribute._sizeCompensation);

            _totalNamesSize = _nameSizes[0] + _nameSizes[1];
        }

        /// <summary>
        /// 表示名の更新
        /// </summary>
        private void UpdateName(int index) {
            var newText = _nameGetters[index].GetValue();
            var newIcon = _iconGetters[index].GetValue();

            _buttonContents[index].tooltip = _tooltipGetters[index].GetValue();

            var needUpdate = _buttonContents[index].text != newText | _buttonContents[index].image != newIcon;

            _needUpdate |= needUpdate;

            _buttonContents[index].text = newText;
            _buttonContents[index].image = newIcon;
            _nameSizes[index] = SirenixGUIStyles.MiniButton.CalcSize(_buttonContents[index]).x;
        }

        protected override void DrawPropertyLayout(GUIContent label) {
            foreach (var valueResolver in _nameGetters)
                valueResolver.DrawError();

            foreach (var valueResolver in _iconGetters)
                valueResolver.DrawError();

            foreach (var valueResolver in _tooltipGetters)
                valueResolver.DrawError();

            foreach (var valueResolver in _colorGetters)
                valueResolver.DrawError();

            if (Event.current.type == EventType.Layout)
                UpdateNames();

            var currentValue = (bool)Property.ValueEntry.WeakSmartValue;

            var buttonIndex = 0;

            var rect = new Rect();

            SirenixEditorGUI.GetFeatureRichControlRect(label,
                Mathf.CeilToInt(EditorGUIUtility.singleLineHeight * (Attribute._singleButton ? 1 : _rows)),
                out int _, out bool _, out var valueRect);

            if (Attribute._singleButton) {
                DrawSingleButton(currentValue, valueRect);
            } else {
                valueRect.height = EditorGUIUtility.singleLineHeight;

                rect = valueRect;

                for (int rowIndex = 0; rowIndex < _rows; ++rowIndex) {
                    valueRect.xMin = rect.xMin;
                    valueRect.xMax = rect.xMax;

                    var xMax = valueRect.xMax;

                    for (int columnIndex = 0; columnIndex < (_rows == 2 ? 1 : 2); ++columnIndex) {
                        valueRect.width = (int)rect.width * _nameSizes[buttonIndex] / _totalNamesSize;
                        valueRect = DrawButton(buttonIndex, currentValue, valueRect, columnIndex, rowIndex, xMax);
                        ++buttonIndex;
                    }

                    valueRect.y += valueRect.height;
                }
            }

            if (Event.current.type != EventType.Repaint || _previousControlRectWidth == rect.width && !_needUpdate ||
                Attribute._singleButton)
                return;

            _previousControlRectWidth = rect.width;

            _rows = rect.width < _nameSizes[0] + _nameSizes[1] + 6f ? 2 : 1;

            _needUpdate = false;
        }

        private void DrawSingleButton(bool currentValue, Rect valueRect) {
            if (DO_MANUAL_COLORING)
                _color = UpdateColor(_color, currentValue ? ACTIVE_COLOR : INACTIVE_COLOR);

            GUIStyle style = currentValue ? SirenixGUIStyles.MiniButtonSelected : SirenixGUIStyles.MiniButton;

            GUI.backgroundColor = _colorGetters[currentValue ? 0 : 1].GetValue();

            if (DO_MANUAL_COLORING)
                GUIHelper.PushColor(_color.Value * GUI.color);

            valueRect.x--;
            valueRect.xMax += 2;

            if (GUI.Button(valueRect, _buttonContents[currentValue ? 0 : 1], style))
                Property.ValueEntry.WeakSmartValue = !currentValue;

            if (DO_MANUAL_COLORING)
                GUIHelper.PopColor();

            GUI.backgroundColor = Color.white;
        }

        private Rect DrawButton(int buttonIndex, bool currentValue, Rect valueRect, int columnIndex, int rowIndex,
            float xMax) {
            var selectionColor = new Color?();
            var buttonValue = buttonIndex == 0;

            if (DO_MANUAL_COLORING) {
                var color = currentValue == buttonValue ? ACTIVE_COLOR : INACTIVE_COLOR;
                selectionColor = _selectionColors[buttonValue ? 0 : 1];

                selectionColor = UpdateColor(selectionColor, color);

                _selectionColors[buttonValue ? 0 : 1] = selectionColor;
            }

            var position = valueRect;
            GUIStyle style;

            if (columnIndex == 0 && columnIndex == (_rows == 2 ? 1 : 2) - 1) {
                style = currentValue ? SirenixGUIStyles.MiniButtonSelected : SirenixGUIStyles.MiniButton;
                --position.x;
                position.xMax = xMax + 1f;
            } else if (buttonIndex == 0)
                style = currentValue ? SirenixGUIStyles.MiniButtonLeftSelected : SirenixGUIStyles.MiniButtonLeft;
            else {
                style = currentValue ? SirenixGUIStyles.MiniButtonRightSelected : SirenixGUIStyles.MiniButtonRight;
                position.xMax = xMax;
            }

            GUI.backgroundColor = _colorGetters[buttonIndex].GetValue();

            if (DO_MANUAL_COLORING)
                GUIHelper.PushColor(selectionColor.Value * GUI.color);

            if (GUI.Button(position, _buttonContents[buttonIndex], style))
                Property.ValueEntry.WeakSmartValue = buttonValue;

            GUI.backgroundColor = Color.white;

            if (DO_MANUAL_COLORING)
                GUIHelper.PopColor();


            valueRect.x += valueRect.width;

            return valueRect;
        }

        private static Color? UpdateColor(Color? nullable, Color color) {
            if (!nullable.HasValue)
                nullable = color;
            else if (nullable.Value != color && Event.current.type == EventType.Layout) {
                nullable = color.MoveTowards(color, EditorTimeHelper.Time.DeltaTime * 4f);

                GUIHelper.RequestRepaint();
            }

            return nullable;
        }
    }

}
#endif