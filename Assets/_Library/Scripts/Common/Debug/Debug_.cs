using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

// [�Q�l]
//  qiita: UnityEditor�̎��̂�Debug.Log���o�����@ https://qiita.com/toRisouP/items/d856d65dcc44916c487d
//  zonn: Debug.Log��֗��ɂ��邽�߂ɍH�v���Ă��邱�� https://zenn.dev/happy_elements/articles/38be21755773e0
//  _: Color�^�ϐ������Ƃ�Debug.Log�̕����F��ύX���� https://www.kemomimi.dev/unity/78/

namespace nitou {
    using nitou.RichText;

    /// <summary>
    /// Debug.Log�̃��b�p�[�N���X
    /// </summary>
    public static class Debug_ {

        /// ----------------------------------------------------------------------------
        // Public Method (�ʏ탍�O)

        /// <summary>
        /// UnityEditor��ł̂ݎ��s�����Log���\�b�h
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void Log(object o) => Debug.Log(FormatObject(o));

        /// <summary>
        /// UnityEditor��ł̂ݎ��s�����Log���\�b�h
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
        /// UnityEditor��ł̂ݎ��s�����LogWarning���\�b�h
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(object o) => Debug.LogWarning(o);

        /// <summary>
        /// UnityEditor��ł̂ݎ��s�����LogError���\�b�h
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void LogError(object o) => Debug.LogError(o);


        /// ----------------------------------------------------------------------------
        // Public Method (�z��)

        private static readonly int MAX_LIST_SIZE = 100;

        public static void Main() {


        }

        /// <summary>
        /// UnityEditor��ł̂ݎ��s�����Log���\�b�h
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        public static void ListLog<T>(IReadOnlyList<T> list) {

            var sb = new StringBuilder();
            sb.Append($"(Total list count is {list.Count})\n");

            // ������֕ϊ�
            for (int i = 0; i < list.Count; i++) {

                // �ő�s���𒴂����ꍇ�C
                if (i >= MAX_LIST_SIZE) {
                    sb.Append($"(+{list.Count - MAX_LIST_SIZE} items has been omitted)");
                    break;
                }

                // �v�f�̒ǉ�
                sb.Append($"{FormatObject(list[i])} \n");
            }

            // �R���\�[���o��
            Debug.Log(sb.ToString());
        }

        public static void DictLog<T>(IReadOnlyList<T> dict) {

        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// ������ւ̕ϊ��i��null�C�󕶎������ʂł���`���j
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