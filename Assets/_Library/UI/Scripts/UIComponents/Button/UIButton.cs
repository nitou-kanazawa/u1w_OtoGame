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
    /// ��{�@�\�݂̂̓Ǝ��{�^��UI
    /// </summary>
    public class UIButton : Selectable, IUIComponent
        , ISubmitHandler {

        /// <summary>
        /// �N���b�N���ꂽ���̃C�x���g�ʒm
        /// </summary>
        public event System.Action OnClicked = delegate { };

        /// <summary>
        /// �I�����ꂽ���̃C�x���g�ʒm
        /// </summary>
        public event System.Action OnSelected = delegate { };

        /// <summary>
        /// ��I�����ꂽ���̃C�x���g�ʒm
        /// </summary>
        public event System.Action OnDeselected = delegate { };



        /// ----------------------------------------------------------------------------
        // Public Method



        /// ----------------------------------------------------------------------------
        // Interface Method

        /// <summary>
        /// ������͂��ꂽ�Ƃ��̏���
        /// </summary>
        public void OnSubmit(BaseEventData eventData) {
            OnClicked.Invoke();
        }

        /// <summary>
        /// �N���b�N���ꂽ�Ƃ��̏���
        /// </summary>
        public override void OnPointerDown(PointerEventData eventData) {
            base.OnPointerDown(eventData);
            OnClicked.Invoke();
        }

        /// <summary>
        /// �I�����ꂽ�Ƃ��̏���
        /// </summary>
        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);
            OnSelected.Invoke();
        }

        /// <summary>
        /// �I�����ꂽ�Ƃ��̏���
        /// </summary>
        public override void OnDeselect(BaseEventData eventData) {
            base.OnDeselect(eventData);
            OnDeselected.Invoke();
        }
    }

}