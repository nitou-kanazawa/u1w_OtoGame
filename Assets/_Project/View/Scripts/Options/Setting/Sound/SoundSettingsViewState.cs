using UniRx;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class SoundSettingsViewState : AppViewState {

        // 実装
        private readonly ReactiveProperty<float> _bgmVolume = new ();
        private readonly ReactiveProperty<float> _seVolume = new ();
        private readonly ReactiveProperty<float> _voiceVolume = new ();
        private readonly ReactiveProperty<bool> _isBgmEnabled = new ();
        private readonly ReactiveProperty<bool> _isSeEnabled = new ();
        private readonly ReactiveProperty<bool> _isVoiceEnabled = new ();

        // インターフェース
        public IReactiveProperty<float> BgmVolume => _bgmVolume;
        public IReactiveProperty<float> SeVolume => _seVolume;
        public IReactiveProperty<float> VoiceVolume => _voiceVolume;
        public IReactiveProperty<bool> IsBgmEnabled => _isBgmEnabled;
        public IReactiveProperty<bool> IsSeEnabled => _isSeEnabled;
        public IReactiveProperty<bool> IsVoiceEnabled => _isVoiceEnabled;


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 終了処理
        /// </summary>
        protected override void DisposeInternal() {
            _bgmVolume.Dispose();
            _seVolume.Dispose();
            _voiceVolume.Dispose();
            _isBgmEnabled.Dispose();
            _isSeEnabled.Dispose();
            _isVoiceEnabled.Dispose();
        }
    }

}
