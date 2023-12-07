using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou {

    /// <summary>
    /// �Ǝ��f�[�^�Ƌ��ʃf�[�^�̐؂�ւ����\�ȃf�[�^�Q�ƃN���X
    /// </summary>
    /// <typeparam name="TValue">�Ώی^</typeparam>
    /// <typeparam name="TAsset">�Ώی^�̋��ʃf�[�^�A�Z�b�g</typeparam>
    [InlineProperty]
    [LabelWidth(75)]
    public abstract class ValueRefrence<TValue, TAsset> where TAsset : ValueAsset<TValue> {

        [HorizontalGroup("Reference", MaxWidth = 100)]
        [ValueDropdown("_valueList")]
        [SerializeField]
        protected bool _useValue = true;


        /// ----------------------------------------------------------------------------
        // Field (�f�[�^)

        [ShowIf("_useValue",Animate =false)]
        [HorizontalGroup("Reference")]
        [HideLabel]
        [SerializeField]
        protected TValue _uniqueValue;

        [ShowIf("_useValue", Animate = false)]
        [HorizontalGroup("Reference")]
        [HideLabel]
        [OnValueChanged("UpdateAsset")]
        [SerializeField]
        protected TAsset _assetReference;

        /// <summary>
        /// �l�̎擾�v���p�e�B
        /// </summary>
        public TValue value =>
            (_assetReference == null || _useValue) ? _uniqueValue : _assetReference.value;


        /// ----------------------------------------------------------------------------
        // Field (�C���X�y�N�^����p)

        [ShowIf("@_assetReference != null && _useValue == false")]
        [LabelWidth(100)]
        [SerializeField]
        protected bool _editAsset = false;

        [ShowIf("@_assetReference != null && _useValue == false")]
        [EnableIf("_editAsset")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [SerializeField]
        protected TAsset _assetReferenceForEdit;


        /// �h���b�v�_�E��
        private static ValueDropdownList<bool> _valueList = new() {
            { "Value", true },
            { "Referrence", false },
        };

        /// �C���X�y�N�^�ύX���̍X�V����
        protected void UpdateAsset() {
            _assetReferenceForEdit = _assetReference;
        }

        /// TValue�ւ̈ÖٓI�L���X�g
        public static implicit operator TValue(ValueRefrence<TValue,TAsset> valueRefrence)=>
            valueRefrence.value;
    }


}