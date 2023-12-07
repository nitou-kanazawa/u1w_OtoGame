using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace nitou.UI {
    public partial class ScreenContainer {

        /// <summary>
        /// ���[�_���̕\����Ԃ��Ď�����R���|�[�l���g
        /// </summary>
        [RequireComponent(typeof(ModalContainer))]
        public class ModalContainerObserver : MonoBehaviour, IModalContainerCallbackReceiver {

            /// <summary>
            /// �Ǘ��I�u�W�F�N�g
            /// </summary>
            public ScreenContainer Context { get; set; }

            private readonly Color color = Colors.Aquamarine;


            /// ----------------------------------------------------------------------------
            // Interface Method (Push�J��)

            void IModalContainerCallbackReceiver.BeforePush(Modal enterModal, Modal exitModal) {
                Debug_.Log("Before Push", color);

            }

            async void IModalContainerCallbackReceiver.AfterPush(Modal enterModal, Modal exitModal) {
                Debug_.Log("After Push", color);

                // ��Ԃ̍X�V
                Context.ActiveModal = enterModal;
                Context.PreviousModal = exitModal;
                Context.UpdateStateInfo();

                // ���[�_���v�f�̑I��
                if (enterModal is ISelectableContainer selectableModal) {
                    Debug_.Log("Auto select", color);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(selectableModal.Delay));

                    UISelectionManager.Instance.RegisterGroup(selectableModal.SelectableGroup, UISelectionManager.ScreenType.Modal);
                    UISelectionManager.Instance.Activate(UISelectionManager.ScreenType.Modal);
                }
            }


            /// ----------------------------------------------------------------------------
            // Interface Method (Pop�J��)

            void IModalContainerCallbackReceiver.BeforePop(Modal enterModal, Modal exitModal) {

                // Modal���S�Ĕ�\���Ȃ�ꍇ�C
                if (enterModal == null) {
                    UISelectionManager.Instance.Deactivate();
                    UISelectionManager.Instance.Activate(UISelectionManager.ScreenType.Page);
                }
            }

            async void IModalContainerCallbackReceiver.AfterPop(Modal enterModal, Modal exitModal) {
                Debug_.Log("After Push", color);

                // ��Ԃ̍X�V
                Context.ActiveModal = enterModal;
                Context.PreviousModal = exitModal;
                Context.UpdateStateInfo();

                // ���[�_���v�f�̑I��
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