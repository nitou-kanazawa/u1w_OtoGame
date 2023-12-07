using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using Sirenix.OdinInspector;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class SettingsView : AppView<SettingsViewState> {

        // コンポーネント
        [BoxGroup("設定項目"), Indent]
        public SoundSettingsView SoundSettingsView;

        [BoxGroup("その他"), Indent]
        public Button CloseButton;


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override async UniTask Initialize(SettingsViewState viewState) {

            // 
            await SoundSettingsView.InitializeAsync(viewState.SoundSettings);

            CloseButton.SetOnClickDestination(viewState.InvokeCloseButtonClicked).AddTo(this);
        }

    }
}
