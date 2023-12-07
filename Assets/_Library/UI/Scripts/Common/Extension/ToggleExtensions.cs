using System;
using UnityEngine.UI;
using UniRx;

namespace nitou.UI {

    /// <summary>
    ///  Toggle�̊g�����\�b�h�N���X
    /// </summary>
    public static class ToggleExtensions {

        /// ----------------------------------------------------------------------------
        // �C�x���g�̓o�^

        /// <summary>
        /// �C�x���g��o�^����g�����\�b�h
        /// </summary>
        public static IDisposable SetOnValueChangedDestination(this Toggle @this, Action<bool> onValueChanged) {
            return @this.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(@this);
        }
    }
}
