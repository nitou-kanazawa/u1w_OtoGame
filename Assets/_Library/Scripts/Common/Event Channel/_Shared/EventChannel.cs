using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou.EventChannel.Shared {

    /// <summary>
    /// イベントチャンネル用のたたき台となるScriptable Object
    /// </summary>
    public abstract class EventChannelSO : ScriptableObject {

        // 説明文（※メモ書き）
        [Multiline]
        [SerializeField] private string _description = default;

        // イベント
        public event System.Action OnEventRaised = delegate { };

        /// <summary>
        /// イベントの発火
        /// </summary>
        [Button("Raise")]
        public void RaiseEvent() =>OnEventRaised.Invoke();
    }


    /// <summary>
    /// イベントチャンネル用のたたき台となるScriptable Object
    /// </summary>
    public abstract class EventChannelSO<Type> : ScriptableObject {

        // 説明文（※メモ書き）
        [Multiline]
        [SerializeField] private string _description = default;

        // イベント
        public event System.Action<Type> OnEventRaised = delegate { };

        /// <summary>
        /// イベントの発火
        /// </summary>
        [Button("Raise")]
        public void RaiseEvent(Type value) {
            if (value == null) {
                Debug.LogWarning($"[{name}] イベント引数がnullです");
                return;
            }
            OnEventRaised.Invoke(value);
        }
    }
}