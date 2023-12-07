using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace nitou {

    /// <summary>
    /// Int�^�̊g�����\�b�h�N���X
    /// </summary>
    public static partial class IntExtensions {

        /// ----------------------------------------------------------------------------
        // �l�̔���

        /// <summary>
        /// �������ǂ����𔻒肷��
        /// </summary>
        public static bool IsEven(this int @this) => (@this % 2) == 0;

        /// <summary>
        /// ����ǂ����𔻒肷��
        /// </summary>
        public static bool IsOdd(this int @this) => (@this % 2) != 0;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// ���l�����Z���āA�͈͂𒴂������� 0 ����̒l�Ƃ��ď������ĕԂ�
        /// </summary>
        public static int Repeat(this int @this, int value, int max) => (@this + value + max) % max;


        /// ----------------------------------------------------------------------------
        // ������ւ̕ϊ�

        /// <summary>
        /// �w�肳�ꂽ������0���߂����������Ԃ�
        /// </summary>
        public static string ToString_ZeroFill(this int @this, int numberOfDigits) =>
            @this.ToString("D" + numberOfDigits);

        /// <summary>
        /// �w�肳�ꂽ�����̌Œ菬���_����t�������������Ԃ�
        /// </summary>
        /// <remarks>
        /// 123.FixedPoint(2) �� 123.00
        /// 123.FixedPoint(4) �� 123.0000
        /// </remarks>
        public static string ToString_FixedPoint(this int @this, int numberOfDigits) =>
            @this.ToString("F" + numberOfDigits);

        /// <summary>
        /// �w�肳�ꂽ
        /// </summary>
        public static string ToString_Separate(this int @this) =>
            string.Format("{0:#,0}", @this);

    }


}
