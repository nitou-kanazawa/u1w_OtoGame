using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace nitou.UI {

    /// <summary>
    /// 複数のSelectableを含むコンテナであることを意味するインターフェース
    /// </summary>
    public interface ISelectableContainer {

        /// <summary>
        /// デフォルトで選択されるUI
        /// </summary>
        public Selectable DefaultSelection { get; }

        /// <summary>
        /// 管理下のUIリスト
        /// </summary>
        public SelectableGroup SelectableGroup { get; set; }

        /// <summary>
        /// システムクラスに自動選択されるかどうか
        /// </summary>
        //public bool AutoSelected { get;}

        /// <summary>
        /// システムクラスに選択される時の遅延
        /// </summary>
        public float Delay { get;}
    }

}