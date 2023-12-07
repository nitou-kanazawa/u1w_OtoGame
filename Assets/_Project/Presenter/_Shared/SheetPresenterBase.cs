using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {

    public abstract class SheetPresenterBase<TSheet, TRootView, TRootViewState>
        : SheetPresenter<TSheet, TRootView, TRootViewState>
        where TSheet : Sheet<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        /// <summary>
        /// UI画面遷移サービス
        /// </summary>
        protected ITransitionService TransitionService { get; }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected SheetPresenterBase(TSheet view, ITransitionService transitionService) : base(view) {
            TransitionService = transitionService;
        }

    }
}
