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
        /// イベント発火時のレスポンス
        /// </summary>
        public void Respond() => OnEventRaised.Invoke();
    }

}