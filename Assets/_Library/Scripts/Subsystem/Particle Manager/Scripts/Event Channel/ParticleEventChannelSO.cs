using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace nitou.Particle {

    /// <summary>
    /// パーティクルマネージャーのイベントチャンネル
    /// (※モジュール外部からの呼び出しメソッドを提供)
    /// </summary>
    [CreateAssetMenu(fileName = "Event_Particle", menuName = "EventChannel SO/Particle Event")]
    public class ParticleEventChannelSO : ScriptableObject {

        /// <summary>
        /// イベント通知
        /// </summary>
        internal event UnityAction<ParticleRequestData> OnEventRaised = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 指定した名前のパーティクル再生
        /// </summary>
        public void PlayParticle(string particleName, Vector3 position, Quaternion rotation) =>
            OnEventRaised.Invoke(new(particleName, position, rotation, false));

        /// <summary>
        /// 指定した名前のパーティクル再生 (オーバーロード)
        /// </summary>
        public void PlayParticle(string particleName, Vector3 position) =>
            OnEventRaised.Invoke(new(particleName, position, Quaternion.identity, false));

        /// <summary>
        /// 指定した名前のパーティクル再生
        /// </summary>
        public void PlayParticleWithEvent(string particleName, Vector3 position, Quaternion rotation) =>
            OnEventRaised.Invoke(new(particleName, position, rotation, true));

        /// <summary>
        /// 指定した名前のパーティクル再生 (オーバーロード)
        /// </summary>
        public void PlayParticleWithEvent(string particleName, Vector3 position) =>
            OnEventRaised.Invoke(new(particleName, position, Quaternion.identity, true));
    }


    // ----------------------------------------------------------------------------

    /// <summary>
    /// データ受け渡し用のクラス
    /// </summary>
    [System.Serializable]
    internal class ParticleRequestData {

        // フィールド
        public string particleName;
        public Vector3 position;
        public Quaternion rotation;
        public bool withEvent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ParticleRequestData(string particleName, Vector3 position, Quaternion rotation, bool withEvent) {
            this.particleName = particleName;
            this.position = position;
            this.rotation = rotation;
            this.withEvent = withEvent;
        }
    }
}