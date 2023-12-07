using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace nitou.UI {

    /// <summary>
    /// ������Selectable���܂ރR���e�i�ł��邱�Ƃ��Ӗ�����C���^�[�t�F�[�X
    /// </summary>
    public interface ISelectableContainer {

        /// <summary>
        /// �f�t�H���g�őI�������UI
        /// </summary>
        public Selectable DefaultSelection { get; }

        /// <summary>
        /// �Ǘ�����UI���X�g
        /// </summary>
        public SelectableGroup SelectableGroup { get; set; }

        /// <summary>
        /// �V�X�e���N���X�Ɏ����I������邩�ǂ���
        /// </summary>
        //public bool AutoSelected { get;}

        /// <summary>
        /// �V�X�e���N���X�ɑI������鎞�̒x��
        /// </summary>
        public float Delay { get;}
    }

}