using UnityEngine;

namespace nitou.EventChannel {

    public class VoidEventListener : MonoBehaviour {

        // 
        public VoidEventChannelSO Channel = null;
        public event System.Action OnEventRaised = delegate { };


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
        public void Respond() => OnEventRaised.Invoke();
    }

}