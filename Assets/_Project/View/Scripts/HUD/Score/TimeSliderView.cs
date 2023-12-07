using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

namespace OtoGame.View.Components {

    public class TimeSliderView : MonoBehaviour {

        [Title("Slider")]
        [SerializeField] private Slider _timeSlider;

        [Title("Text")]
        [SerializeField] private TextMeshProUGUI _currentTimeText;
        [SerializeField] private TextMeshProUGUI _maxTimeText;



        public void SetValue(float time, float maxTime) {

            // スライダー
            var progress = (maxTime != 0) ? time / maxTime : 0;
            _timeSlider.value = progress;

            // テキスト
            _currentTimeText.text = ToDisplayTime(time);
            _maxTimeText.text = ToDisplayTime(maxTime);
        }


        public string ToDisplayTime(float second) {
            return ((int)(second / 60)).ToString("00") + ":" + ((int)second % 60).ToString("00");
        }
    }

}