using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.RichText {

    /// <summary>
    /// ����������b�`�e�L�X�g�֕ϊ�����g�����\�b�h�N���X
    /// </summary>
    public static class RichTextUtility {

        /// <summary>
        /// �J���[�^�O��}������
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
        /// �����^�O��}������
        /// </summary>
        public static string WithBoldTag(this string @this) {
            return $"<b>{@this}</b>";
        }

        /// <summary>
        /// �Α̃^�O��}������
        /// </summary>
        public static string WithItalicTag(this string @this) {
            return $"<i>{@this}</i>";
        }

        /// <summary>
        /// �T�C�Y�^�O��}������
        /// </summary>
        public static string WithSizeTag(this string @this, int size) {
            return $"<size={size}>{@this}</size>";
        }

    }
}