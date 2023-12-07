using System.Collections;
using System.Collections.Generic;

// [参考]
//  qiita: パスワードのようなランダムな文字列を生成して返す関数 https://baba-s.hatenablog.com/entry/2015/07/07/000000


namespace nitou {

    /// <summary>
    /// 
    /// </summary>
    public static partial class StringUtility {

        /// ----------------------------------------------------------------------------
        // 文字列の生成

        /// <summary>
        /// 
        /// </summary>
        public static string GeneratePassword(int length) {
            var sb = new System.Text.StringBuilder(length);
            var r = new System.Random();

            for (int i = 0; i < length; i++) {
                int pos = r.Next(PASSWORD_CHARS.Length);
                char c = PASSWORD_CHARS[pos];
                sb.Append(c);
            }

            return sb.ToString();
        }
        private const string PASSWORD_CHARS = "0123456789abcdefghijklmnopqrstuvwxyz";





        /// ----------------------------------------------------------------------------
        // 

    }

}