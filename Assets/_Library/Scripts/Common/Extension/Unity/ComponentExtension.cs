using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [参考]
//  qiita: Unityで使える便利関数(拡張メソッド)達 https://qiita.com/nmss208/items/9846525cf523fb961b48

namespace nitou {

    /// <summary>
    /// Componentの拡張メソッドクラス
    /// </summary>
    public static partial class ComponentExtensions {

        /// ----------------------------------------------------------------------------
        // 複製

        /// <summary>
        /// GameObjectが対象のコンポーネント持つ場合はそれを取得し，なければ追加して返す拡張メソッド
        /// </summary>
        public static T GetOrAddComponent<T>(this Component @this) where T : Component {
            return GameObjectExtensions.GetOrAddComponent<T>(@this.gameObject);
        }

        /// ----------------------------------------------------------------------------
        // 破棄

        public static void Destroy(this Component @this) {
            Object.Destroy(@this);
        }

        /// <summary>
        /// ComponentがアタッチされているGameObjectを破棄する
        /// </summary>
        public static void DestroyGameObject(this Component @this) {
            Object.Destroy(@this.gameObject);
        }


        /// ----------------------------------------------------------------------------
        // レイヤー

        /// <summary>
        /// GameObjectが対象のレイヤーに含まれているかを調べる拡張メソッド
        /// </summary>
        public static bool IsInLayerMask(this Component component, LayerMask layerMask) {
            return GameObjectExtensions.IsInLayerMask(component.gameObject, layerMask);
        }
    }

}