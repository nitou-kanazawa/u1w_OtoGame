using System;
using UnityEngine.UI;
using UniRx;

namespace nitou.UI {

    /// <summary>
    ///  Toggleの拡張メソッドクラス
    /// </summary>
    public static class ToggleExtensions {

        /// ----------------------------------------------------------------------------
        // イベントの登録

        /// <summary>
        /// イベントを登録する拡張メソッド
        /// </summary>
        public static IDisposable SetOnValueChangedDestination(this Toggle @this, Action<bool> onValueChanged) {
            return @this.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(@this);
        }
    }
}
