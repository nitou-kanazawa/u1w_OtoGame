using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using nitou;

namespace OtoGame.Presentation {
    using OtoGame.View;

    public sealed class LoadingPagePresenter : PagePresenterBase<LoadingPage, LoadingView, LoadingViewState> {

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LoadingPagePresenter(LoadingPage view, ITransitionService transitionService)
            : base(view, transitionService) {
        }


        /// ----------------------------------------------------------------------------
        // Protected Method        


        /// <summary>
        /// �y�[�W�����[�h���ꂽ����ɌĂ΂�鏈��
        /// </summary>
        protected override void ViewDidPushEnter(LoadingPage view, LoadingViewState viewState) {

            // ��ʂ�Pop�J�n
            TransitionService.LoadPage_AfterPush();
        }

        /// <summary>
        /// �y�[�W��Pop�J�ڂŔ�\���Ȃ钼�O�ɌĂ΂�鏈��
        /// </summary>
        protected async override Task ViewWillPopExit(LoadingPage view, LoadingViewState viewState) {
            await TransitionService.LoadPage_BeforePop();
        }

        /// <summary>
        /// �y�[�W��Pop�J�ڂŔ�\�����ꂽ����ɌĂ΂�鏈��
        /// </summary>
        protected override void ViewDidPopExit(LoadingPage view, LoadingViewState viewState) {
            TransitionService.LoadPage_AfterPop();
        }

    }
}
