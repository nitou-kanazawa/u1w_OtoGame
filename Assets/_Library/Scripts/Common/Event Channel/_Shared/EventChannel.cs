using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou.EventChannel.Shared {

    /// <summary>
    /// �C�x���g�`�����l���p�̂�������ƂȂ�Scriptable Object
    /// </summary>
    public abstract class EventChannelSO : ScriptableObject {

        // �������i�����������j
        [Multiline]
        [SerializeField] private string _description = default;

        // �C�x���g
        public event System.Action OnEventRaised = delegate { };

        /// <summary>
        /// �C�x���g�̔���
        /// </summary>
        [Button("Raise")]
        public void RaiseEvent() =>OnEventRaised.Invoke();
    }


    /// <summary>
    /// �C�x���g�`�����l���p�̂�������ƂȂ�Scriptable Object
    /// </summary>
    public abstract class EventChannelSO<Type> : ScriptableObject {

        // �������i�����������j
        [Multiline]
        [SerializeField] private string _description = default;

        // �C�x���g
        public event System.Action<Type> OnEventRaised = delegate { };

        /// <summary>
        /// �C�x���g�̔���
        /// </summary>
        [Button("Raise")]
        public void RaiseEvent(Type value) {
            if (value == null) {
                Debug.LogWarning($"[{name}] �C�x���g������null�ł�");
                return;
            }
            OnEventRaised.Invoke(value);
        }
    }
}