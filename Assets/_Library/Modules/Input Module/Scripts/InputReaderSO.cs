using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// [�Q�l]
//  Unity Forums ScriptableObject behaviour discussion (how Scriptable Objects work) https://forum.unity.com/threads/scriptableobject-behaviour-discussion-how-scriptable-objects-work.541212/

namespace nitou.Input {

    /// <summary>
    /// ActionMap�̎��
    /// ��Asset�ɑΉ�������
    /// </summary>
    public enum InputTarget {
        Player,
        UI,
        None,
    }

    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/Input/Test Input Reader")]
    public class InputReaderSO : ScriptableObject, InputProvider.IPlayerActions, InputProvider.IUIActions, System.IDisposable {

        /// <summary>
        /// Asset���琶���������̓N���X
        /// </summary>
        private InputProvider _provider;

        /// <summary>
        /// Action Map�̎擾
        /// </summary>
        public InputActionMap GetActionMap(InputTarget actionMap) =>
            actionMap switch {
                InputTarget.Player => _provider.Player,
                InputTarget.UI => _provider.UI,
                _ => throw new System.NotImplementedException(),
            };

        /// <summary>
        /// ���͂̑���Ώ�
        /// </summary>
        public InputTarget CurrentTarget {
            get => _currentTarget;
            private set {
                if (_currentTarget == value) return;

                _currentTarget = value;
                OnTargetChanged?.Invoke(_currentTarget);
            }
        }
        private InputTarget _currentTarget = InputTarget.None;

        /// <summary>
        /// �Ώې؂�ւ����̃C�x���g�ʒm
        /// </summary>
        public System.Action<InputTarget> OnTargetChanged;


        /// ----------------------------------------------------------------------------
        // Event

        public event UnityAction SubmitEvent = delegate { };
        public event UnityAction<Vector3> MoveEvent = delegate { };
        public event UnityAction AttackEvent = delegate { };
        public event UnityAction JumpEvent = delegate { };
        public event UnityAction PauseEvent = delegate { };
        public event UnityAction OpenInventoryEvent = delegate { };
        public event UnityAction<Vector2> CameraMoveEvent = delegate { };
        //public event UnityAction<Vector2> CameraZoomEvent = delegate { };

        public event UnityAction UISubmitEvent = delegate { };
        public event UnityAction UICancelEvent = delegate { };
        public event UnityAction UIMoveNextTabEvent = delegate { };
        public event UnityAction UIMovePreviousTabEvent = delegate { };


        /// ----------------------------------------------------------------------------
        // Read Method

        public Vector3 ReadMoveValue() {
            var vec2 = _provider.Player.Move.ReadValue<Vector2>();
            return new Vector3(vec2.x, 0, vec2.y);
        }

        public bool ReadSubmitPressedValue() {
            return _provider.Player.Submit.WasPressedThisFrame();
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// ����������
        /// </summary>
        public void Initialize() {
            Debug.Log("[Test Input Reader] Initialize");
            _provider = new InputProvider();

            // �R�[���o�b�N�̓o�^
            _provider.Player.SetCallbacks(this);
            _provider.UI.SetCallbacks(this);

            DisableAllInput();
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
            if (_provider == null) return;

            Debug.Log("[Test Input Reader] Dispose");

            _provider.Dispose();
            _provider = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (�A�N�e�B�u�ݒ�)

        public void EnablePlayerInput() {
            _provider.Player.Enable();
            _provider.UI.Disable();

            CurrentTarget = InputTarget.Player;
        }

        public void EnableUIInput() {
            _provider.Player.Disable();
            _provider.UI.Enable();

            CurrentTarget = InputTarget.UI;
        }

        public void DisableAllInput() {
            _provider.Player.Disable();
            _provider.UI.Disable();

            CurrentTarget = InputTarget.None;
        }


        /// ----------------------------------------------------------------------------
        // Interface Method

        /// <summary>
        /// ����
        /// </summary>
        public void OnSubmit(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                SubmitEvent.Invoke();
            }
        }

        /// <summary>
        /// �ړ�
        /// </summary>
        void InputProvider.IPlayerActions.OnMove(InputAction.CallbackContext context) {
            var vec2 = context.ReadValue<Vector2>();
            MoveEvent.Invoke(new Vector3(vec2.x, 0, vec2.y));
        }

        /// <summary>
        /// �W�����v
        /// </summary>
        void InputProvider.IPlayerActions.OnJump(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// ����
        /// </summary>
        void InputProvider.IPlayerActions.OnRun(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// �U��
        /// </summary>
        void InputProvider.IPlayerActions.OnAttack(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                AttackEvent.Invoke();
            }
        }

        /// <summary>
        /// �J��������
        /// </summary>
        /// <param name="context"></param>
        void InputProvider.IPlayerActions.OnCameraXY(InputAction.CallbackContext context) {
            CameraMoveEvent.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// ���_�J�[�\������
        /// </summary>
        void InputProvider.IPlayerActions.OnMoveCursor(InputAction.CallbackContext context) {

        }

        /// <summary>
        /// �|�[�YUI
        /// </summary>
        /// <param name="context"></param>
        void InputProvider.IPlayerActions.OnPause(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                PauseEvent.Invoke();
            }
        }

        /// <summary>
        /// �C���x���g��UI
        /// </summary>
        /// <param name="context"></param>
        void InputProvider.IPlayerActions.OnOpenInventory(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                OpenInventoryEvent.Invoke();
            }
        }



        /// ----------------------------------------------------------------------------
        // Interface Method

        /// <summary>
        /// �i�r�Q�[�V����
        /// </summary>
        void InputProvider.IUIActions.OnNavigate(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// ����
        /// </summary>
        void InputProvider.IUIActions.OnSubmit(InputAction.CallbackContext context) {
            UISubmitEvent.Invoke();
        }

        /// <summary>
        /// �L�����Z��
        /// </summary>
        void InputProvider.IUIActions.OnCancel(InputAction.CallbackContext context) {
            UICancelEvent.Invoke();
        }

        /// <summary>
        /// �\�[�g
        /// </summary>
        void InputProvider.IUIActions.OnSort(InputAction.CallbackContext context) {

        }

        // 

        /// <summary>
        /// �}�E�X�N���b�N
        /// </summary>
        void InputProvider.IUIActions.OnClick(InputAction.CallbackContext context) {

        }

        /// <summary>
        /// �}�E�X���N���b�N
        /// </summary>
        void InputProvider.IUIActions.OnMiddleClick(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// �}�E�X���W
        /// </summary>
        void InputProvider.IUIActions.OnPoint(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// �}�E�X�E�N���b�N
        /// </summary>
        void InputProvider.IUIActions.OnRightClick(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// �}�E�X�z�C�[���X�N���[��
        /// </summary>
        void InputProvider.IUIActions.OnScrollWheel(InputAction.CallbackContext context) {
        }

        // 

        /// <summary>
        /// �^�u�؂�ւ��i���j
        /// </summary>
        void InputProvider.IUIActions.OnMoveNextTab(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                UIMoveNextTabEvent.Invoke();
            }
        }

        /// <summary>
        /// �^�u�؂�ւ��i�O�j
        /// </summary>
        void InputProvider.IUIActions.OnMovePreviousTab(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                UIMovePreviousTabEvent.Invoke();
            }
        }

        // 

        /// <summary>
        /// ���f����]
        /// </summary>
        void InputProvider.IUIActions.OnRotateModel(InputAction.CallbackContext context) {
        }


    }

}