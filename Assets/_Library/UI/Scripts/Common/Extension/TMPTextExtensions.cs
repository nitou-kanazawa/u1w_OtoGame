using System;
using TMPro;
using UniRx;

namespace nitou.UI {

    /// <summary>
    /// TMPText�̊g�����\�b�h�N���X
    /// </summary>
    public static partial class TMPTextExtensions {

        /// <summary>
        /// �w�肵���X�g���[�g����\��������g�����\�b�h
        /// </summary>
        public static IDisposable SetTextSource(this TMP_Text @this, IObservable<string> source) {
            return source
                .Subscribe(x => { @this.text = x; })
                .AddTo(@this);
        }

        /// <summary>
        /// �w�肵���X�g���[�g����\��������g�����\�b�h
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
