using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace nitou.UI {

    /// <summary>
    /// "Selectable"���Ǘ�����N���X
    /// </summary>
    [Serializable]
    public partial class SelectableGroup : IDisposable {

        /// <summary>
        /// �Ώ�UI�̃��X�g
        /// </summary>
        [ShowInInspector, ReadOnly]
        public IReadOnlyList<Selectable> Selectables => _selectables;
        private List<Selectable> _selectables = null;

        /// <summary>
        /// �ŏ��̗v�f
        /// </summary>
        public Selectable First => _selectables.First();

        /// <summary>
        /// ���݂̑I�����
        /// </summary>
        public Selectable Current { get; private set; }


        // �C�x���g�ʒm
        public event Action<GameObject> OnPointerEnter = delegate { };
        public event Action<GameObject> OnPointerExit = delegate { };
        public event Action<GameObject> OnSelect = delegate { };

        // ���X�g�N���A�p
        private event Action OnListRelesed = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public SelectableGroup(Selectable selectable) {
            if(selectable==null) throw new System.ArgumentNullException(nameof(selectable));

            _selectables = new List<Selectable>() { selectable };
            ResetSelection();
            AttachComponent();
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public SelectableGroup(List<Selectable> selectables) {
            _selectables = selectables ?? throw new System.ArgumentNullException(nameof(selectables));
            ResetSelection();
            AttachComponent();
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
            OnListRelesed?.Invoke();

            _selectables?.Clear();
            _selectables = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        public void SetIntarability(bool value) {
            _selectables.ForEach(s => s.interactable = value);
        }

        /// <summary>
        /// �I������ݒ肷��
        /// </summary>
        /// <param name="target"></param>
        public bool SetSelection(Selectable target) {

            if (target != null && Selectables.Contains(target)) {
                Current = target;
                return true;
            }

            return false;
        }

        /// <summary>
        /// �I���������Z�b�g����
        /// </summary>
        public void ResetSelection() {
                Current = First;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �Ď��p�R���|�[�l���g���A�^�b�`����
        /// </summary>
        private void AttachComponent() {
            foreach (var selectable in _selectables) {
                // �R���|�[�l���g�t�^
                var hooker = selectable.gameObject.GetOrAddComponent<SelectableGroup.SelectionHooker>();
                hooker.Register(this);

                // �N���A���̃R�[���o�b�N
                OnListRelesed += hooker.Unegister;
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method (���Ď��R���|�[�l���g����Ăяo�����\�b�h)

        /// <summary>
        /// UI�C�x���g���O���֒ʒm����
        /// </summary>
        private void NotifyPointerEnter(GameObject selected) => OnPointerEnter.Invoke(selected);

        /// <summary>
        /// UI�C�x���g���O���֒ʒm����
        /// </summary>
        private void NotifyPointerExit(GameObject selected) => OnPointerExit.Invoke(selected);

        /// <summary>
        /// UI�C�x���g���O���֒ʒm����
        /// </summary>
        private void NotifySelected(GameObject selected) => OnSelect.Invoke(selected);

    }



}