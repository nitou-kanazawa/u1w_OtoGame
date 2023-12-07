using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// [参考]
//  Hatena Blog: Action, Func, Predicateデリゲートを使ってみた https://oooomincrypto.hatenadiary.jp/entry/2022/04/24/201149
//  JojoBase: 拡張メソッドは作って貯めておくと便利です https://johobase.com/custom-extension-methods-list/#i-5
//  qiita: あるとちょっと便利な拡張メソッド紹介 https://qiita.com/s_mino_ri/items/0fd2e2b3cebb7a62ad46

namespace nitou {

    /// <summary>
    /// Collectionの拡張メソッドクラス
    /// </summary>
    public static partial class CollectionExtensions {

        /// ----------------------------------------------------------------------------
        // 要素の判定

        /// <summary>
        /// コレクションがNullまたは空かどうかを判定する拡張メソッド
        /// </summary>
        public static bool IsNullOrEmpty(this IList @this) =>
            @this == null || @this.Count == 0;


        /// ----------------------------------------------------------------------------
        // 要素の追加・削除

        /// <summary>
        /// 指定した処理条件を満たす場合に項目を追加する拡張メソッド
        /// </summary>
        public static bool AddIf<T>(this ICollection<T> @this, Predicate<T> predicate, T item) {
            if (predicate(item)) {
                @this.Add(item);
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 指定した処理条件を満たす場合に項目を削除する拡張メソッド
        /// </summary>
        public static void RemoveIf<T>(this ICollection<T> @this, Predicate<T> predicate, T item) {
            if (predicate(item)) {
                @this.Remove(item);
            }
        }


        /// ----------------------------------------------------------------------------
        // 要素の取得

        /// <summary>
        /// ランダムに要素を取得する拡張メソッド
        /// </summary>
        public static T GetRandom<T>(this IList<T> @this) =>
            @this[UnityEngine.Random.Range(0, @this.Count)];


        /// ----------------------------------------------------------------------------
        // 要素の走査

        /// <summary>
        /// IEnumerable の各要素に対して、指定された処理を実行する拡張メソッド
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action) {
            foreach (var n in source.Select((val, index) => new { val, index })) {
                action(n.val, n.index);
            }
        }

    }
}