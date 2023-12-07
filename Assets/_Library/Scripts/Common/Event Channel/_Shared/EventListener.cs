using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.EventChannel.Shared {

    /// <summary>
    /// �C�x���g���X�i�[
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
        /// �C�x���g���Ύ��̃��X�|���X
        /// </summary>
        public void Respond(Type value) => OnEventRaised.Invoke(value);     // ��null�`�F�b�N��Channel���ōs��

    }

}