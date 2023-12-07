using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


// [参考]
//  DoTweenの教科書: カスタムプロパティ https://zenn.dev/ohbashunsuke/books/20200924-dotween-complete/viewer/dotween-34


namespace nitou.UI.Component {

    /// <summary>
    /// シンプルなパーセントゲージのViewクラス
    /// ※PresentorクラスでReactivePropaty等と紐づけて用いる
    /// </summary>
    public class PercentGageView : MonoBehaviour, IUIComponent {

        // 制御対象
        [SerializeField] Image _image;
        [SerializeField] TextMeshProUGUI _text;

        /// <summary>
        /// 現在のパーセント値
        /// </summary>
        public int Percent {
            get => _percent;
            set {
                _percent = value;
                float ratio = (float)value / 100f;

                // UIの更新
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
        /// アニメーション付きでView表示を更新する
        /// ※UIはカスタムプロパティ経由で更新する
        /// </summary>
        public void DOChangeValue(int value, float duration = 0.5f) =>
            DOTween.To(() => Percent, x => Percent = x, value, duration);

    }


}