using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [�Q�l]
//  �˂������V�e�B: ���C���[���C���X�y�N�^�[����I���\�ɂ���\���� https://nekojara.city/unity-layer-inspector

namespace nitou {

    [System.Serializable]
    public struct Layer {
        [SerializeField] private int _value;

        /// <summary>
        /// ���C���[�l
        /// </summary>
        public int Value {
            get => _value;
            set {
                // ���C���[�͈̔̓`�F�b�N
                if (value < 0 || 31 < value) {
                    throw new System.ArgumentOutOfRangeException(nameof(value), "���C���[��0�`31�͈̔͂Ŏw�肵�Ă��������B");
                }
                _value = value;
            }
        }

        /// <summary>
        /// ���C���[��
        /// </summary>
        public string Name {
            get => LayerMask.LayerToName(_value);
            set {
                var layerValue = LayerMask.NameToLayer(value);

                // ���C���[�������݂��Ȃ��ꍇ�̓G���[
                if (layerValue == -1)
                    throw new System.ArgumentException($"���C���[���u{value}�v�͑��݂��܂���B", nameof(value));

                _value = layerValue;
            }
        }

        /// <summary>
        /// int�^�ւ̕ϊ� 
        /// </summary>
        public static implicit operator int(Layer layer) {
            return layer.Value;
        }

        /// <summary>
        /// Layer�^�ւ̕ϊ� 
        /// </summary>
        public static explicit operator Layer(int value) {
            return new Layer { Value = value };
        }

        /// <summary>
        /// string�^�ւ̕ϊ�
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

            // ���ݐݒ肳��Ă��郌�C���[�l���擾
            var currentValue = valueProperty.intValue;

            // ���C���[�ꗗ��\��
            var newValue = EditorGUI.LayerField(position, label, currentValue);

            // ���C���[�l���X�V
            valueProperty.intValue = newValue;

            EditorGUI.EndProperty();
        }
    }
#endif

}
