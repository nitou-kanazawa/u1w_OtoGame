using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.LevelObjects {

    /// <summary>
    /// �I�[�f�B�I�Đ��ɔ����C�x���g���s���s���C���^�[�t�F�[�X
    /// </summary>
    public interface IMusicReactor {


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// �Ώۂ̉���
        /// </summary>
        public AudioClip TargetClip { get; }

        /// <summary>
        /// �Z�b�g�A�b�v���������Ă��邩�ǂ���
        /// </summary>
        public bool IsSetuped { get; }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �I�[�f�B�I�Đ��ɔ����X�V�C�x���g
        /// </summary>
        public void UpdateWithAudio(float audioTime);

        /// <summary>
        /// �|�[�Y���̏���
        /// </summary>
        public void OnPause();

        /// <summary>
        /// ���X�^�[�g���̏���
        /// </summary>
        public void OnUnPause();

        /// <summary>
        /// �I�����̏���
        /// </summary>
        public void OnStop();
    }

}