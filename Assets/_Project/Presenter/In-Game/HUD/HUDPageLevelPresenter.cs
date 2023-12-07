using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {
    using OtoGame.Model;
    using OtoGame.View;
    using OtoGame.LevelObjects;

    public class HUDPageLevelPresenter : PagePresenter<HUDPage> {

        // モデル
        private ResultData _model;

        // シーン参照
        private readonly StageLevelReference _levelReference;

        private IntReactiveProperty _showNum = new(0);
        CompositeDisposable disposables;

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HUDPageLevelPresenter(HUDPage page, StageLevelReference levelReference, ResultData resultData) : base(page) {
            if (levelReference == null) {
                throw new ArgumentNullException(nameof(levelReference));
            }
            this._levelReference = levelReference;
            this._model = resultData;
        }


        /// <summary>
        /// ページがロードされた直後に呼ばれる処理
        /// </summary>
        protected override Task ViewDidLoad(HUDPage view) {


            // 観客
            disposables = new CompositeDisposable();
            _model.Score.Subscribe(x => _showNum.Value = x/10000).AddTo(disposables);
            _showNum.Subscribe(x => _levelReference.ThardParty.ShowBoat(x)).AddTo(disposables);

            return base.ViewDidLoad(view);
        }

        protected override Task ViewWillDestroy(HUDPage view) {
            disposables.Dispose();
            return base.ViewWillDestroy(view);
        }

        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// Push遷移で表示される直前の処理
        /// </summary>
        protected async override Task ViewWillPushEnter(HUDPage view) {

            await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));

            await _levelReference.HideFrontWater();

        }

        /// <summary>
        /// Push遷移で非表示になる直前の処理
        /// </summary>
        protected async override Task ViewWillPushExit(HUDPage view) {
        }

    }

}