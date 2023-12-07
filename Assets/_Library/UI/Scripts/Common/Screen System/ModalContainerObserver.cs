using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace nitou.UI {
    public partial class ScreenContainer {

        /// <summary>
        /// モーダルの表示状態を監視するコンポーネント
        /// </summary>
        [RequireComponent(typeof(ModalContainer))]
        public class ModalContainerObserver : MonoBehaviour, IModalContainerCallbackReceiver {

            /// <summary>
            /// 管理オブジェクト
            /// </summary>
            public ScreenContainer Context { get; set; }

            private readonly Color color = Colors.Aquamarine;


            /// ----------------------------------------------------------------------------
            // Interface Method (Push遷移)

            void IModalContainerCallbackReceiver.BeforePush(Modal enterModal, Modal exitModal) {
                Debug_.Log("Before Push", color);

            }

            async void IModalContainerCallbackReceiver.AfterPush(Modal enterModal, Modal exitModal) {
                Debug_.Log("After Push", color);

                // 状態の更新
                Context.ActiveModal = enterModal;
                Context.PreviousModal = exitModal;
                Context.UpdateStateInfo();

                // モーダル要素の選択
                if (enterModal is ISelectableContainer selectableModal) {
                    Debug_.Log("Auto select", color);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(selectableModal.Delay));

                    UISelectionManager.Instance.RegisterGroup(selectableModal.SelectableGroup, UISelectionManager.ScreenType.Modal);
                    UISelectionManager.Instance.Activate(UISelectionManager.ScreenType.Modal);
                }
            }


            /// ----------------------------------------------------------------------------
            // Interface Method (Pop遷移)

            void IModalContainerCallbackReceiver.BeforePop(Modal enterModal, Modal exitModal) {

                // Modalが全て非表示なる場合，
                if (enterModal == null) {
                    UISelectionManager.Instance.Deactivate();
                    UISelectionManager.Instance.Activate(UISelectionManager.ScreenType.Page);
                }
            }

            async void IModalContainerCallbackReceiver.AfterPop(Modal enterModal, Modal exitModal) {
                Debug_.Log("After Push", color);

                // 状態の更新
                Context.ActiveModal = enterModal;
                Context.PreviousModal = exitModal;
                Context.UpdateStateInfo();

                // モーダル要素の選択
                if (enterModal is ISelectableContainer selectableModal) {
                    Debug_.Log("Auto select", color);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(selectableModal.Delay));

                    UISelectionManager.Instance.RegisterGroup(selectableModal.SelectableGroup, UISelectionManager.ScreenType.Modal);
                    UISelectionManager.Instance.Activate(UISelectionManager.ScreenType.Modal);
                }
            }
        }

    }
}