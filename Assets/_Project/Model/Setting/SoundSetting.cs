using System;
using UniRx;

namespace OtoGame.Model {

    /// <summary>
    /// 音量とON/OFF情報を持つサウンド設定クラス
    /// </summary>
    public sealed class SoundSetting {

        /// <summary>
        /// 音量
        /// </summary>
        public float Volume {
            get => _volumeRP.Value; 
            set => _volumeRP.Value = value; 
        }

        /// <summary>
        /// ON/OFF
        /// </summary>
        public bool Muted { 
            get => _mutedRP.Value; 
            set => _mutedRP.Value = value; 
        }

        // 実装
        private readonly ReactiveProperty<float> _volumeRP = new();
        private readonly ReactiveProperty<bool> _mutedRP = new();

        // 購読用
        public IReadOnlyReactiveProperty<float> VolumeRP => _volumeRP;
        public IReadOnlyReactiveProperty<bool> MutedRP => _mutedRP;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 値の設定
        /// </summary>
        public void SetValues(float volume, bool muted) {
            Volume = volume;
            Muted = muted;
        }

    }
}
