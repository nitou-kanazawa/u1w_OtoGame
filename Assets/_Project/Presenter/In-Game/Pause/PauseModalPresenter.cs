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
        /// コンストラクタ
        /// </summary>
        public PauseModalPresenter(PauseModal view, ITransitionService transitionService)
            : base(view, transitionService) {
        }


        /// ----------------------------------------------------------------------------
        // Protected Method    

        /// <summary>
        /// モーダルがロードされた直後に呼ばれる処理
        /// </summary>
        protected override Task ViewDidLoad(PauseModal view, PauseViewState viewState) {

            // 選択候補UI
            view.SelectableGroup = new SelectableGroup(
                new List<Selectable>() {
                    view.root.ContinueButton,
                    view.root.RestartButton,
                    view.root.QuitButton,
                }
            );

            // 遷移イベントのバインド
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