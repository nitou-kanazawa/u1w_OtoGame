using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Modal;
using Sirenix.OdinInspector;

namespace nitou.UI {

    /// <summary>
    /// UnityScreenNavigatorコンテナの管理クラス
    /// </summary>
    public partial class ScreenContainer : MonoBehaviour {

        /// ----------------------------------------------------------------------------
        #region Inner Class

        /// <summary>
        /// アクティブなScreenのタイプ
        /// </summary>
        public enum ScreenType {
            Page,
            Modal,
            None,
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // Field

        [Title("Container")]
        [SerializeField] private PageContainer _pageContainer;
        [SerializeField] private ModalContainer _modalContainer;

        [Title("Overlay")]
        [SerializeField] private OverlayScreen _overlay;


        /// ----------------------------------------------------------------------------
        // Properity

        public PageContainer PageContainer => _pageContainer;
        public ModalContainer ModalContainer => _modalContainer;
        public OverlayScreen Overlay => _overlay;


        [FoldoutGroup("State info")]
        [ShowInInspector, ReadOnly]
        public ScreenType CurrentMode { get; set; } = ScreenType.None;


        /// <summary>
        /// Pushされているページ数
        /// </summary>
        [FoldoutGroup("State info")]
        [ShowInInspector, ReadOnly]
        public int PageCount => (_pageContainer != null) ? _pageContainer.Pages.Count : 0;

        /// <summary>
        /// Pushされているモーダル数
        /// </summary>
        [FoldoutGroup("State info")]
        [ShowInInspector, ReadOnly]
        public int ModalCount => (_modalContainer != null) ? _modalContainer.Modals.Count : 0;


        public Page PreviousPage { get; private set; }
        public Page ActivePage { get; private set; }

        public Modal PreviousModal { get; private set; }
        public Modal ActiveModal { get; private set; }


        /// <summary>
        /// 画面遷移中かどうか
        /// </summary>
        public bool IsInTransition => _pageContainer.IsInTransition || _modalContainer.IsInTransition;


        /// ----------------------------------------------------------------------------

        private void Awake() {
            Initialize();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize() {
            CurrentMode = ScreenType.None;

            // PageContainer
            var pageObserver = _pageContainer.gameObject.AddComponent<PageContainerObserver>();
            pageObserver.Context = this;
            _pageContainer.AddCallbackReceiver(pageObserver);

            // ModalContainer
            var modalObserver = _modalContainer.gameObject.AddComponent<ModalContainerObserver>();
            modalObserver.Context = this;
            _modalContainer.AddCallbackReceiver(modalObserver);
        }


        /// ----------------------------------------------------------------------------



        public void Pop(bool playAnimation) {
            // 遷移中の場合，エラーを投げる
            if (IsInTransition)
                throw new InvalidOperationException("遷移中はPage/ModalをPop遷移を行えません．");

            if (_modalContainer.Modals.Count >= 1) {
                _modalContainer.Pop(playAnimation);

            } else if (_pageContainer.Pages.Count >= 1) {
                _pageContainer.Pop(playAnimation);

            } else {
                throw new InvalidOperationException("コンテナにPage/Modalが存在しないため，Pop遷移を行えません.");
            }
        }


        /// ----------------------------------------------------------------------------

        private void UpdateStateInfo() {

            // 
            if (ModalCount > 1) {
                CurrentMode = ScreenType.Modal;
            }
            // 
            else if (PageCount > 1) {
                CurrentMode = ScreenType.Page;
            }
            // 
            else {
                CurrentMode = ScreenType.None;
            }
        }


        public void DebugLog() {
            Debug_.Log($"Page : {PageCount},  Modal {ModalCount}");

            Debug_.ListLog(_pageContainer.OrderedPagesIds);
            foreach (var id in _pageContainer.OrderedPagesIds) {
                Debug_.Log(_pageContainer.Pages[id].name);
            }
        }

        /// ----------------------------------------------------------------------------



    }

}













/*
         /// <summary>
        /// ページをPushする
        /// </summary>
        public async UniTask PushPage<TPage>(
            string resourceKey, bool playAnimation,
            bool stack = true, string pageId = null, bool loadAsync = true,
            Action<(string pageId, TPage page)> onLoad = null)
            where TPage : Page {

            if (CurrentMode == ScreenType.Modal) {
                throw new InvalidOperationException("Modalが");
            }

            // ページの追加
            await _pageContainer.Push<TPage>(resourceKey, playAnimation, stack, pageId, loadAsync,
                onLoad: x => {
                    Debug_.Log("[Screen Container] onLoad", Colors.Orange);
                    if (onLoad != null)
                        onLoad.Invoke(x);
                }
            );
            Debug_.Log("[Screen Container] await push", Colors.Orange);


        }

        /// <summary>
        /// モーダルをPushする
        /// </summary>
        public void PushModal() {

        }

        public void PopPage() {

        }

        public void PopModal() {

        }
 */