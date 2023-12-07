using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


// [�Q�l]
//  DoTween�̋��ȏ�: �J�X�^���v���p�e�B https://zenn.dev/ohbashunsuke/books/20200924-dotween-complete/viewer/dotween-34


namespace nitou.UI.Component {

    /// <summary>
    /// �V���v���ȃp�[�Z���g�Q�[�W��View�N���X
    /// ��Presentor�N���X��ReactivePropaty���ƕR�Â��ėp����
    /// </summary>
    public class PercentGageView : MonoBehaviour, IUIComponent {

        // ����Ώ�
        [SerializeField] Image _image;
        [SerializeField] TextMeshProUGUI _text;

        /// <summary>
        /// ���݂̃p�[�Z���g�l
        /// </summary>
        public int Percent {
            get => _percent;
            set {
                _percent = value;
                float ratio = (float)value / 100f;

                // UI�̍X�V
                _image.fillAmount = ratio;
                _text.text = $"{value}%";
            }
        }
        private int _percent;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 

        private void Start() {
            _image.type = Image.Type.Filled;
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �A�j���[�V�����t����View�\�����X�V����
        /// ��UI�̓J�X�^���v���p�e�B�o�R�ōX�V����
        /// </summary>
        public void DOChangeValue(int value, float duration = 0.5f) =>
            DOTween.To(() => Percent, x => Percent = x, value, duration);

    }


}