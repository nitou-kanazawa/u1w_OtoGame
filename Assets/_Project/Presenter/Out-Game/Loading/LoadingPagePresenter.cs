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
        /// コンストラクタ
        /// </summary>
        public LoadingPagePresenter(LoadingPage view, ITransitionService transitionService)
            : base(view, transitionService) {
        }


        /// ----------------------------------------------------------------------------
        // Protected Method        


        /// <summary>
        /// ページがロードされた直後に呼ばれる処理
        /// </summary>
        protected override void ViewDidPushEnter(LoadingPage view, LoadingViewState viewState) {

            // 画面のPop開始
            TransitionService.LoadPage_AfterPush();
        }

        /// <summary>
        /// ページがPop遷移で非表示なる直前に呼ばれる処理
        /// </summary>
        protected async override Task ViewWillPopExit(LoadingPage view, LoadingViewState viewState) {
            await TransitionService.LoadPage_BeforePop();
        }

        /// <summary>
        /// ページがPop遷移で非表示された直後に呼ばれる処理
        /// </summary>
        protected override void ViewDidPopExit(LoadingPage view, LoadingViewState viewState) {
            TransitionService.LoadPage_AfterPop();
        }

    }
}
