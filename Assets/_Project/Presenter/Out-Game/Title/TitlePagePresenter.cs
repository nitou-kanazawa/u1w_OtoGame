using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using nitou;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {
    using OtoGame.View;

    public sealed class TitlePagePresenter : PagePresenterBase<TitlePage, TitleView, TitleViewState> {

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TitlePagePresenter(TitlePage view, ITransitionService transitionService)
            : base(view, transitionService) {
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// モーダルがロードされた直後に呼ばれる処理
        /// </summary>
        protected override Task ViewDidLoad(TitlePage view, TitleViewState viewState) {
            
            Debug_.Log("[Presenter] didLoad", Colors.GreenYellow);


            // UI選択グループ
            view.SelectableGroup = new SelectableGroup(view.root._button);


            viewState.OnClicked.Subscribe(_ => TransitionService.TitlePage_StartButtonClicked()).AddTo(this);

            return Task.CompletedTask;
        }

        protected override Task ViewWillPushEnter(TitlePage view, TitleViewState viewState) {
            Debug_.Log("[Presenter] willPushEnter", Colors.GreenYellow);


            return base.ViewWillPushEnter(view, viewState);
        }

        protected override void ViewDidPushEnter(TitlePage view, TitleViewState viewState) {
            Debug_.Log("[Presenter] didPushEnter", Colors.GreenYellow);

            base.ViewDidPushEnter(view, viewState);
        }



    }
}
