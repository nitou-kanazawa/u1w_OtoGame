using UnityEngine;

// [参考]
//  qiita: シングルトンを使ってみよう　https://qiita.com/Teach/items/c146c7939db7acbd7eee
//  kanのメモ帳: MonoBehaviourを継承したシングルトン https://kan-kikuchi.hatenablog.com/entry/SingletonMonoBehaviour

namespace nitou.DesiginPattern {

    /// <summary>
    /// MonoBehaviourを継承したシングルトン
    /// ※DontDestroyOnLoadは呼ばない（派生クラス側で行う）
    /// </summary>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

        // 
        private static T instance;
        public static T Instance {
            get {
                if (instance == null) {
                    // シーン内からインスタンスを取得
                    instance = (T)FindObjectOfType(typeof(T));      // ※Findで参照しているため,Awakeのタイミングにも参照可能!

                    // シーン内に存在しない場合はエラー
                    if (instance == null) {
                        Debug.LogError(typeof(T) + " をアタッチしているGameObjectはありません");
                    }
                }
                return instance;
            }
        }

        // 
        protected virtual void Awake() {
            // 既にインスタンスが存在するなら破棄
            if (!CheckInstance())
                Destroy(this.gameObject);
        }

        /// <summary>
        /// 他のゲームオブジェクトにアタッチされているか調べる
        /// アタッチされている場合は破棄する。
        /// </summary>
        protected bool CheckInstance() {
            // 存在しない（or自分自身）場合
            if (instance == null) {
                instance = this as T;
                return true;
            } else if (Instance == this) {
                return true;
            }
            // 既に存在する場合
            return false;
        }
    }

}