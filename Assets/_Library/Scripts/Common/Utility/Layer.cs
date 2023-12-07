using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [参考]
//  ねこじゃらシティ: レイヤーをインスペクターから選択可能にする構造体 https://nekojara.city/unity-layer-inspector

namespace nitou {

    [System.Serializable]
    public struct Layer {
        [SerializeField] private int _value;

        /// <summary>
        /// レイヤー値
        /// </summary>
        public int Value {
            get => _value;
            set {
                // レイヤーの範囲チェック
                if (value < 0 || 31 < value) {
                    throw new System.ArgumentOutOfRangeException(nameof(value), "レイヤーは0～31の範囲で指定してください。");
                }
                _value = value;
            }
        }

        /// <summary>
        /// レイヤー名
        /// </summary>
        public string Name {
            get => LayerMask.LayerToName(_value);
            set {
                var layerValue = LayerMask.NameToLayer(value);

                // レイヤー名が存在しない場合はエラー
                if (layerValue == -1)
                    throw new System.ArgumentException($"レイヤー名「{value}」は存在しません。", nameof(value));

                _value = layerValue;
            }
        }

        /// <summary>
        /// int型への変換 
        /// </summary>
        public static implicit operator int(Layer layer) {
            return layer.Value;
        }

        /// <summary>
        /// Layer型への変換 
        /// </summary>
        public static explicit operator Layer(int value) {
            return new Layer { Value = value };
        }

        /// <summary>
        /// string型への変換
        /// </summary>
        public override string ToString() {
            return $"{Name}({_value})";
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Layer))]
    public class LayerPropertyDrawer : PropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var valueProperty = property.FindPropertyRelative("_value");

            // 現在設定されているレイヤー値を取得
            var currentValue = valueProperty.intValue;

            // レイヤー一覧を表示
            var newValue = EditorGUI.LayerField(position, label, currentValue);

            // レイヤー値を更新
            valueProperty.intValue = newValue;

            EditorGUI.EndProperty();
        }
    }
#endif

}
