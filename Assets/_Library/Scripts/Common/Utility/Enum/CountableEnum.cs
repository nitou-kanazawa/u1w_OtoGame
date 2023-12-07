using System;

// 
// [�Q�l]
//  ���C�u�h�A�u���O: �W�F�l���N�X�^�̔�r���@ http://templatecreate.blog.jp/archives/30579779.html

namespace nitou {

    /// <summary>
    /// �񋓌^�̗v�f���ɈӖ����������邽�߂̃��b�v�N���X�iNext,Previous�ւ̑J�ځj
    /// </summary>
    class CountableEnum<T> where T : Enum {
        private Array values;   // �Ώ�(�񋓌^)�̑S�v�f
        private int id;         // ���ݒl�̃C���f�b�N�X

        // �R���X�g���N�^
        public CountableEnum(T target) {
            values = Enum.GetValues(target.GetType());      // �񋓌^�̑S�v�f
            id = GetId(target);                             // �w��v�f�̃C���f�b�N�X
        }

        // �v���p�e�B
        public Type Type { get => Get(0).GetType(); }
        public T Head { get => Get(0); }
        public T Tail { get => Get(values.Length - 1); }
        public T Current { get => Get(id); }

        // ����
        public bool IsHead { get => Get(id).Equals(Head); }
        public bool IsTail { get => Get(id).Equals(Tail); }

        // ��r
        public bool Is(T target) => Get(id).CompareTo(target) == 0;

        // �擾
        private T Get(int id) => (T)values.GetValue(id);
        // 
        private int GetId(T key) {
            for (int i = 0; i < values.Length; i++) {
                var value = Get(i);
                if (key.Equals(value)) return i;
            }
            throw new System.ArgumentException();
        }

        // �J��
        public T MoveNext() {
            id = IsTail ? 0 : id + 1;   // ���̒l�ɐi�߂�
            return Current;
        }
        public T MovePrevious() {
            id = IsHead ? values.Length - 1 : id - 1;   // �O�̒l�ɖ߂�
            return Current;
        }

        // �f�o�b�O
        public override string ToString() {
            return string.Format("type:{0} [{1}-{2}]  current:{3}", Type, Head, Tail, Get(id));
        }
    }

}
