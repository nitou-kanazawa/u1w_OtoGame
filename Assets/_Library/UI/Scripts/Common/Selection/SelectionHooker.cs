using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;


namespace nitou.UI {
    public partial class SelectableGroup {

        /// <summary>
        /// "Selectable"�Ɏ����A�^�b�`����鑀��p�R���|�[�l���g
        /// </summary>
        public class SelectionHooker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler {

            // �e�I�u�W�F�N�g
            [ShowInInspector, ReadOnly]
            public SelectableGroup Group { get; private set; }


            /// ----------------------------------------------------------------------------
            // Interface Method

            public void OnPointerEnter(PointerEventData eventData) {
                Group?.NotifyPointerEnter(this.gameObject);
            }

            public void OnPointerExit(PointerEventData eventData) {
                Group?.NotifyPointerExit(this.gameObject);
            }

            public void OnSelect(BaseEventData eventData) {
                Group?.NotifySelected(this.gameObject);
            }


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// �O���[�v�o�^
            /// </summary>
            internal void Register(SelectableGroup group) {
                Group = group ?? throw new System.ArgumentNullException(nameof(group));
                enabled = true;
            }

            /// <summary>
            /// �O���[�v�o�^����
            /// </summary>
            internal void Unegister() {
                Group = null;
                enabled = false;
            }
        }

    }
}