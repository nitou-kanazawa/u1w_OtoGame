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
    /// �q�v�f��"Selectable"�ɑ΂���I����Ԃ𐧌䂷��R���|�[�l���g
    /// ��UI�P��ʂ̃��[�g�ɃA�^�b�`���邱�Ƃ�z��
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
        #region Inspecter Group (���C���X�y�N�^�\���p)

        // �O���[�v
        private const string STATE_INFO_GROUP = "Selection state";
        private const string TARGETS_INFO_GROUP = "Targets information";
        private const string DEBUG_INFO_GROUP = "Debug information";

        #endregion


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// �A�N�e�B�u���ǂ���
        /// </summary>
        [TitleGroup(STATE_INFO_GROUP), Indent]
        [ShowInInspector,ReadOnly]
        public bool IsActive => ActiveGroup != null;

        /// <summary>
        /// �I�𒆂�UI
        /// </summary>
        [TitleGroup(STATE_INFO_GROUP), Indent]
        [ShowInInspector, ReadOnly]
        public Selectable CurrentSelection { get; private set; }

        /// <summary>
        /// �}�E�X�őI�𒆂�UI
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
        /// �I��Ώۂ�UI 
        /// </summary>
        [TitleGroup(TARGETS_INFO_GROUP), Indent]
        [ShowInInspector, ReadOnly]
        public IReadOnlyList<Selectable> Selectables => ActiveGroup?.Selectables;




        /// <summary>
        /// �A�N�e�B�u�ȑI��ΏۃO���[�v
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
            // EventSystem�̑I���I�u�W�F�N�g
            var targetObj = EventSystem.current.currentSelectedGameObject;
            if (targetObj == CurrentSelection.gameObject) return;

            // �I����Ԃ̈ێ��i���̈�O�N���b�N�Ȃǂł̑I�������̖h�~�j
            if (targetObj == null) {
                CurrentSelection.Select();
                return;
            }

            // ------------------------------
            // �ΏۃI�u�W�F�N�g�̃R���|�[�l���g
            var targetSelectable = targetObj.GetComponent<Selectable>();

            // �A�N�e�B�uGroup�ȊO�̃I�u�W�F�N�g�̏ꍇ�C
            if (!ActiveGroup.Selectables.Contains(targetSelectable)) {
                CurrentSelection.Select();
                return;
            }
        }

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �O���[�v�̓o�^
        /// </summary>
        public void RegisterGroup(SelectableGroup group, ScreenType type = ScreenType.Page) {
            if (group == null) throw new System.ArgumentNullException(nameof(group));

            // �O���[�v�����o�^�̏ꍇ�C
            if (!_groups.ContainsKey(type)) {
                //Debug_.Log("�O���[�v��V�K�o�^");
                _groups.Add(type, group);
            }

            // ���O���[�v���o�^����Ă���ꍇ�C
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
        /// �O���[�v�̓o�^����
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

            // �C�x���g�����̃o�C���h
            ActiveGroup.OnPointerEnter += HandleMouseEnter;
            ActiveGroup.OnPointerExit += HandleMouseExit;
            ActiveGroup.OnSelect += HandleSelected;


            Select();
        }

        public void Deactivate() {
            if (IsActive) {
                // �C�x���g�����̃o�C���h����
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

            // �O���[�v�ɑΏۂ��܂܂��ꍇ�C
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
        // Private Method (��SelectableGroup����Ăяo�����\�b�h)

        /// <summary>
        /// UI�ɃJ�[�\�����d�Ȃ������̏���
        /// </summary>
        private void HandleMouseEnter(GameObject selected) {
            MouseSelection = selected.GetComponent<Selectable>();
            EventSystem.current.SetSelectedGameObject(selected);
        }

        /// <summary>
        /// UI����J�[�\�����O�ꂽ���̏���
        /// </summary>
        public void HandleMouseExit(GameObject selected) {
            if (EventSystem.current.currentSelectedGameObject != selected) return;

            // �I����Ԃ�߂�
            MouseSelection = null;
            EventSystem.current.SetSelectedGameObject(CurrentSelection.gameObject);
        }

        /// <summary>
        /// UI���I�����ꂽ���̏���
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
        /// �I��Ώ�UI�O���[�v�̓o�^
        /// </summary>
        public void RegiserPageSelectables(SelectableGroup group) {
            if (group == null) throw new System.ArgumentNullException(nameof(group));

            // ----
            // �A�N�e�B�u�O���[�v�̕ύX
            if (group != _pageGroup) {

                // ���O���[�v�̓o�^����
                UnregisterPageSelectables();

                // �V�O���[�v�̓o�^
                _pageGroup = group;
                _pageGroup.SetIntarability(true);

                // �C�x���g�����̃o�C���h
                _pageGroup.OnPointerEnter += HandleMouseEnter;
                _pageGroup.OnPointerExit += HandleMouseExit;
                _pageGroup.OnSelect += HandleSelected;
            }

            ScreenTarget = ScreenType.Page;
        }

        /// <summary>
        /// �I��Ώ�UI�O���[�v�̓o�^����
        /// </summary>
        public void UnregisterPageSelectables() {
            if (_pageGroup == null) return;

            CurrentSelection = null;
            MouseSelection = null;

            // �A�N�e�B�u�O���[�v�̉���
            {
                _pageGroup.SetIntarability(false);

                // �C�x���g�����̓o�^����
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
        /// �I��Ώ�UI�O���[�v�̓o�^
        /// </summary>
        public void RegiserModalSelectables(SelectableGroup group) {
            if (group == null) throw new System.ArgumentNullException(nameof(group));

            // ----
            // �A�N�e�B�u�O���[�v�̕ύX
            if (group != _modalGroup) {

                // ���O���[�v�̓o�^����
                UnregisterModalSelectables();

                // �V�O���[�v�̓o�^
                _modalGroup = group;
                _modalGroup.SetIntarability(true);

                // �C�x���g�����̃o�C���h
                _modalGroup.OnPointerEnter += HandleMouseEnter;
                _modalGroup.OnPointerExit += HandleMouseExit;
                _modalGroup.OnSelect += HandleSelected;
            }

            ScreenTarget = ScreenType.Modal;
        }

        /// <summary>
        /// �I��Ώ�UI�O���[�v�̓o�^����
        /// </summary>
        public void UnregisterModalSelectables() {
            if (_modalGroup == null) return;

            CurrentSelection = null;
            MouseSelection = null;

            // �A�N�e�B�u�O���[�v�̉���
            {
                _modalGroup.SetIntarability(false);

                // �C�x���g�����̓o�^����
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