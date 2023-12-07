using System;
using UniRx;

namespace OtoGame.Model {

    /// <summary>
    /// ���ʂ�ON/OFF�������T�E���h�ݒ�N���X
    /// </summary>
    public sealed class SoundSetting {

        /// <summary>
        /// ����
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

        // ����
        private readonly ReactiveProperty<float> _volumeRP = new();
        private readonly ReactiveProperty<bool> _mutedRP = new();

        // �w�Ǘp
        public IReadOnlyReactiveProperty<float> VolumeRP => _volumeRP;
        public IReadOnlyReactiveProperty<bool> MutedRP => _mutedRP;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �l�̐ݒ�
        /// </summary>
        public void SetValues(float volume, bool muted) {
            Volume = volume;
            Muted = muted;
        }

    }
}
