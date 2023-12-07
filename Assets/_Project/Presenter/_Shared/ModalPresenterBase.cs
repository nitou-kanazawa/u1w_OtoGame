using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {

    public abstract class ModalPresenterBase<TModal, TRootView, TRootViewState>
        : ModalPresenter<TModal, TRootView, TRootViewState>
        where TModal : Modal<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        /// <summary>
        /// UI��ʑJ�ڃT�[�r�X
        /// </summary>
        protected ITransitionService TransitionService { get; }
        
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        protected ModalPresenterBase(TModal view, ITransitionService transitionService) : base(view) {
            TransitionService = transitionService;
        }

    }
}
