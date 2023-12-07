using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;
using UniRx;
using nitou.UI;

namespace OtoGame.Presentation {
    using OtoGame.Model;
    using OtoGame.View;

    public class PauseModalPresenter : ModalPresenterBase<PauseModal, PauseView, PauseViewState> {

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public PauseModalPresenter(PauseModal view, ITransitionService transitionService)
            : base(view, transitionService) {
        }


        /// ----------------------------------------------------------------------------
        // Protected Method    

        /// <summary>
        /// ���[�_�������[�h���ꂽ����ɌĂ΂�鏈��
        /// </summary>
        protected override Task ViewDidLoad(PauseModal view, PauseViewState viewState) {

            // �I�����UI
            view.SelectableGroup = new SelectableGroup(
                new List<Selectable>() {
                    view.root.ContinueButton,
                    view.root.RestartButton,
                    view.root.QuitButton,
                }
            );

            // �J�ڃC�x���g�̃o�C���h
            view.root.ContinueButton
                .SetOnClickDestination(TransitionService.PausePage_ContinueButtonClicked)
                .AddTo(this);
            view.root.RestartButton
                .SetOnClickDestination(TransitionService.PausePage_RestartButtonClicked)
                .AddTo(this);
            view.root.QuitButton
                .SetOnClickDestination(TransitionService.PausePage_QuitButtonClicked)
                .AddTo(this);

            return Task.CompletedTask;
        }
    }

}