using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.EventChannel.Shared {

    /// <summary>
    /// イベントリスナー
    /// </summary>
    public abstract class EventListener<Type, EventChannel> : MonoBehaviour
        where EventChannel : EventChannelSO<Type> {

        // 
        public EventChannel Channel;
        public event System.Action<Type> OnEventRaised;


        /// ----------------------------------------------------------------------------
        // Public Method

        private void OnEnable() {
            if (Channel == null) return;
            Channel.OnEventRaised += Respond;
        }

        private void OnDisable() {
            if (Channel == null) return;
            Channel.OnEventRaised -= Respond;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// イベント発火時のレスポンス
        /// </summary>
        public void Respond(Type value) => OnEventRaised.Invoke(value);     // ※nullチェックはChannel側で行う

    }

}