using System;
using UniRx;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class SettingsViewState : AppViewState  {

        /// <summary>
        /// クリック時のイベント通知
        /// </summary>
        public IObservable<Unit> CloseButtonClicked => _onCloseButtonClickedSubject;
        private readonly Subject<Unit> _onCloseButtonClickedSubject = new();

        // サウンド設定
        public SoundSettingsViewState SoundSettings { get; } = new();


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 閉じるボタンの実行
        /// </summary>
        public void InvokeCloseButtonClicked() {
            _onCloseButtonClickedSubject.OnNext(Unit.Default);
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 終了処理
        /// </summary>
        protected override void DisposeInternal() {
            SoundSettings.Dispose();
            _onCloseButtonClickedSubject.Dispose();
        }
    }
}
