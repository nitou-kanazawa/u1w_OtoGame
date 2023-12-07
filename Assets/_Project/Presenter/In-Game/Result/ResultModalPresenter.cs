using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;
using UniRx;
using nitou.UI;

namespace OtoGame.Presentation {
    using OtoGame.Model;
    using OtoGame.View;

    public class ResultModalPresenter : ModalPresenterBase<ResultModal, ResultView, ResultViewState> {


        private ResultData _model;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ResultModalPresenter(ResultModal view, ITransitionService transitionService, ResultData resultData)
            : base(view, transitionService) {
            _model = resultData;
        }


        /// ----------------------------------------------------------------------------
        // Protected Method    

        /// <summary>
        /// ���[�_�������[�h���ꂽ����ɌĂ΂�鏈��
        /// </summary>
        protected override Task ViewDidLoad(ResultModal view, ResultViewState viewState) {

            // �I�����UI
            view.SelectableGroup = new SelectableGroup(
                new List<Selectable>() {
                    view.root.QuitButton,
                    //view.root.TweetButton,
                }
            );

            // �X�R�A�\��
            view.root.ScreText.text = $"{_model.Score.Value} <size=35>pt</size>";

            // �J�ڃC�x���g�̃o�C���h
            view.root.QuitButton
                .SetOnClickDestination(TransitionService.ResultPage_QuitButtonCliked)
                .AddTo(this);
            //view.root.TweetButton
            //    .SetOnClickDestination(() => throw new System.NotImplementedException())
            //    .AddTo(this);

            return Task.CompletedTask;
        }
    }

}