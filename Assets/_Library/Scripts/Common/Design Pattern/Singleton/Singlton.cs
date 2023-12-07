using UnityEngine;

// [参考]
//  Hatena: C#でGenericなSingleton https://waken.hatenablog.com/entry/2016/03/05/102928

namespace nitou.DesiginPattern {

    /// <summary>
    /// シンプルなシングルトン (※実装サンプル)
    /// </summary>
    public class Singlton {

        private static Singlton instance = new();
        public static Singlton Instance => instance;

        private Singlton() { }
    }


    /// <summary>
    /// ジェネリックなシングルトン
    /// </summary>
    public class Singleton<T> where T : class, new() {

        private static readonly T _instance = new();
        public static T Instance => _instance;
        
        // 万一、外からコンストラクタを呼ばれたときに、ここで引っ掛ける
        protected Singleton() {
            Debug.Assert(null == _instance);
        }

    }
}