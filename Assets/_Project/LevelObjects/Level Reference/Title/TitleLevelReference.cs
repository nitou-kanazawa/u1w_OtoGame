using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace OtoGame.LevelObjects {

    /// <summary>
    /// 
    /// </summary>
    public class TitleLevelReference : LevelReference {

        // シーン参照
        [SerializeField] TextMeshPro _titleText;

        private Transform _textTrans;
        private Vector3 _defaultPosition;
        private Vector3 _hidePosition;

        private readonly float showDuration = 1f;
        private readonly float hideDuration = 0.7f;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void Start() {
            // 
            _textTrans = (Transform)_titleText.rectTransform;
            _defaultPosition = _textTrans.position;
            _hidePosition = _defaultPosition + Vector3.down * 1.5f;

            // 初期値
            _textTrans.position = _hidePosition;
            _titleText.alpha = 0f;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        public UniTask ShowTitleText() {
             return DOTween.Sequence()
                .Append(_titleText.DOFade(1f, showDuration).SetEase(Ease.OutCubic))
                .Join(_textTrans.DOMove(_defaultPosition, showDuration).SetEase(Ease.OutCubic))
                .SetLink(_titleText.gameObject)
                .ToUniTask();
        }

        public UniTask HideTitleText() {
            return DOTween.Sequence()
                .Append(_titleText.DOFade(0f, hideDuration).SetEase(Ease.InCubic))
                .Join(_textTrans.DOMove(_hidePosition, hideDuration).SetEase(Ease.InCubic))
                .SetLink(_titleText.gameObject)
                .ToUniTask();
        }

    }

}