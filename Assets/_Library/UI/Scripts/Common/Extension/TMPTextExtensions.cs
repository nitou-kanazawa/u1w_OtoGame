using System;
using TMPro;
using UniRx;

namespace nitou.UI {

    /// <summary>
    /// TMPTextの拡張メソッドクラス
    /// </summary>
    public static partial class TMPTextExtensions {

        /// <summary>
        /// 指定したストリート情報を表示させる拡張メソッド
        /// </summary>
        public static IDisposable SetTextSource(this TMP_Text @this, IObservable<string> source) {
            return source
                .Subscribe(x => { @this.text = x; })
                .AddTo(@this);
        }

        /// <summary>
        /// 指定したストリート情報を表示させる拡張メソッド
        /// </summary>
        public static IDisposable SetTextSource(this TMP_Text @this, IObservable<int> source, Func<int, string> converter = null) {
            return source
                .Subscribe(x => {
                    var text = converter == null ? x.ToString() : converter(x);
                    @this.text = text;
                })
                .AddTo(@this);
        }
    }
}
