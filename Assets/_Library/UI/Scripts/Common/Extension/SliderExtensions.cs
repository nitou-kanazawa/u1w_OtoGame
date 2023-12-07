using System;
using UnityEngine.UI;
using UniRx;

namespace nitou.UI {

    /// <summary>
    /// Slider�̊g�����\�b�h�N���X
    /// </summary>
    public static class SliderExtensions {

        /// ----------------------------------------------------------------------------
        // �C�x���g�̓o�^

        /// <summary>
        /// �C�x���g��o�^����g�����\�b�h
        /// </summary>
        public static IDisposable SetOnValueChangedDestination(this Slider @this, Action<float> onValueChanged) {
            return @this.onValueChanged
                .AsObservable()
                .Subscribe(onValueChanged.Invoke)
                .AddTo(@this);
        }

    }
}
