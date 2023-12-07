using System;

// 
// [参考]
//  ライブドアブログ: ジェネリクス型の比較方法 http://templatecreate.blog.jp/archives/30579779.html

namespace nitou {

    /// <summary>
    /// 列挙型の要素順に意味を持たせるためのラップクラス（Next,Previousへの遷移）
    /// </summary>
    class CountableEnum<T> where T : Enum {
        private Array values;   // 対象(列挙型)の全要素
        private int id;         // 現在値のインデックス

        // コンストラクタ
        public CountableEnum(T target) {
            values = Enum.GetValues(target.GetType());      // 列挙型の全要素
            id = GetId(target);                             // 指定要素のインデックス
        }

        // プロパティ
        public Type Type { get => Get(0).GetType(); }
        public T Head { get => Get(0); }
        public T Tail { get => Get(values.Length - 1); }
        public T Current { get => Get(id); }

        // 判定
        public bool IsHead { get => Get(id).Equals(Head); }
        public bool IsTail { get => Get(id).Equals(Tail); }

        // 比較
        public bool Is(T target) => Get(id).CompareTo(target) == 0;

        // 取得
        private T Get(int id) => (T)values.GetValue(id);
        // 
        private int GetId(T key) {
            for (int i = 0; i < values.Length; i++) {
                var value = Get(i);
                if (key.Equals(value)) return i;
            }
            throw new System.ArgumentException();
        }

        // 遷移
        public T MoveNext() {
            id = IsTail ? 0 : id + 1;   // 次の値に進める
            return Current;
        }
        public T MovePrevious() {
            id = IsHead ? values.Length - 1 : id - 1;   // 前の値に戻す
            return Current;
        }

        // デバッグ
        public override string ToString() {
            return string.Format("type:{0} [{1}-{2}]  current:{3}", Type, Head, Tail, Get(id));
        }
    }

}
