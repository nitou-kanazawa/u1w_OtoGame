using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace nitou.Particle {

    /// <summary>
    /// �p�[�e�B�N���}�l�[�W���[�̃C�x���g�`�����l��
    /// (�����W���[���O������̌Ăяo�����\�b�h���)
    /// </summary>
    [CreateAssetMenu(fileName = "Event_Particle", menuName = "EventChannel SO/Particle Event")]
    public class ParticleEventChannelSO : ScriptableObject {

        /// <summary>
        /// �C�x���g�ʒm
        /// </summary>
        internal event UnityAction<ParticleRequestData> OnEventRaised = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ�
        /// </summary>
        public void PlayParticle(string particleName, Vector3 position, Quaternion rotation) =>
            OnEventRaised.Invoke(new(particleName, position, rotation, false));

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ� (�I�[�o�[���[�h)
        /// </summary>
        public void PlayParticle(string particleName, Vector3 position) =>
            OnEventRaised.Invoke(new(particleName, position, Quaternion.identity, false));

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ�
        /// </summary>
        public void PlayParticleWithEvent(string particleName, Vector3 position, Quaternion rotation) =>
            OnEventRaised.Invoke(new(particleName, position, rotation, true));

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ� (�I�[�o�[���[�h)
        /// </summary>
        public void PlayParticleWithEvent(string particleName, Vector3 position) =>
            OnEventRaised.Invoke(new(particleName, position, Quaternion.identity, true));
    }


    // ----------------------------------------------------------------------------

    /// <summary>
    /// �f�[�^�󂯓n���p�̃N���X
    /// </summary>
    [System.Serializable]
    internal class ParticleRequestData {

        // �t�B�[���h
        public string particleName;
        public Vector3 position;
        public Quaternion rotation;
        public bool withEvent;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ParticleRequestData(string particleName, Vector3 position, Quaternion rotation, bool withEvent) {
            this.particleName = particleName;
            this.position = position;
            this.rotation = rotation;
            this.withEvent = withEvent;
        }
    }
}