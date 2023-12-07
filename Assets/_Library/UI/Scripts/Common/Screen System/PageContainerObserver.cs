using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;

namespace nitou.UI {
    public partial class ScreenContainer {

        /// <summary>
        /// �y�[�W�̕\����Ԃ��Ď�����R���|�[�l���g
        /// </summary>
        [RequireComponent(typeof(PageContainer))]
        public class PageContainerObserver : MonoBehaviour, IPageContainerCallbackReceiver {

            /// <summary>
            /// �Ǘ��I�u�W�F�N�g
            /// </summary>
            public ScreenContainer Context { get; set; }

            private readonly Color color = Colors.Aquamarine;


            /// ----------------------------------------------------------------------------
            // Interface Method (Push�J��)

            void IPageContainerCallbackReceiver.BeforePush(Page enterPage, Page exitPage) {
                Debug_.Log("Before Push", color);
            }

            async void IPageContainerCallbackReceiver.AfterPush(Page enterPage, Page exitPage) {
                Debug_.Log("After Push", color);

                // ��Ԃ̍X�V
                Context.ActivePage = enterPage;
                Context.PreviousPage = exitPage;
                Context.UpdateStateInfo();

                // �y�[�W�v�f�̑I��
                if (enterPage is ISelectableContainer selectablePgae) {
                    Debug_.Log("Auto select", color);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(selectablePgae.Delay));

                    UISelectionManager.Instance.RegisterGroup(selectablePgae.SelectableGroup);
                    UISelectionManager.Instance.Activate(UISelectionManager.ScreenType.Page);
                }

            }


            /// ----------------------------------------------------------------------------
            // Interface Method (Pop�J��)

            void IPageContainerCallbackReceiver.BeforePop(Page enterPage, Page exitPage) {
                Debug_.Log("Before Pop", color);

                UISelectionManager.Instance.UnregisterGroup();
            }

            async void IPageContainerCallbackReceiver.AfterPop(Page enterPage, Page exitPage) {
                Debug_.Log("After Pop", color);

                // ��Ԃ̍X�V
                Context.ActivePage = enterPage;
                Context.PreviousPage = exitPage;
                Context.UpdateStateInfo();

                // �y�[�W�v�f�̑I��
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