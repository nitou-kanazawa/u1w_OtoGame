using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// [参考]
//  Unity Forums ScriptableObject behaviour discussion (how Scriptable Objects work) https://forum.unity.com/threads/scriptableobject-behaviour-discussion-how-scriptable-objects-work.541212/

namespace nitou.Input {

    /// <summary>
    /// ActionMapの種類
    /// ※Assetに対応させる
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
        /// Assetから生成した入力クラス
        /// </summary>
        private InputProvider _provider;

        /// <summary>
        /// Action Mapの取得
        /// </summary>
        public InputActionMap GetActionMap(InputTarget actionMap) =>
            actionMap switch {
                InputTarget.Player => _provider.Player,
                InputTarget.UI => _provider.UI,
                _ => throw new System.NotImplementedException(),
            };

        /// <summary>
        /// 入力の操作対象
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
        /// 対象切り替え時のイベント通知
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
        /// 初期化処理
        /// </summary>
        public void Initialize() {
            Debug.Log("[Test Input Reader] Initialize");
            _provider = new InputProvider();

            // コールバックの登録
            _provider.Player.SetCallbacks(this);
            _provider.UI.SetCallbacks(this);

            DisableAllInput();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            if (_provider == null) return;

            Debug.Log("[Test Input Reader] Dispose");

            _provider.Dispose();
            _provider = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (アクティブ設定)

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
        /// 決定
        /// </summary>
        public void OnSubmit(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                SubmitEvent.Invoke();
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        void InputProvider.IPlayerActions.OnMove(InputAction.CallbackContext context) {
            var vec2 = context.ReadValue<Vector2>();
            MoveEvent.Invoke(new Vector3(vec2.x, 0, vec2.y));
        }

        /// <summary>
        /// ジャンプ
        /// </summary>
        void InputProvider.IPlayerActions.OnJump(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// ラン
        /// </summary>
        void InputProvider.IPlayerActions.OnRun(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// 攻撃
        /// </summary>
        void InputProvider.IPlayerActions.OnAttack(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                AttackEvent.Invoke();
            }
        }

        /// <summary>
        /// カメラ操作
        /// </summary>
        /// <param name="context"></param>
        void InputProvider.IPlayerActions.OnCameraXY(InputAction.CallbackContext context) {
            CameraMoveEvent.Invoke(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// 視点カーソル操作
        /// </summary>
        void InputProvider.IPlayerActions.OnMoveCursor(InputAction.CallbackContext context) {

        }

        /// <summary>
        /// ポーズUI
        /// </summary>
        /// <param name="context"></param>
        void InputProvider.IPlayerActions.OnPause(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                PauseEvent.Invoke();
            }
        }

        /// <summary>
        /// インベントリUI
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
        /// ナビゲーション
        /// </summary>
        void InputProvider.IUIActions.OnNavigate(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// 決定
        /// </summary>
        void InputProvider.IUIActions.OnSubmit(InputAction.CallbackContext context) {
            UISubmitEvent.Invoke();
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        void InputProvider.IUIActions.OnCancel(InputAction.CallbackContext context) {
            UICancelEvent.Invoke();
        }

        /// <summary>
        /// ソート
        /// </summary>
        void InputProvider.IUIActions.OnSort(InputAction.CallbackContext context) {

        }

        // 

        /// <summary>
        /// マウスクリック
        /// </summary>
        void InputProvider.IUIActions.OnClick(InputAction.CallbackContext context) {

        }

        /// <summary>
        /// マウス中クリック
        /// </summary>
        void InputProvider.IUIActions.OnMiddleClick(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// マウス座標
        /// </summary>
        void InputProvider.IUIActions.OnPoint(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// マウス右クリック
        /// </summary>
        void InputProvider.IUIActions.OnRightClick(InputAction.CallbackContext context) {
        }

        /// <summary>
        /// マウスホイールスクロール
        /// </summary>
        void InputProvider.IUIActions.OnScrollWheel(InputAction.CallbackContext context) {
        }

        // 

        /// <summary>
        /// タブ切り替え（次）
        /// </summary>
        void InputProvider.IUIActions.OnMoveNextTab(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                UIMoveNextTabEvent.Invoke();
            }
        }

        /// <summary>
        /// タブ切り替え（前）
        /// </summary>
        void InputProvider.IUIActions.OnMovePreviousTab(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                UIMovePreviousTabEvent.Invoke();
            }
        }

        // 

        /// <summary>
        /// モデル回転
        /// </summary>
        void InputProvider.IUIActions.OnRotateModel(InputAction.CallbackContext context) {
        }


    }

}