using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace OtoGame.View.Components {

    public class ScoreTextView : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _scoreText;

        private int _score = 0;

        Tween tween;


        public void SetValue(int value) {
            var tmp = _score;
            _score = value;

            DOTween.To(() => tmp, x => tmp = x, value, 0.2f)
                .OnUpdate(() => _scoreText.text = tmp.ToString())
                .SetLink(gameObject);
        }
    }

}