using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;
using UniRx;
using nitou.UI;

namespace OtoGame.Presentation {
    using OtoGame.View;

    public sealed class MenuTopPagePresenter : PagePresenterBase<MenuTopPage, MenuTopView, MenuTopViewState> {

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MenuTopPagePresenter(MenuTopPage view, ITransitionService transitionService)
            : base(view, transitionService) {
        }


        /// ----------------------------------------------------------------------------
        // Protected Method        

        /// <summary>
        /// ページがロードされた直後に呼ばれる処理
        /// </summary>
        protected override Task ViewDidLoad(MenuTopPage view, MenuTopViewState viewState) {

            // 選択候補UI
            view.SelectableGroup = new SelectableGroup(
                new List<Selectable>() {
                    view.root.playButton,
                    view.root.optionButton,
                    view.root.infomationButton,
                    view.root.backButton
                }
            );

            // 遷移イベントのバインド
            view.root.playButton
                .SetOnClickDestination(TransitionService.MenuPage_PlayButtonClicked)
                .AddTo(this);
            view.root.optionButton
                .SetOnClickDestination(TransitionService.MenuPage_SettingsButtonClicked)
                .AddTo(this);
            view.root.infomationButton
                .SetOnClickDestination(TransitionService.MenuPage_CreditButtonClicked)
                .AddTo(this);
            viewState.OnBackButtonClicked
                .Subscribe(_ => TransitionService.PopCommandExecuted())
                .AddTo(this);

            return Task.CompletedTask;
        }


    }
}
