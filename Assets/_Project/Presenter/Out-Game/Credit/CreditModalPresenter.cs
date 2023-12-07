using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using nitou.UI;

namespace OtoGame.Presentation {
    using OtoGame.View;

    public sealed class CreditModalPresenter : ModalPresenterBase<CreditModal, CreditView, CreditViewState> {

        /// �R���X�g���N�^
        public CreditModalPresenter(CreditModal view, ITransitionService transitionService) 
            : base(view, transitionService) {
        }

        /// <summary>
        /// �y�[�W�����[�h���ꂽ����̏���
        /// </summary>
        protected override Task ViewDidLoad(CreditModal view, CreditViewState viewState) {

            // �I�����UI
            view.SelectableGroup = new SelectableGroup(view.root.backButton);

            // �o�C���h
            view.root.backButton
                .SetOnClickDestination(TransitionService.PopCommandExecuted)
                .AddTo(this);

            return Task.CompletedTask;
        }
    }
}
