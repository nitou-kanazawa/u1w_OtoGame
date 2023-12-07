using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;

namespace nitou.UI {
    public partial class ScreenContainer {

        /// <summary>
        /// ページの表示状態を監視するコンポーネント
        /// </summary>
        [RequireComponent(typeof(PageContainer))]
        public class PageContainerObserver : MonoBehaviour, IPageContainerCallbackReceiver {

            /// <summary>
            /// 管理オブジェクト
            /// </summary>
            public ScreenContainer Context { get; set; }

            private readonly Color color = Colors.Aquamarine;


            /// ----------------------------------------------------------------------------
            // Interface Method (Push遷移)

            void IPageContainerCallbackReceiver.BeforePush(Page enterPage, Page exitPage) {
                Debug_.Log("Before Push", color);
            }

            async void IPageContainerCallbackReceiver.AfterPush(Page enterPage, Page exitPage) {
                Debug_.Log("After Push", color);

                // 状態の更新
                Context.ActivePage = enterPage;
                Context.PreviousPage = exitPage;
                Context.UpdateStateInfo();

                // ページ要素の選択
                if (enterPage is ISelectableContainer selectablePgae) {
                    Debug_.Log("Auto select", color);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(selectablePgae.Delay));

                    UISelectionManager.Instance.RegisterGroup(selectablePgae.SelectableGroup);
                    UISelectionManager.Instance.Activate(UISelectionManager.ScreenType.Page);
                }

            }


            /// ----------------------------------------------------------------------------
            // Interface Method (Pop遷移)

            void IPageContainerCallbackReceiver.BeforePop(Page enterPage, Page exitPage) {
                Debug_.Log("Before Pop", color);

                UISelectionManager.Instance.UnregisterGroup();
            }

            async void IPageContainerCallbackReceiver.AfterPop(Page enterPage, Page exitPage) {
                Debug_.Log("After Pop", color);

                // 状態の更新
                Context.ActivePage = enterPage;
                Context.PreviousPage = exitPage;
                Context.UpdateStateInfo();

                // ページ要素の選択
                if (enterPage is ISelectableContainer selectablePgae) {
                    Debug_.Log("Auto select", color);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(selectablePgae.Delay));

                    UISelectionManager.Instance.RegisterGroup(selectablePgae.SelectableGroup);
                    UISelectionManager.Instance.Activate(UISelectionManager.ScreenType.Page);
                }
            }

        }

    }
}