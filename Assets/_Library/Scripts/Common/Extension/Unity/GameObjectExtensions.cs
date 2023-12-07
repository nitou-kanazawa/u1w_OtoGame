using System.Linq;
using UniRx;
using UnityEngine;

// [参考]
//  コガネブログ: GetComponentsInChildrenで自分自身を含まないようにする拡張メソッド https://baba-s.hatenablog.com/entry/2014/06/05/220224
//  qiita: ちょっとだけ便利になるかもしれない拡張メソッド集 https://qiita.com/tanikura/items/ed5d56ebbfcad19c488d

namespace nitou {

    /// <summary>
    /// GameObjectの拡張メソッドクラス
    /// </summary>
    public static partial class GameObjectExtensions {

        /// ----------------------------------------------------------------------------
        // コンポーネント（確認）

        /// <summary>
        /// 指定されたコンポーネントがアタッチされているかどうかを確認する拡張メソッド
        /// </summary>
        public static bool HasComponent<T>(this GameObject @this) 
            where T : Component {
            return @this.GetComponent<T>();
        }

        /// <summary>
        /// 指定されたコンポーネントがアタッチされているかどうかを確認する拡張メソッド
        /// </summary>
        public static bool HasComponent(this GameObject @this, System.Type type) {
            return @this.GetComponent(type);
        }


        /// ----------------------------------------------------------------------------
        // コンポーネント（削除）

        /// <summary>
        /// 指定されたコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveComponent<T>(this GameObject @this)
            where T : Component {
            T component = @this.GetComponent<T>();
            if (component != null) Object.Destroy(component);
            return @this;
        }

        /// <summary>
        /// 指定されたコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveComponent<T1, T2>(this GameObject @this)
            where T1 : Component where T2 : Component {
            @this.RemoveComponent<T1>();
            @this.RemoveComponent<T2>();
            return @this;
        }

        /// <summary>
        /// 指定されたコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveComponent<T1, T2, T3>(this GameObject @this)
            where T1 : Component where T2 : Component where T3 : Component {
            @this.RemoveComponent<T1, T2>();
            @this.RemoveComponent<T3>();
            return @this;
        }

        /// <summary>
        /// 指定されたコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveComponent<T1, T2, T3, T4>(this GameObject @this)
            where T1 : Component where T2 : Component where T3 : Component where T4 : Component {
            @this.RemoveComponent<T1, T2, T3>();
            @this.RemoveComponent<T4>();
            return @this;
        }


        /// ----------------------------------------------------------------------------
        // コンポーネント（追加）

        /// <summary>
        /// 指定されたコンポーネントを追加する拡張メソッド
        /// </summary>
        public static GameObject AddComponents<T1, T2>(this GameObject @this) 
            where T1 : Component where T2 : Component {
            @this.AddComponent<T1>();
            @this.AddComponent<T2>();
            return @this;
        }


        /// ----------------------------------------------------------------------------
        // コンポーネント（取得）

        /// <summary>
        /// 自分自身を含まないGetComponentsInChaidrenの拡張メソッド
        /// </summary>
        public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject @this) 
            where T : Component {
            return @this.GetComponentsInChildren<T>().Where(c => @this != c.gameObject).ToArray();
        }

        /// <summary>
        /// 対象のコンポーネント持つ場合はそれを取得し，なければ追加して返す拡張メソッド
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject @this) 
            where T : Component {
            var component = @this.GetComponent<T>();
            return component ?? @this.AddComponent<T>();
        }


        /// ----------------------------------------------------------------------------
        // コンポーネント（有効状態）

        /// <summary>
        /// 指定されたコンポーネントを有効化する拡張メソッド
        /// </summary>
        public static GameObject EnableComponent<T>(this GameObject @this) 
            where T : Behaviour {
            if (@this.HasComponent<T>()) {
                @this.GetComponent<T>().enabled = true;
            }
            return @this;
        }

        /// <summary>
        /// 指定されたコンポーネントを非有効化する拡張メソッド
        /// </summary>
        public static GameObject DisableComponent<T>(this GameObject @this
            ) 
            where T : Behaviour {
            if (@this.HasComponent<T>()) {
                @this.GetComponent<T>().enabled = false;
            }
            return @this;
        }


        /// ----------------------------------------------------------------------------
        // 複製

        /// <summary>
        /// 対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject Instantiate(this GameObject @this) {
            return Object.Instantiate(@this);
        }

        /// <summary>
        /// 生成後に親となるTransformを指定して、対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject Instantiate(this GameObject @this, Transform parent) {
            return Object.Instantiate(@this, parent);
        }

        /// <summary>
        /// 生成後の座標及び姿勢を指定して、対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject Instantiate(this GameObject @this, Vector3 pos, Quaternion rot) {
            return Object.Instantiate(@this, pos, rot);
        }

        /// <summary>
        /// 生成後に親となるTransform、また生成後の座標及び姿勢を指定して、対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject Instantiate(this GameObject @this, Vector3 pos, Quaternion rot, Transform parent) {
            return Object.Instantiate(@this, pos, rot, parent);
        }

        /// <summary>
        /// 生成後に親となるTransform、また生成後のローカル座標を指定して、対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject InstantiateWithLocalPosition(this GameObject @this, Transform parent, Vector3 localPos) {
            var instance = Object.Instantiate(@this, parent);
            instance.transform.localPosition = localPos;
            return instance;
        }


        /// ----------------------------------------------------------------------------
        // アクティブ状態

        /// <summary>
        /// アクティブ状態の切り替え設定を行う拡張メソッド
        /// </summary>
        public static System.IDisposable SetActiveSelfSource(this GameObject @this, System.IObservable<bool> source, bool invert = false) {
            return source
                .Subscribe(x => {
                    x = invert ? !x : x;
                    @this.SetActive(x);
                })
                .AddTo(@this);
        }


        /// ----------------------------------------------------------------------------
        // 破棄

        /// <summary>
        /// Destroyの拡張メソッド
        /// </summary>
        public static void Destroy(this GameObject @this) {
            Object.Destroy(@this);
        }

        /// <summary>
        /// DontDestroyOnLoadの拡張メソッド
        /// </summary>
        public static void DontDestroyOnLoad(this GameObject @this) {
            Object.DontDestroyOnLoad(@this);
        }



        /// ----------------------------------------------------------------------------
        // レイヤー

        /// <summary>
        /// 対象のレイヤーに含まれているかを調べる拡張メソッド
        /// </summary>
        public static bool IsInLayerMask(this GameObject @this, LayerMask layerMask) {
            int objLayerMask = (1 << @this.layer);
            return (layerMask.value & objLayerMask) > 0;
        }

    }




}