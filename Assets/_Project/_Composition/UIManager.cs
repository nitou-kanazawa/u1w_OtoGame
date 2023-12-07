using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;
using Sirenix.OdinInspector;
using nitou;
using nitou.UI.PresentationFramework;


namespace OtoGame.Composition {
    using OtoGame.Model;
    using OtoGame.View;
    using OtoGame.Presentation;
    using OtoGame.Foundation;

    public partial class UIManager : MonoBehaviour {

        [Title("Screen Container")]
        [SerializeField] private PageContainer _mainPageContainer;
        [SerializeField] private ModalContainer _mainModalContainer;



        private TransitionService _transitionService;

        //public UniTask PushTitlePage() {

        //}

        /// ----------------------------------------------------------------------------
        // Public Methord

        public void Initialize(TransitionService transitionService) {
            _transitionService = transitionService;

            var obj = _mainModalContainer.gameObject.AddComponent<PageContainerEvent>();
            obj.Setup(this);
        }

        /// <summary>
        /// タイトル画面
        /// </summary>
        public UniTask Push_TitlePage() {

            return _mainPageContainer.Push<TitlePage>(ResourceKey.UI.TitlePage, true,
                onLoad: x => {
                    var page = x.page;
                    var presenter = new TitlePagePresenter(page, _transitionService);
                    OnPagePresenterCreated(presenter, page);

                    //var pre = new TitlePageLevelPresenter(page);
                    //OnPagePresenterCreated(pre, page);
                })
                .ToUniTask();
        }

        /// <summary>
        /// メニュー画面
        /// </summary>
        public UniTask Push_MenuPage() {

            return _mainPageContainer.Push<MenuTopPage>(ResourceKey.UI.MenuTopPage, true,
                onLoad: x => {
                    var page = x.page;
                    var presenter = new MenuTopPagePresenter(page, _transitionService);
                    OnPagePresenterCreated(presenter, page);
                })
                .ToUniTask();
        }

        /// <summary>
        /// 画面をポップする
        /// </summary>
        public UniTask PopCommandExecuted() {

            // 遷移中ならエラーを投げる
            if (_mainModalContainer.IsInTransition || _mainPageContainer.IsInTransition)
                throw new InvalidOperationException("Cannot pop page or modal while in transition.");

            if (_mainModalContainer.Modals.Count >= 1) {
                return _mainModalContainer.Pop(true).ToUniTask();

            } else if (_mainPageContainer.Pages.Count >= 1) {
                return _mainPageContainer.Pop(true).ToUniTask();

            } else {
                throw new InvalidOperationException("Cannot pop page or modal because there is no page or modal.");
            }
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




        private class PageContainerEvent : MonoBehaviour, IPageContainerCallbackReceiver {
            
            private UIManager Context { get; set; }

            public void Setup(UIManager uiManager) {
                Context = uiManager;
            }

            void IPageContainerCallbackReceiver.BeforePop(Page enterPage, Page exitPage) {
                Debug.Log("Before pop");
            }

            void IPageContainerCallbackReceiver.AfterPop(Page enterPage, Page exitPage) {
                Debug.Log("After pop");
            }

            void IPageContainerCallbackReceiver.BeforePush(Page enterPage, Page exitPage) {
                Debug.Log("Before push");
            }

            void IPageContainerCallbackReceiver.AfterPush(Page enterPage, Page exitPage) {
                Debug.Log("After push");
            }


        }
    }

}