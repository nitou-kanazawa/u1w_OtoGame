using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {

    public abstract class PagePresenterBase<TPage, TRootView, TRootViewState>
        : PagePresenter<TPage, TRootView, TRootViewState>
        where TPage : Page<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        /// <summary>
        /// UI画面遷移サービス
        /// </summary>
        protected ITransitionService TransitionService { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected PagePresenterBase(TPage view, ITransitionService transitionService) : base(view) {
            TransitionService = transitionService;
        }
    }
}
