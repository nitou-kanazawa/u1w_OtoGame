using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {

    public abstract class ModalPresenterBase<TModal, TRootView, TRootViewState>
        : ModalPresenter<TModal, TRootView, TRootViewState>
        where TModal : Modal<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        /// <summary>
        /// UI画面遷移サービス
        /// </summary>
        protected ITransitionService TransitionService { get; }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected ModalPresenterBase(TModal view, ITransitionService transitionService) : base(view) {
            TransitionService = transitionService;
        }

    }
}
