using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace OtoGame.View.Components {

    public class ComboTextView : MonoBehaviour {

        [Title("View components")]
        [SerializeField] private TextMeshProUGUI _comboText;
        [SerializeField] private CanvasGroup _canvasGroup;


        /// ----------------------------------------------------------------------------
        // Protected Method 

        /// <summary>
        /// ílÇê›íËÇ∑ÇÈ
        /// </summary>
        public void SetValue(int value) {
            _canvasGroup.alpha = (value >= 5) ? 1 : 0;
            _comboText.text = $"{value}";
        }


    }

}