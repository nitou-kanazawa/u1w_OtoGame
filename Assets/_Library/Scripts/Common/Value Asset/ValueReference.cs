using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou {

    /// <summary>
    /// 独自データと共通データの切り替えが可能なデータ参照クラス
    /// </summary>
    /// <typeparam name="TValue">対象型</typeparam>
    /// <typeparam name="TAsset">対象型の共通データアセット</typeparam>
    [InlineProperty]
    [LabelWidth(75)]
    public abstract class ValueRefrence<TValue, TAsset> where TAsset : ValueAsset<TValue> {

        [HorizontalGroup("Reference", MaxWidth = 100)]
        [ValueDropdown("_valueList")]
        [SerializeField]
        protected bool _useValue = true;


        /// ----------------------------------------------------------------------------
        // Field (データ)

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
        /// 値の取得プロパティ
        /// </summary>
        public TValue value =>
            (_assetReference == null || _useValue) ? _uniqueValue : _assetReference.value;


        /// ----------------------------------------------------------------------------
        // Field (インスペクタ操作用)

        [ShowIf("@_assetReference != null && _useValue == false")]
        [LabelWidth(100)]
        [SerializeField]
        protected bool _editAsset = false;

        [ShowIf("@_assetReference != null && _useValue == false")]
        [EnableIf("_editAsset")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [SerializeField]
        protected TAsset _assetReferenceForEdit;


        /// ドロップダウン
        private static ValueDropdownList<bool> _valueList = new() {
            { "Value", true },
            { "Referrence", false },
        };

        /// インスペクタ変更時の更新処理
        protected void UpdateAsset() {
            _assetReferenceForEdit = _assetReference;
        }

        /// TValueへの暗黙的キャスト
        public static implicit operator TValue(ValueRefrence<TValue,TAsset> valueRefrence)=>
            valueRefrence.value;
    }


}