using UnityEngine;
using UnityEngine.UI;

// [参考]
//  ゲームUIネット : DOTweenで作成したモーション17個を含むプロジェクトを公開 https://game-ui.net/?p=975

namespace nitou {

    /// <summary>
    /// 
    /// </summary>
    public static partial class ColorExtensions {

        /// <summary>
        /// α値を設定する拡張メソッド
        /// </summary>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        /// <summary>
        /// α値を設定する拡張メソッド
        /// </summary>
        public static void SetAlpha(this Graphic graphic, float alpha) {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }

        /// <summary>
        /// α値を設定する拡張メソッド
        /// </summary>
        public static void SetAlphasInChildren(this GameObject obj, float alpha) {
            var spriteRenderers = obj.GetComponentsInChildren<SpriteRenderer>();
            var graphics = obj.GetComponentsInChildren<Graphic>();

            if (spriteRenderers != null) {
                foreach (var spriteRenderer in spriteRenderers) {
                    spriteRenderer.SetAlpha(alpha);
                }
            }

            if (graphics != null) {
                foreach (var graphic in graphics) {
                    graphic.SetAlpha(alpha);
                }
            }
        }
    }
}