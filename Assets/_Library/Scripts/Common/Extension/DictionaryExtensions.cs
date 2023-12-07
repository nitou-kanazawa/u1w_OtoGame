using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// [参考]
//  コガネブログ: Dictionaryをforeachで使う時の記述を簡略化するDeconstruction https://baba-s.hatenablog.com/entry/2019/09/03/231000

namespace nitou {

    /// <summary>
    /// Dictionaryの拡張メソッドクラス
    /// </summary>
    public static class DictionaryExtensions {

        /// <summary>
        /// デコンストラクト
        /// </summary>
        public static void Deconstruct<TKey, TValue>(
            this KeyValuePair<TKey, TValue> @this,
            out TKey key,
            out TValue value
        ) {
            key = @this.Key;
            value = @this.Value;
        }


        /// <summary>
        /// 指定されたキーが格納されている場合にactionを呼び出す拡張メソッド
        /// </summary>
        public static void SafeCall<TKey, TValue>(
            this Dictionary<TKey, TValue> @this, 
            TKey key,
            Action<TValue> action
        ) {
            if (!@this.ContainsKey(key)) {
                return;
            }
            action(@this[key]);
        }


    }
}