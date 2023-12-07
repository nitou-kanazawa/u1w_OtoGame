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
        /// �R���X�g���N�^
        /// </summary>
        public MenuTopPagePresenter(MenuTopPage view, ITransitionService transitionService)
            : base(view, transitionService) {
        }


        /// ----------------------------------------------------------------------------
        // Protected Method        

        /// <summary>
        /// �y�[�W�����[�h���ꂽ����ɌĂ΂�鏈��
        /// </summary>
        protected override Task ViewDidLoad(MenuTopPage view, MenuTopViewState viewState) {

            // �I�����UI
            view.SelectableGroup = new SelectableGroup(
                new List<Selectable>() {
                    view.root.playButton,
                    view.root.optionButton,
                    view.root.infomationButton,
                    view.root.backButton
                }
            );

            // �J�ڃC�x���g�̃o�C���h
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
