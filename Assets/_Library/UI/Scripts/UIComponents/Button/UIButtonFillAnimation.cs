using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

namespace nitou.UI.Component {

    /// <summary>
    /// 独自ボタンにアニメーション再生をフックするコンポーネント
    /// </summary>
    public class UIButtonFillAnimation : MonoBehaviour ,ISelectHandler, IDeselectHandler{

        // 対象のボタン

        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _text;

        [Space]

        [SerializeField] private Color _rollOverTextColor;
        private Color _defaultTextColor;

        private float _duration = 0.2f;

        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method


        private void Start() {
            _fillImage.fillAmount = 0f;
            _defaultTextColor = _text.color;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        void ISelectHandler.OnSelect(BaseEventData eventData) {
            _fillImage.DOFillAmount(1f, _duration).SetEase(Ease.OutCubic).SetLink(gameObject);
            _text.DOColor(_rollOverTextColor, _duration).SetLink(gameObject);
        }

        void IDeselectHandler.OnDeselect(BaseEventData eventData) {
            _fillImage.DOFillAmount(0f, _duration).SetEase(Ease.OutCubic).SetLink(gameObject);
            _text.DOColor(_defaultTextColor, _duration).SetLink(gameObject);
        }
    }

}