using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using nitou.UI;

namespace OtoGame.Presentation {
    using OtoGame.Model;
    using OtoGame.View;
    using OtoGame.LevelObjects;

    public class HUDPagePresenter : PagePresenterBase<HUDPage, HUDView, HUDViewState> {

        // Model
        private ResultData _model;
        private AudioManager _audioManager;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HUDPagePresenter(HUDPage view, ITransitionService transitionService, ResultData resultData, AudioManager audioManager)
            : base(view, transitionService) {

            _model = resultData;
            _audioManager = audioManager;
        }


        /// ----------------------------------------------------------------------------
        // Protected Method        

        /// <summary>
        /// ページがロードされた直後に呼ばれる処理
        /// </summary>
        protected override Task ViewDidLoad(HUDPage view, HUDViewState viewState) {

            // スコア
            _model.Score.Subscribe(x => view.root.ScoreText.SetValue(x)).AddTo(this);
            
            // カウント
            _model.GreatCount.Subscribe(x => view.root.GreateCounter.SetValue(x)).AddTo(this);
            _model.GoodCount.Subscribe(x => view.root.GoodCounter.SetValue(x)).AddTo(this);
            _model.BadCount.Subscribe(x => view.root.BadCounter.SetValue(x)).AddTo(this);

            // コンボ
            _model.Combo.Subscribe(x => view.root.ComboText.SetValue(x)).AddTo(this);

            // タイム
            _audioManager.Time.Subscribe(x => view.root.TimeSlider.SetValue(x,_audioManager.MaxTime)).AddTo(this);

            return Task.CompletedTask;
        }

    }

}