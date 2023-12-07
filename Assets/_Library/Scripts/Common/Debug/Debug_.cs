using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

// [参考]
//  qiita: UnityEditorの時のみDebug.Logを出す方法 https://qiita.com/toRisouP/items/d856d65dcc44916c487d
//  zonn: Debug.Logを便利にするために工夫していること https://zenn.dev/happy_elements/articles/38be21755773e0
//  _: Color型変数をもとにDebug.Logの文字色を変更する https://www.kemomimi.dev/unity/78/

namespace nitou {
    using nitou.RichText;

    /// <summary>
    /// Debug.Logのラッパークラス
    /// </summary>
    public static class Debug_ {

        /// ----------------------------------------------------------------------------
        // Public Method (通常ログ)

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o) => Debug.Log(FormatObject(o));

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o, Color color) => Debug.Log(FormatObject(o).WithColorTag(color));
        //    {
        //    var message = string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
        //        (byte)(color.r * 255f),
        //        (byte)(color.g * 255f),
        //        (byte)(color.b * 255f),
        //        o);
        //    Debug.Log(message);
        //}

        /// <summary>
        /// UnityEditor上でのみ実行されるLogWarningメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(object o) => Debug.LogWarning(o);

        /// <summary>
        /// UnityEditor上でのみ実行されるLogErrorメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void LogError(object o) => Debug.LogError(o);


        /// ----------------------------------------------------------------------------
        // Public Method (配列)

        private static readonly int MAX_LIST_SIZE = 100;

        public static void Main() {


        }

        /// <summary>
        /// UnityEditor上でのみ実行されるLogメソッド
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void ListLog<T>(IReadOnlyList<T> list) {

            var sb = new StringBuilder();
            sb.Append($"(Total list count is {list.Count})\n");

            // 文字列へ変換
            for (int i = 0; i < list.Count; i++) {

                // 最大行数を超えた場合，
                if (i >= MAX_LIST_SIZE) {
                    sb.Append($"(+{list.Count - MAX_LIST_SIZE} items has been omitted)");
                    break;
                }

                // 要素の追加
                sb.Append($"{FormatObject(list[i])} \n");
            }

            // コンソール出力
            Debug.Log(sb.ToString());
        }

        public static void DictLog<T>(IReadOnlyList<T> dict) {

        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// 文字列への変換（※null，空文字が判別できる形式）
        /// </summary>
        public static string FormatObject(object o) {
            if (o is null) {
                return "(null)";
            }
            if (o as string == string.Empty) {
                return "(empty)";
            }

            return o.ToString();
        }
    }

}