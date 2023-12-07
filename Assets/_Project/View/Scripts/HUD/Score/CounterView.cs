using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace OtoGame.View.Components {

    public class CounterView : MonoBehaviour {

        [Title("View components")]
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Image _backdropImage;


        [Title("Parameters")]
        [SerializeField] private Color _backColor = Color.white;


        /// ----------------------------------------------------------------------------
        // Protected Method 

        /// <summary>
        /// ílÇê›íËÇ∑ÇÈ
        /// </summary>
        public void SetValue(int value) {
            _countText.text = value.ToString();
        }


        /// ----------------------------------------------------------------------------
        // Editor Method
#if UNITY_EDITOR
        private void OnValidate() {
            if (_backdropImage != null) {
                _backdropImage.color = _backColor;
            }
        }
#endif


    }

}