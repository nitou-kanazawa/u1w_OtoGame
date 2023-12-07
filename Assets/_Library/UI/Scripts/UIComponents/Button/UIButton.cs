using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using Sirenix.OdinInspector;


namespace nitou.UI.Component {

    /// <summary>
    /// 基本機能のみの独自ボタンUI
    /// </summary>
    public class UIButton : Selectable, IUIComponent
        , ISubmitHandler {

        /// <summary>
        /// クリックされた時のイベント通知
        /// </summary>
        public event System.Action OnClicked = delegate { };

        /// <summary>
        /// 選択された時のイベント通知
        /// </summary>
        public event System.Action OnSelected = delegate { };

        /// <summary>
        /// 非選択された時のイベント通知
        /// </summary>
        public event System.Action OnDeselected = delegate { };



        /// ----------------------------------------------------------------------------
        // Public Method



        /// ----------------------------------------------------------------------------
        // Interface Method

        /// <summary>
        /// 決定入力されたときの処理
        /// </summary>
        public void OnSubmit(BaseEventData eventData) {
            OnClicked.Invoke();
        }

        /// <summary>
        /// クリックされたときの処理
        /// </summary>
        public override void OnPointerDown(PointerEventData eventData) {
            base.OnPointerDown(eventData);
            OnClicked.Invoke();
        }

        /// <summary>
        /// 選択されたときの処理
        /// </summary>
        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);
            OnSelected.Invoke();
        }

        /// <summary>
        /// 選択されたときの処理
        /// </summary>
        public override void OnDeselect(BaseEventData eventData) {
            base.OnDeselect(eventData);
            OnDeselected.Invoke();
        }
    }

}