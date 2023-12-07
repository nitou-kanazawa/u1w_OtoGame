using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [�Q�l]
//  PG����: ��{�^�ɔ͈̓`�F�b�N�@�\��ǉ����� https://takap-tech.com/entry/2020/06/20/232208#IsInRangemin-max%E5%A4%89%E6%95%B0%E5%80%A4%E3%81%8C-min--max-%E3%81%AE%E7%AF%84%E5%9B%B2%E5%86%85%E3%81%8B%E7%A2%BA%E8%AA%8D%E3%81%99%E3%82%8B

namespace nitou {

    /// <summary>
    /// Generic�̊g�����\�b�h�N���X
    /// </summary>
    public static class GenericExtensions {

        /// ----------------------------------------------------------------------------
        // �l�̔���

        /// <summary>
        /// Null���ǂ����𔻒肷��
        /// </summary>
        public static bool IsNull<T>(this T @this, T replacementValue) => @this == null;

        /// <summary>
        /// �l���͈͓��ɂ��邩�ǂ����𔻒肷��
        /// </summary>
        public static bool IsInRange<T>(this T @this, T beginValue, T endValue) where T : IComparable {
            bool isBetween = (@this.CompareTo(beginValue) >= 0 && @this.CompareTo(endValue) <= 0);
            return isBetween;
        }

        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 


        /// ----------------------------------------------------------------------------
        // Public Method 


        /// ----------------------------------------------------------------------------
        // Private Method 
    }

}