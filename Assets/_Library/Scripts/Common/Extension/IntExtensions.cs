using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace nitou {

    /// <summary>
    /// Int型の拡張メソッドクラス
    /// </summary>
    public static partial class IntExtensions {

        /// ----------------------------------------------------------------------------
        // 値の判定

        /// <summary>
        /// 偶数かどうかを判定する
        /// </summary>
        public static bool IsEven(this int @this) => (@this % 2) == 0;

        /// <summary>
        /// 奇数かどうかを判定する
        /// </summary>
        public static bool IsOdd(this int @this) => (@this % 2) != 0;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// 数値を加算して、範囲を超えた分は 0 からの値として処理して返す
        /// </summary>
        public static int Repeat(this int @this, int value, int max) => (@this + value + max) % max;


        /// ----------------------------------------------------------------------------
        // 文字列への変換

        /// <summary>
        /// 指定された桁数で0埋めした文字列を返す
        /// </summary>
        public static string ToString_ZeroFill(this int @this, int numberOfDigits) =>
            @this.ToString("D" + numberOfDigits);

        /// <summary>
        /// 指定された桁数の固定小数点数を付加した文字列を返す
        /// </summary>
        /// <remarks>
        /// 123.FixedPoint(2) → 123.00
        /// 123.FixedPoint(4) → 123.0000
        /// </remarks>
        public static string ToString_FixedPoint(this int @this, int numberOfDigits) =>
            @this.ToString("F" + numberOfDigits);

        /// <summary>
        /// 指定された
        /// </summary>
        public static string ToString_Separate(this int @this) =>
            string.Format("{0:#,0}", @this);

    }


}
