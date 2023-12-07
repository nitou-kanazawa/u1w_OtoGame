using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// [参考]
//  コガネブログ: 指定された文字列が電話番号かどうかを返す関数 https://baba-s.hatenablog.com/entry/2014/11/10/110048

namespace nitou {

    /// <summary>
    /// Stringの拡張メソッドクラス
    /// </summary>
    public static class StringExtensions {

        /// ----------------------------------------------------------------------------
        // 文字列の判定

        /// <summary>
        /// 文字列が指定されたいずれかの文字列と等しいかどうかを判定する
        /// </summary>
        public static bool IsAny(this string @this, params string[] values) =>
            values.Any(c => c == @this);

        /// <summary>
        /// 文字列がメールアドレスかどうかを返します
        /// </summary>
        public static bool IsMailAddress(string @this) {
            if (string.IsNullOrEmpty(@this)) { return false; }

            return Regex.IsMatch(
                @this,
                @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 文字列が電話番号かどうかを判定する
        /// </summary>
        public static bool IsPhoneNumber(this string @this) {
            if (string.IsNullOrEmpty(@this)) { return false; }

            return Regex.IsMatch(
                @this,
                @"^0\d{1,4}-\d{1,4}-\d{4}$"
            );
        }

        /// <summary>
        /// 文字列が郵便番号かどうかを判定する
        /// </summary>
        public static bool IsZipCode(string @this) {
            if (string.IsNullOrEmpty(@this)) { return false; }

            return Regex.IsMatch(
                @this,
                @"^\d\d\d-\d\d\d\d$",
                RegexOptions.ECMAScript
            );
        }

        /// <summary>
        /// 文字列が URL かどうかを判定する
        /// </summary>
        public static bool IsUrl(this string @this) {
            if (string.IsNullOrEmpty(@this)) { return false; }

            return Regex.IsMatch(
               @this,
               @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$"
            );
        }

        /// <summary>
        /// 文字列が Nullもしくは空 かどうかを判定する
        /// </summary>
        public static bool IsNullOrEmpty(this string @this) =>
            string.IsNullOrEmpty(@this);

        /// <summary>
        /// 文字列が Null/空文字/空白文字 かどうかを判定する
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string @this) =>
            string.IsNullOrWhiteSpace(@this);

        /// <summary>
        /// 指定されたいずれかの文字列を含むかどうかを判定する
        /// </summary>
        public static bool IncludeAny(this string self, params string[] list) =>
        list.Any(c => self.Contains(c));


        /// ----------------------------------------------------------------------------
        // 文字列の

        /// ----------------------------------------------------------------------------
        // 


        /// ----------------------------------------------------------------------------
        // 
    }

}