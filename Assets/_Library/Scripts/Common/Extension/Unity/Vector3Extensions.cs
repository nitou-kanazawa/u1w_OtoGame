using UnityEngine;
using UnityEngine.UI;

// [参考]
//  PG日誌 : Vector3(構造体)に自分自身の値を変更する拡張メソッドを定義する https://takap-tech.com/entry/2022/12/24/175039

namespace nitou {

    /// <summary>
    /// Vector3の拡張メソッドクラス
    /// </summary>
    public static partial class Vector3Extensions {

        /// ----------------------------------------------------------------------------
        // 値の設定

        /// <summary>
        /// X値を設定する拡張メソッド
        /// </summary>
        public static void SetX(ref this Vector3 @this, float x) => @this.x = x;

        /// <summary>
        /// Y値を設定する拡張メソッド
        /// </summary>
        public static void SetY(ref this Vector3 @this, float y) => @this.y = y;

        /// <summary>
        /// Z値を設定する拡張メソッド
        /// </summary>
        public static void SetZ(ref this Vector3 @this, float z) => @this.z = z;


        /// ----------------------------------------------------------------------------
        // 値の加算

        /// <summary>
        /// X値に加算する拡張メソッド
        /// </summary>
        public static void AddX(ref this Vector3 @this, float x) => @this.x += x;

        /// <summary>
        /// Y値に加算する拡張メソッド
        /// </summary>
        public static void AddY(ref this Vector3 @this, float y) => @this.y += y;

        /// <summary>
        /// Z値に加算する拡張メソッド
        /// </summary>
        public static void AddZ(ref this Vector3 @this, float z) => @this.z += z;
    }

}