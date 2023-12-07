using nitou.DesiginPattern;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;

namespace nitou.UI {

    /// <summary>
    /// 子要素の"Selectable"に対する選択状態を制御するコンポーネント
    /// ※UI１画面のルートにアタッチすることを想定
    /// </summary>
    public partial class UISelectionManager : SingletonMonoBehaviour<UISelectionManager> {

        /// <summary>
        /// 
        /// </summary>
        public enum ScreenType {
            Page,
            Modal,
        }

        /// ----------------------------------------------------------------------------
        #region Inspecter Group (※インスペクタ表示用)

        // グループ
        private const string STATE_INFO_GROUP = "Selection state";
        private const string TARGETS_INFO_GROUP = "Targets information";
        private const string DEBUG_INFO_GROUP = "Debug information";

        #endregion


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// アクティブかどうか
        /// </summary>
        [TitleGroup(STATE_INFO_GROUP), Indent]
        [ShowInInspector,ReadOnly]
        public bool IsActive => ActiveGroup != null;

        /// <summary>
        /// 選択中のUI
        /// </summary>
        [TitleGroup(STATE_INFO_GROUP), Indent]
        [ShowInInspector, ReadOnly]
        public Selectable CurrentSelection { get; private set; }

        /// <summary>
        /// マウスで選択中のUI
        /// </summary>
        [TitleGroup(STATE_INFO_GROUP), Indent]
        [ShowInInspector, ReadOnly]
        public Selectable MouseSelection { get; private set; }


        /// ----------------------------------------------------------------------------
        // Private Method 

        [TitleGroup(TARGETS_INFO_GROUP), Indent]
        [GUIColor(0f, 1f, 0.5f)]
        [ShowInInspector, ReadOnly]
        public ScreenType ScreenTarget { get; private set; } = ScreenType.Page;

        /// <summary>
        /// 選択対象のUI 
        /// </summary>
        [TitleGroup(TARGETS_INFO_GROUP), Indent]
        [ShowInInspector, ReadOnly]
        public IReadOnlyList<Selectable> Selectables => ActiveGroup?.Selectables;




        /// <summary>
        /// アクティブな選択対象グループ
        /// </summary>
        public SelectableGroup ActiveGroup { get; private set; } = null;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<ScreenType, SelectableGroup> _groups = new();



        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected override void Awake() {
            if (!base.CheckInstance()) {
                Destroy(this);
                return;
            }
        }

        private void OnDisable() {
            Deactivate();
        }

        private void OnDestroy() {
            _groups.Clear();
        }


        private void LateUpdate() {
            if (!IsActive || EventSystem.current == null) return;

            // ------------------------------
            // EventSystemの選択オブジェクト
            var targetObj = EventSystem.current.currentSelectedGameObject;
            if (targetObj == CurrentSelection.gameObject) return;

            // 選択状態の維持（※領域外クリックなどでの選択解除の防止）
            if (targetObj == null) {
                CurrentSelection.Select();
                return;
            }

            // ------------------------------
            // 対象オブジェクトのコンポーネント
            var targetSelectable = targetObj.GetComponent<Selectable>();

            // アクティブGroup以外のオブジェクトの場合，
            if (!ActiveGroup.Selectables.Contains(targetSelectable)) {
                CurrentSelection.Select();
                return;
            }
        }

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// グループの登録
        /// </summary>
        public void RegisterGroup(SelectableGroup group, ScreenType type = ScreenType.Page) {
            if (group == null) throw new System.ArgumentNullException(nameof(group));

            // グループが未登録の場合，
            if (!_groups.ContainsKey(type)) {
                //Debug_.Log("グループを新規登録");
                _groups.Add(type, group);
            }

            // 他グループが登録されている場合，
            else if (group != _groups[type]) {

                // 
                if (_groups[type] == ActiveGroup) {
                    Deactivate();
                    _groups[type] = group;
                    Activate(type);
                }
                // 
                else {
                    _groups[type] = group;
                }
            }
        }

        /// <summary>
        /// グループの登録解除
        /// </summary>
        public void UnregisterGroup(ScreenType type = ScreenType.Page) {

            if (!_groups.ContainsKey(type)) return;

            if (_groups[type] == ActiveGroup) {
                Deactivate();
            }

            _groups.Remove(type);
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        public void Activate(ScreenType type = ScreenType.Page) {

            if (!_groups.ContainsKey(type) || _groups[type] == ActiveGroup) return;

            // 
            Deactivate();

            ActiveGroup = _groups[type];
            ActiveGroup.SetIntarability(true);

            // イベント処理のバインド
            ActiveGroup.OnPointerEnter += HandleMouseEnter;
            ActiveGroup.OnPointerExit += HandleMouseExit;
            ActiveGroup.OnSelect += HandleSelected;


            Select();
        }

        public void Deactivate() {
            if (IsActive) {
                // イベント処理のバインド解除
                ActiveGroup.OnPointerEnter -= HandleMouseEnter;
                ActiveGroup.OnPointerExit -= HandleMouseExit;
                ActiveGroup.OnSelect -= HandleSelected;

                ActiveGroup.SetIntarability(false);
                ActiveGroup = null;
            }

            Unselect();
        }

        public void Select(Selectable target) {
            if (target == null || ActiveGroup == null) return;

            // グループに対象が含まれる場合，
            if (ActiveGroup.SetSelection(target)) {
                CurrentSelection = ActiveGroup.Current;
                MouseSelection = ActiveGroup.Current;

                CurrentSelection.Select();
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void Select() {
            if (ActiveGroup == null) return;

            CurrentSelection = ActiveGroup.Current;
            MouseSelection = ActiveGroup.Current;

            CurrentSelection.Select();
        }

        private void Unselect() {
            CurrentSelection = null;
            MouseSelection = null;

            var eventSystem = EventSystem.current;
            if (eventSystem != null)
                eventSystem.SetSelectedGameObject(null);
        }


        /// ----------------------------------------------------------------------------
        // Private Method (※SelectableGroupから呼び出すメソッド)

        /// <summary>
        /// UIにカーソルが重なった時の処理
        /// </summary>
        private void HandleMouseEnter(GameObject selected) {
            MouseSelection = selected.GetComponent<Selectable>();
            EventSystem.current.SetSelectedGameObject(selected);
        }

        /// <summary>
        /// UIからカーソルが外れた時の処理
        /// </summary>
        public void HandleMouseExit(GameObject selected) {
            if (EventSystem.current.currentSelectedGameObject != selected) return;

            // 選択状態を戻す
            MouseSelection = null;
            EventSystem.current.SetSelectedGameObject(CurrentSelection.gameObject);
        }

        /// <summary>
        /// UIが選択された時の処理
        /// </summary>
        public void HandleSelected(GameObject selected) {
            var target = selected.GetComponent<Selectable>();

            ActiveGroup?.SetSelection(target);
            CurrentSelection = target;
            MouseSelection = target;

        }

    }


}


/*
 

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 選択対象UIグループの登録
        /// </summary>
        public void RegiserPageSelectables(SelectableGroup group) {
            if (group == null) throw new System.ArgumentNullException(nameof(group));

            // ----
            // アクティブグループの変更
            if (group != _pageGroup) {

                // 現グループの登録解除
                UnregisterPageSelectables();

                // 新グループの登録
                _pageGroup = group;
                _pageGroup.SetIntarability(true);

                // イベント処理のバインド
                _pageGroup.OnPointerEnter += HandleMouseEnter;
                _pageGroup.OnPointerExit += HandleMouseExit;
                _pageGroup.OnSelect += HandleSelected;
            }

            ScreenTarget = ScreenType.Page;
        }

        /// <summary>
        /// 選択対象UIグループの登録解除
        /// </summary>
        public void UnregisterPageSelectables() {
            if (_pageGroup == null) return;

            CurrentSelection = null;
            MouseSelection = null;

            // アクティブグループの解除
            {
                _pageGroup.SetIntarability(false);

                // イベント処理の登録解除
                _pageGroup.OnPointerEnter -= HandleMouseEnter;
                _pageGroup.OnPointerExit -= HandleMouseExit;
                _pageGroup.OnSelect -= HandleSelected;

                _pageGroup = null;
            }

            ScreenTarget = ScreenType.None;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 選択対象UIグループの登録
        /// </summary>
        public void RegiserModalSelectables(SelectableGroup group) {
            if (group == null) throw new System.ArgumentNullException(nameof(group));

            // ----
            // アクティブグループの変更
            if (group != _modalGroup) {

                // 現グループの登録解除
                UnregisterModalSelectables();

                // 新グループの登録
                _modalGroup = group;
                _modalGroup.SetIntarability(true);

                // イベント処理のバインド
                _modalGroup.OnPointerEnter += HandleMouseEnter;
                _modalGroup.OnPointerExit += HandleMouseExit;
                _modalGroup.OnSelect += HandleSelected;
            }

            ScreenTarget = ScreenType.Modal;
        }

        /// <summary>
        /// 選択対象UIグループの登録解除
        /// </summary>
        public void UnregisterModalSelectables() {
            if (_modalGroup == null) return;

            CurrentSelection = null;
            MouseSelection = null;

            // アクティブグループの解除
            {
                _modalGroup.SetIntarability(false);

                // イベント処理の登録解除
                _modalGroup.OnPointerEnter -= HandleMouseEnter;
                _modalGroup.OnPointerExit -= HandleMouseExit;
                _modalGroup.OnSelect -= HandleSelected;

                _modalGroup = null;
            }

            if (_pageGroup != null) {
                ScreenTarget = ScreenType.Page;
                Select(_pageGroup.First);
            } else {
                ScreenTarget = ScreenType.None;
            }

        }

 */