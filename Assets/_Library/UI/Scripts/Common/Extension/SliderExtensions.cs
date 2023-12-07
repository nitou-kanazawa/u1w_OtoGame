using System;
using UnityEngine.UI;
using UniRx;

namespace nitou.UI {

    /// <summary>
    /// Sliderの拡張メソッドクラス
    /// </summary>
    public static class SliderExtensions {

        /// ----------------------------------------------------------------------------
        // イベントの登録

        /// <summary>
        /// イベントを登録する拡張メソッド
        /// </summary>
        public static IDisposable SetOnValueChangedDestination(this Slider @this, Action<float> onValueChanged) {
            return @this.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(@this);
        }

    }
}
