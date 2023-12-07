using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using Sirenix.OdinInspector;


namespace nitou.UI.Component {

    public class HorizontalSelecter : MonoBehaviour {

        /// ----------------------------------------------------------------------------
        #region Inner Definition

        [System.Serializable]
        public class OptionItem {
            public string contentNeme = "Option";
            public object contentData;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // Public Method

        private const string CONTROL_BTN = "Control buttons";
        
        [TitleGroup(CONTROL_BTN), Indent]
        [SerializeField] private Button _previousButton;
        
        [TitleGroup(CONTROL_BTN), Indent]
        [SerializeField] private Button _nextButton;


        [TitleGroup(CONTROL_BTN), Indent]
        [SerializeField] private TextMeshProUGUI _text;


        /// <summary>
        /// クリックされた時のイベント通知
        /// </summary>
        public event System.Action OnValueChanged = delegate { };

        public List<OptionItem> optionItems = new();


        
        
        /// ----------------------------------------------------------------------------
        // Public Method



        public void Initialize() {
            _previousButton.SetOnClickDestination(OnMovePrevious).AddTo(this);
            _nextButton.SetOnClickDestination(OnMoveNext).AddTo(this);
        }



        void OnMovePrevious() {

        }

        void OnMoveNext() {


        }
    }

}