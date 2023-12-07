using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// [�Q�l]
//  Hatena Blog: Action, Func, Predicate�f���Q�[�g���g���Ă݂� https://oooomincrypto.hatenadiary.jp/entry/2022/04/24/201149
//  JojoBase: �g�����\�b�h�͍���Ē��߂Ă����ƕ֗��ł� https://johobase.com/custom-extension-methods-list/#i-5
//  qiita: ����Ƃ�����ƕ֗��Ȋg�����\�b�h�Љ� https://qiita.com/s_mino_ri/items/0fd2e2b3cebb7a62ad46

namespace nitou {

    /// <summary>
    /// Collection�̊g�����\�b�h�N���X
    /// </summary>
    public static partial class CollectionExtensions {

        /// ----------------------------------------------------------------------------
        // �v�f�̔���

        /// <summary>
        /// �R���N�V������Null�܂��͋󂩂ǂ����𔻒肷��g�����\�b�h
        /// </summary>
        public static bool IsNullOrEmpty(this IList @this) =>
            @this == null || @this.Count == 0;


        /// ----------------------------------------------------------------------------
        // �v�f�̒ǉ��E�폜

        /// <summary>
        /// �w�肵�����������𖞂����ꍇ�ɍ��ڂ�ǉ�����g�����\�b�h
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
        /// �w�肵�����������𖞂����ꍇ�ɍ��ڂ��폜����g�����\�b�h
        /// </summary>
        public static void RemoveIf<T>(this ICollection<T> @this, Predicate<T> predicate, T item) {
            if (predicate(item)) {
                @this.Remove(item);
            }
        }


        /// ----------------------------------------------------------------------------
        // �v�f�̎擾

        /// <summary>
        /// �����_���ɗv�f���擾����g�����\�b�h
        /// </summary>
        public static T GetRandom<T>(this IList<T> @this) =>
            @this[UnityEngine.Random.Range(0, @this.Count)];


        /// ----------------------------------------------------------------------------
        // �v�f�̑���

        /// <summary>
        /// IEnumerable �̊e�v�f�ɑ΂��āA�w�肳�ꂽ���������s����g�����\�b�h
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action) {
            foreach (var n in source.Select((val, index) => new { val, index })) {
                action(n.val, n.index);
            }
        }

    }
}