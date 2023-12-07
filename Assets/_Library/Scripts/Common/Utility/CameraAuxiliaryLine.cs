using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [参考]
//  qiita: Unityで構図のための補助線を表示する https://qiita.com/shirokuma1101/items/6af3f2bbeb62fd879c1d

namespace nitou {

    /// <summary>
    /// Cameraにアタッチして構図用の補助線を表示するコンポーネント
    /// </summary>
    public class CameraAuxiliaryLine : MonoBehaviour {

        /// <summary>
        /// 補助線の描画方法
        /// </summary>
        private enum CompositionMode {
            Symmetrical, // 対称
            Bisection,   // 二分割
            Thirds,      // 三分割
            Diagonal,    // 対角線
        }

        [SerializeField] private CompositionMode compositionMode = CompositionMode.Symmetrical;

        [SerializeField] private Color _gizmoColor = Color.red;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            var camera = gameObject.GetComponent<Camera>();
            Gizmos.color = _gizmoColor;

            switch (compositionMode) {
                case CompositionMode.Symmetrical:
                    DrawSymmetricalComposition(camera);
                    break;
                case CompositionMode.Bisection:
                    DrawBisectionComposition(camera);
                    break;
                case CompositionMode.Thirds:
                    DrawThirdsComposition(camera);
                    break;
                case CompositionMode.Diagonal:
                    DrawDiagonalComposition(camera);
                    break;
            }
        }
#endif

        /// ----------------------------------------------------------------------------
        // Private Method

        // 対称
        private void DrawSymmetricalComposition(Camera camera) {
            // 縦線
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0.5f, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(0.5f, 1, camera.nearClipPlane + 0.1f))
            );
        }

        // 二分割構図
        private void DrawBisectionComposition(Camera camera) {
            // 縦線
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0.5f, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(0.5f, 1, camera.nearClipPlane + 0.1f))
            );
            // 横線
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0, 0.5f, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane + 0.1f))
            );
        }

        // 三分割構図
        private void DrawThirdsComposition(Camera camera) {
            // 縦線
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(1.0f / 3.0f * 1, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1.0f / 3.0f * 1, 1, camera.nearClipPlane + 0.1f))
            );
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(1.0f / 3.0f * 2, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1.0f / 3.0f * 2, 1, camera.nearClipPlane + 0.1f))
            );
            // 横線
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0, 1.0f / 3.0f * 1, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1, 1.0f / 3.0f * 1, camera.nearClipPlane + 0.1f))
            );
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0, 1.0f / 3.0f * 2, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1, 1.0f / 3.0f * 2, camera.nearClipPlane + 0.1f))
            );
        }

        // 対角線
        private void DrawDiagonalComposition(Camera camera) {
            // 縦線
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane + 0.1f))
            );
            // 横線
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane + 0.1f))
            );
        }

    }


}