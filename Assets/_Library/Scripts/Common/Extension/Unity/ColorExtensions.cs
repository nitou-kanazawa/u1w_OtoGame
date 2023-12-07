using UnityEngine;
using UnityEngine.UI;

// [�Q�l]
//  �Q�[��UI�l�b�g : DOTween�ō쐬�������[�V����17���܂ރv���W�F�N�g�����J https://game-ui.net/?p=975

namespace nitou {

    /// <summary>
    /// 
    /// </summary>
    public static partial class ColorExtensions {

        /// <summary>
        /// ���l��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        /// <summary>
        /// ���l��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetAlpha(this Graphic graphic, float alpha) {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }

        /// <summary>
        /// ���l��ݒ肷��g�����\�b�h
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