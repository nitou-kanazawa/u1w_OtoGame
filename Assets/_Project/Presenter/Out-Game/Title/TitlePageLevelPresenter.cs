using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {
    using OtoGame.View;
    using OtoGame.LevelObjects;

    public class TitlePageLevelPresenter : PagePresenter<TitlePage> {

        // シーン参照
        private readonly TitleLevelReference _levelReference;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TitlePageLevelPresenter(TitlePage page, TitleLevelReference levelReference): base(page) {
            if (levelReference == null) {
                throw new ArgumentNullException(nameof(levelReference));
            }
            this._levelReference = levelReference;
        }

        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// Push遷移で表示される直前の処理
        /// </summary>
        protected async override Task ViewWillPushEnter(TitlePage view) {

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            await _levelReference.ShowTitleText();
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));

        }

        /// <summary>
        /// Push遷移で非表示になる直前の処理
        /// </summary>
        protected async override Task ViewWillPushExit(TitlePage view) {
            await _levelReference.HideTitleText();
        }


        /// <summary>
        /// Pop遷移で表示される直前の処理
        /// </summary>
        protected async override Task ViewWillPopEnter(TitlePage view) {

            _levelReference.ShowTitleText().Forget();

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        }

        //protected override void ViewDidPopEnter(TitlePage view) {
        //    _levelReference.ShowTitleText();
        //}
    }
}