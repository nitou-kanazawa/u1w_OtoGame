using System.Collections;
using System.Collections.Generic;

// [�Q�l]
//  qiita: �p�X���[�h�̂悤�ȃ����_���ȕ�����𐶐����ĕԂ��֐� https://baba-s.hatenablog.com/entry/2015/07/07/000000


namespace nitou {

    /// <summary>
    /// 
    /// </summary>
    public static partial class StringUtility {

        /// ----------------------------------------------------------------------------
        // ������̐���

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