using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// [�Q�l]
//  �R�K�l�u���O: Dictionary��foreach�Ŏg�����̋L�q���ȗ�������Deconstruction https://baba-s.hatenablog.com/entry/2019/09/03/231000

namespace nitou {

    /// <summary>
    /// Dictionary�̊g�����\�b�h�N���X
    /// </summary>
    public static class DictionaryExtensions {

        /// <summary>
        /// �f�R���X�g���N�g
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
        /// �w�肳�ꂽ�L�[���i�[����Ă���ꍇ��action���Ăяo���g�����\�b�h
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