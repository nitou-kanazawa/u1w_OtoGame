using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace nitou {

    /// <summary>
    /// カラー設定が可能なグループ表示を行う属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class ColoredFoldoutGroupAttribute : PropertyGroupAttribute {

        /// ----------------------------------------------------------------------------
        // Field

        public float R = 0;
        public float G = 0;
        public float B = 0;
        public float A = 1;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColoredFoldoutGroupAttribute(string path)
            : base(path) {}

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColoredFoldoutGroupAttribute(string path, float r, float g, float b, float a = 1f)
            : base(path) {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 設定カラーの合成
        /// </summary>
        protected override void CombineValuesWith(PropertyGroupAttribute other) {
            var otherAttr = (ColoredFoldoutGroupAttribute)other;

            this.R = Mathf.Max(otherAttr.R, this.R);
            this.G = Mathf.Max(otherAttr.G, this.G);
            this.B = Mathf.Max(otherAttr.B, this.B);
            this.A = Mathf.Max(otherAttr.A, this.A);
        }


    }
}