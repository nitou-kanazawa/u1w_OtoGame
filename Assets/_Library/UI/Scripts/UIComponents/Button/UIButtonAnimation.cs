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
    /// �Ǝ��{�^���ɃA�j���[�V�����Đ����t�b�N����R���|�[�l���g
    /// </summary>
    [RequireComponent(typeof(UIButton))]
    public class UIButtonAnimation : MonoBehaviour {

        // �Ώۂ̃{�^��
        private UIButton _button;


        [SerializeField] private Animator _animator;


        /// ----------------------------------------------------------------------------
        // Public Method

        private void OnEnable() {
            _button = GetComponent<UIButton>();
            _button.OnClicked += OnButtonClicked;
        }
                
        private void OnDisable() {
            _button.OnClicked -= OnButtonClicked;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 
        /// </summary>
        private void OnButtonClicked() {
            if (_animator != null) {
                if (_animator.enabled == false) {
                    _animator.enabled = true;
                }
                _animator.Play("Transition", 0, 0);
            }
        }

    }


    /// <summary>
    /// �J�ڃA�j���[�V�����̎��
    /// </summary>
    public enum TransitionTriggerType {
        Normal,
        Highlighted,
        Pressed,
        Selected,
        Disabled,
    }

}