using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;


namespace nitou.UI {
    public partial class SelectableGroup {

        /// <summary>
        /// "Selectable"に自動アタッチされる操作用コンポーネント
        /// </summary>
        public class SelectionHooker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler {

            // 親オブジェクト
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
            /// グループ登録
            /// </summary>
            internal void Register(SelectableGroup group) {
                Group = group ?? throw new System.ArgumentNullException(nameof(group));
                enabled = true;
            }

            /// <summary>
            /// グループ登録解除
            /// </summary>
            internal void Unegister() {
                Group = null;
                enabled = false;
            }
        }

    }
}