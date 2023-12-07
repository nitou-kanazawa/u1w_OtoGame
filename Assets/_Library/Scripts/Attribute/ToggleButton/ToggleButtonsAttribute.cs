using System;

// [参考]
//  qiita: カスタムAttributeまとめ 概要編 https://qiita.com/daria_sieben/items/5bfe4a40f000ff94acb3

namespace nitou {

    /// <summary>
    /// インスペクターにトグルボタンを表示する属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
    public class ToggleButtonsAttribute : Attribute {

        /// ----------------------------------------------------------------------------
        // Field

        // 表示テキスト
        public string _trueText;
        public string _falseText;

        // ツールチップ
        public string _trueTooltip;
        public string _falseTooltip;

        // アイコン
        public string _trueIcon;
        public string _falseIcon;

        // カラー
        public string _trueColor;
        public string _falseColor;

        // 
        public float _sizeCompensation;
        public bool _singleButton;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
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