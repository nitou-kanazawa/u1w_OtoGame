using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using Sirenix.OdinInspector;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class SoundSettingsView : AppView<SoundSettingsViewState> {

        // コンポーネント
        [BoxGroup("BGM"), Indent] public Toggle bgmToggle;
        [BoxGroup("BGM"), Indent] public Slider bgmSlider;

        [BoxGroup("SE"), Indent] public Toggle seToggle;
        [BoxGroup("SE"), Indent] public Slider seSlider;

        //[BoxGroup("VOICE"), Indent] public Toggle voiceToggle;
        //[BoxGroup("VOICE"), Indent] public Slider voiceSlider;


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override UniTask Initialize(SoundSettingsViewState viewState) {
            
            // 音量設定の同期 (ViewState => View)
            viewState.BgmVolume.Subscribe(x => bgmSlider.value = x).AddTo(this);
            viewState.SeVolume.Subscribe(x => seSlider.value = x).AddTo(this);
            //viewState.VoiceVolume.Subscribe(x => voiceSlider.value = x).AddTo(this);

            viewState.IsBgmEnabled.Subscribe(x => bgmToggle.isOn = x).AddTo(this);
            viewState.IsBgmEnabled.Subscribe(x => bgmSlider.interactable = x).AddTo(this);
            viewState.IsSeEnabled.Subscribe(x => seToggle.isOn = x).AddTo(this);
            viewState.IsSeEnabled.Subscribe(x => seSlider.interactable = x).AddTo(this);
            //viewState.IsVoiceEnabled.Subscribe(x => voiceToggle.isOn = x).AddTo(this);
            //viewState.IsVoiceEnabled.Subscribe(x => voiceSlider.interactable = x).AddTo(this);

            // 音量設定の同期 (View => ViewState)
            bgmSlider.SetOnValueChangedDestination(x => viewState.BgmVolume.Value = x).AddTo(this);
            bgmToggle.SetOnValueChangedDestination(x => viewState.IsBgmEnabled.Value = x).AddTo(this);
            seSlider.SetOnValueChangedDestination(x => viewState.SeVolume.Value = x).AddTo(this);
            seToggle.SetOnValueChangedDestination(x => viewState.IsSeEnabled.Value = x).AddTo(this);
            //voiceSlider.SetOnValueChangedDestination(x => viewState.VoiceVolume.Value = x).AddTo(this);
            //voiceToggle.SetOnValueChangedDestination(x => viewState.IsVoiceEnabled.Value = x).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}
