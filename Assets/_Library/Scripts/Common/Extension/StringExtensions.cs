using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// [�Q�l]
//  �R�K�l�u���O: �w�肳�ꂽ�����񂪓d�b�ԍ����ǂ�����Ԃ��֐� https://baba-s.hatenablog.com/entry/2014/11/10/110048

namespace nitou {

    /// <summary>
    /// String�̊g�����\�b�h�N���X
    /// </summary>
    public static class StringExtensions {

        /// ----------------------------------------------------------------------------
        // ������̔���

        /// <summary>
        /// �����񂪎w�肳�ꂽ�����ꂩ�̕�����Ɠ��������ǂ����𔻒肷��
        /// </summary>
        public static bool IsAny(this string @this, params string[] values) =>
            values.Any(c => c == @this);

        /// <summary>
        /// �����񂪃��[���A�h���X���ǂ�����Ԃ��܂�
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
        /// �����񂪓d�b�ԍ����ǂ����𔻒肷��
        /// </summary>
        public static bool IsPhoneNumber(this string @this) {
            if (string.IsNullOrEmpty(@this)) { return false; }

            return Regex.IsMatch(
                @this,
                @"^0\d{1,4}-\d{1,4}-\d{4}$"
            );
        }

        /// <summary>
        /// �����񂪗X�֔ԍ����ǂ����𔻒肷��
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
        /// ������ URL ���ǂ����𔻒肷��
        /// </summary>
        public static bool IsUrl(this string @this) {
            if (string.IsNullOrEmpty(@this)) { return false; }

            return Regex.IsMatch(
               @this,
               @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$"
            );
        }

        /// <summary>
        /// ������ Null�������͋� ���ǂ����𔻒肷��
        /// </summary>
        public static bool IsNullOrEmpty(this string @this) =>
            string.IsNullOrEmpty(@this);

        /// <summary>
        /// ������ Null/�󕶎�/�󔒕��� ���ǂ����𔻒肷��
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string @this) =>
            string.IsNullOrWhiteSpace(@this);

        /// <summary>
        /// �w�肳�ꂽ�����ꂩ�̕�������܂ނ��ǂ����𔻒肷��
        /// </summary>
        public static bool IncludeAny(this string self, params string[] list) =>
        list.Any(c => self.Contains(c));


        /// ----------------------------------------------------------------------------
        // �������

        /// ----------------------------------------------------------------------------
        // 


        /// ----------------------------------------------------------------------------
        // 
    }

}