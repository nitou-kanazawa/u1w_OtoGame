using System;

// [�Q�l]
//  qiita: �J�X�^��Attribute�܂Ƃ� �T�v�� https://qiita.com/daria_sieben/items/5bfe4a40f000ff94acb3

namespace nitou {

    /// <summary>
    /// �C���X�y�N�^�[�Ƀg�O���{�^����\�����鑮��
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
    public class ToggleButtonsAttribute : Attribute {

        /// ----------------------------------------------------------------------------
        // Field

        // �\���e�L�X�g
        public string _trueText;
        public string _falseText;

        // �c�[���`�b�v
        public string _trueTooltip;
        public string _falseTooltip;

        // �A�C�R��
        public string _trueIcon;
        public string _falseIcon;

        // �J���[
        public string _trueColor;
        public string _falseColor;

        // 
        public float _sizeCompensation;
        public bool _singleButton;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ToggleButtonsAttribute(
            string trueText = "Yes", string falseText = "No", 
            bool singleButton = false, float sizeCompensation = 1f, 
            string trueTooltip = "", string falseTooltip = "",
            string trueColor = "", string falseColor = "", 
            string trueIcon = "", string falseIcon = ""
            ) {

            _trueText = trueText;
            _falseText = falseText;

            _singleButton = singleButton;
            _sizeCompensation = sizeCompensation;

            _trueTooltip = trueTooltip;
            _falseTooltip = falseTooltip;

            _trueIcon = trueIcon;
            _falseIcon = falseIcon;

            _trueColor = trueColor;
            _falseColor = falseColor;
        }
    }

}