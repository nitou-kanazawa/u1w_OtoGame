using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using nitou.UI;

namespace OtoGame.Presentation {
    using OtoGame.View;

    public sealed class CreditModalPresenter : ModalPresenterBase<CreditModal, CreditView, CreditViewState> {

        /// コンストラクタ
        public CreditModalPresenter(CreditModal view, ITransitionService transitionService) 
            : base(view, transitionService) {
        }

        /// <summary>
        /// ページがロードされた直後の処理
        /// </summary>
        protected override Task ViewDidLoad(CreditModal view, CreditViewState viewState) {

            // 選択候補UI
            view.SelectableGroup = new SelectableGroup(view.root.backButton);

            // バインド
            view.root.backButton
                .SetOnClickDestination(TransitionService.PopCommandExecuted)
                .AddTo(this);

            return Task.CompletedTask;
        }
    }
}
