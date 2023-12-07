using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;
using nitou;
using nitou.UI;
using nitou.UI.PresentationFramework;


namespace OtoGame.Composition {
    using OtoGame.Model;
    using OtoGame.View;
    using OtoGame.Presentation;
    using OtoGame.LevelObjects;
    using OtoGame.Foundation;

    /// <summary>
    /// UI画面遷移を管理するクラス
    /// </summary>
    public sealed class TransitionService : ITransitionService {

        // コンテナ
        public PageContainer MainPageContainer { get; private set; }
        public  ModalContainer MainModalContainer { get; private set; }

        // 
        public OverlayScreen Overlay { get; private set; }

        // シーン参照の取得用
        private readonly ILevelReferenceProvider _referenceProvider;

        // シーン切り替え
        private readonly ISceneSwitcher _sceneSwitcher;

        // Modelデータ
        private readonly IDataProvider _dataProvider;


        private readonly IGameStageManager _gameStageManager;



        /// ----------------------------------------------------------------------------
        // Public Methord (起動時)

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TransitionService(
            ScreenContainer screenContainer,
            ILevelReferenceProvider referenceProvider,
            ISceneSwitcher sceneSwitcher,
            IDataProvider dataProvider,
            IGameStageManager gameStageManager) {

            // スクリーン
            MainPageContainer = screenContainer.PageContainer;
            MainModalContainer = screenContainer.ModalContainer;
            Overlay = screenContainer.Overlay;

            _referenceProvider = referenceProvider;
            _sceneSwitcher = sceneSwitcher;
            _dataProvider = dataProvider;
            _gameStageManager = gameStageManager;
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (起動時)

        public void ApplivationStarted() {

            MainPageContainer.Push<TitlePage>(ResourceKey.UI.TitlePage, true,
                onLoad: x => {
                    var page = x.page;

                    // 基本プレゼンター
                    var presenter = new TitlePagePresenter(page, this);
                    OnPagePresenterCreated(presenter, page);

                    // 追加プレゼンター
                    if (_referenceProvider.TryGetTitleLevel(out var reference)) {
                        var additionalPresenter = new TitlePageLevelPresenter(page, reference);
                        OnPagePresenterCreated(additionalPresenter, page);
                    }
                })
                .ToUniTask();
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (タイトル画面)

        void ITransitionService.TitlePage_StartButtonClicked() {

            MainPageContainer.Push<MenuTopPage>(ResourceKey.UI.MenuTopPage, true,
                onLoad: x => {
                    var page = x.page;

                    // 基本プレゼンター
                    var presenter = new MenuTopPagePresenter(page, this);
                    OnPagePresenterCreated(presenter, page);
                })
                .ToUniTask();
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (メニュー画面)

        void ITransitionService.MenuPage_PlayButtonClicked() {
            MainPageContainer.Push<LoadingPage>(ResourceKey.UI.LoadingPage, true,
                onLoad: x => {
                    var page = x.page;
                    var presenter = new LoadingPagePresenter(page, this);
                    OnPagePresenterCreated(presenter, page);
                })
                .ToUniTask();
        }

        void ITransitionService.MenuPage_SettingsButtonClicked() {

            // モーダルの表示
            MainModalContainer.Push<SettingsModal>(ResourceKey.UI.SettingsModal, true,
                onLoad: x => {
                    var modal = x.modal;
                    var presenter = new SettingsModalPresenter(modal, this, _dataProvider.Settings);
                    OnModalPresenterCreated(presenter, modal);
                });
        }

        void ITransitionService.MenuPage_CreditButtonClicked() {

            // モーダルの表示
            MainModalContainer.Push<CreditModal>(ResourceKey.UI.CreditModal, true,
                onLoad: x => {
                    var modal = x.modal;
                    var presenter = new CreditModalPresenter(modal, this);
                    OnModalPresenterCreated(presenter, modal);
                });
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (ロード画面)


        void ITransitionService.LoadPage_AfterPush() {
            // 全ページのPop処理を開始する
            MainPageContainer.Pop(true, MainPageContainer.Pages.Count);
        }

        UniTask ITransitionService.LoadPage_BeforePop() {
            // 暗転中にシーン切り替え
            return _sceneSwitcher.SwitchToInGame();
        }

        async void ITransitionService.LoadPage_AfterPop() {

            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            await MainPageContainer.Push<HUDPage>(ResourceKey.UI.HUDPage, true,
                onLoad: x => {
                    var page = x.page;

                    void OnDidPushEnter() {
                        _gameStageManager.StartInGame();
                    }
                    page.AddLifecycleEvent(onDidPushEnter: OnDidPushEnter);

                    // 基本プレゼンター
                    var presenter = new HUDPagePresenter(page, this, _dataProvider.ResultData, _dataProvider.AudioManager);
                    OnPagePresenterCreated(presenter, page);

                    // 追加プレゼンター
                    if (_referenceProvider.TryGetStageLevel(out var reference)) {
                        var additionalPresenter = new HUDPageLevelPresenter(page, reference, _dataProvider.ResultData);
                        OnPagePresenterCreated(additionalPresenter, page);
                    }
                })
                .ToUniTask();
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (HUD画面)

        public void Push_PausePage() {
            // モーダルの表示
            MainModalContainer.Push<PauseModal>(ResourceKey.UI.PauseModal, true,
                onLoad: x => {
                    var modal = x.modal;
                    var presenter = new PauseModalPresenter(modal, this);
                    OnModalPresenterCreated(presenter, modal);
                });
        }

        public void Push_ResultPage() {
            // モーダルの表示
            MainModalContainer.Push<ResultModal>(ResourceKey.UI.ResultModal, true,
                onLoad: x => {
                    var modal = x.modal;
                    var presenter = new ResultModalPresenter(modal, this,_dataProvider.ResultData);
                    OnModalPresenterCreated(presenter, modal);
                });
        }

        /// ----------------------------------------------------------------------------
        // Public Methord (ポーズ画面)

        async void ITransitionService.PausePage_ContinueButtonClicked() {
            await MainModalContainer.Pop(true);
            _gameStageManager.UnPauseGame();
        }

        async void ITransitionService.PausePage_RestartButtonClicked() {
            await MainModalContainer.Pop(true);
            _gameStageManager.ResetartGame();
        }

        async void ITransitionService.PausePage_QuitButtonClicked() {

            // 
            await Overlay.Close(0.4f);

            // 全ページのPop処理を開始する
            var tasks = new List<UniTask>(){
                MainPageContainer.Pop(false, MainPageContainer.Pages.Count).ToUniTask(),
                MainModalContainer.Pop(false,MainModalContainer.Modals.Count).ToUniTask(),
                _sceneSwitcher.SwitchToOutGame(),
            };
            await UniTask.WhenAll(tasks);

            //
            await Overlay.Open(0.4f);
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (リザルト画面)

        async void ITransitionService.ResultPage_QuitButtonCliked() {
            // 
            await Overlay.Close(0.4f);

            // 全ページのPop処理を開始する
            var tasks = new List<UniTask>(){
                MainPageContainer.Pop(false, MainPageContainer.Pages.Count).ToUniTask(),
                MainModalContainer.Pop(false,MainModalContainer.Modals.Count).ToUniTask(),
                _sceneSwitcher.SwitchToOutGame(),
            };
            await UniTask.WhenAll(tasks);

            //
            await Overlay.Open(0.4f);
        }

        void ITransitionService.ResultPage_TweenButtonCliked() {
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (画面遷移に関するイベント)

        /// <summary>
        /// Pageプレゼンターのセットアップ
        /// </summary>
        private IPagePresenter OnPagePresenterCreated(IPagePresenter presenter, Page page, bool shouldInitialize = true) {
            if (shouldInitialize) {
                ((IPresenter)presenter).Initialize();
                presenter.AddTo(page.gameObject);
            }
            return presenter;
        }

        /// <summary>
        /// Modalプレゼンターのセットアップ
        /// </summary>
        private IModalPresenter OnModalPresenterCreated(IModalPresenter presenter, Modal modal, bool shouldInitialize = true) {
            if (shouldInitialize) {
                ((IPresenter)presenter).Initialize();
                presenter.AddTo(modal.gameObject);
            }
            return presenter;
        }





        /// ----------------------------------------------------------------------------
        // Public Methord

        /// <summary>
        /// 
        /// </summary>
        public void PopCommandExecuted() {
            // 遷移中ならエラーを投げる
            if (MainModalContainer.IsInTransition || MainPageContainer.IsInTransition)
                throw new InvalidOperationException("Cannot pop page or modal while in transition.");

            if (MainModalContainer.Modals.Count >= 1) {
                MainModalContainer.Pop(true).ToUniTask();

            } else if (MainPageContainer.Pages.Count >= 1) {
                MainPageContainer.Pop(true).ToUniTask();

            } else {
                throw new InvalidOperationException("Cannot pop page or modal because there is no page or modal.");
            }

        }

  
    }

}
