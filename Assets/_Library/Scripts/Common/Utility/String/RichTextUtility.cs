using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.RichText {

    /// <summary>
    /// 文字列をリッチテキストへ変換する拡張メソッドクラス
    /// </summary>
    public static class RichTextUtility {

        /// <summary>
        /// カラータグを挿入する
        /// </summary>
        public static string WithColorTag(this string @this, Color color) {
            return string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
                (byte)(color.r * 255f),
                (byte)(color.g * 255f),
                (byte)(color.b * 255f),
                @this
            );
        }

        /// <summary>
        /// 太字タグを挿入する
        /// </summary>
        public static string WithBoldTag(this string @this) {
            return $"<b>{@this}</b>";
        }

        /// <summary>
        /// 斜体タグを挿入する
        /// </summary>
        public static string WithItalicTag(this string @this) {
            return $"<i>{@this}</i>";
        }

        /// <summary>
        /// サイズタグを挿入する
        /// </summary>
        public static string WithSizeTag(this string @this, int size) {
            return $"<size={size}>{@this}</size>";
        }

    }
}