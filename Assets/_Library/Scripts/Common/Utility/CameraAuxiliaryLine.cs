using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [�Q�l]
//  qiita: Unity�ō\�}�̂��߂̕⏕����\������ https://qiita.com/shirokuma1101/items/6af3f2bbeb62fd879c1d

namespace nitou {

    /// <summary>
    /// Camera�ɃA�^�b�`���č\�}�p�̕⏕����\������R���|�[�l���g
    /// </summary>
    public class CameraAuxiliaryLine : MonoBehaviour {

        /// <summary>
        /// �⏕���̕`����@
        /// </summary>
        private enum CompositionMode {
            Symmetrical, // �Ώ�
            Bisection,   // �񕪊�
            Thirds,      // �O����
            Diagonal,    // �Ίp��
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

        // �Ώ�
        private void DrawSymmetricalComposition(Camera camera) {
            // �c��
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0.5f, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(0.5f, 1, camera.nearClipPlane + 0.1f))
            );
        }

        // �񕪊��\�}
        private void DrawBisectionComposition(Camera camera) {
            // �c��
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0.5f, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(0.5f, 1, camera.nearClipPlane + 0.1f))
            );
            // ����
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0, 0.5f, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1, 0.5f, camera.nearClipPlane + 0.1f))
            );
        }

        // �O�����\�}
        private void DrawThirdsComposition(Camera camera) {
            // �c��
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(1.0f / 3.0f * 1, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1.0f / 3.0f * 1, 1, camera.nearClipPlane + 0.1f))
            );
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(1.0f / 3.0f * 2, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1.0f / 3.0f * 2, 1, camera.nearClipPlane + 0.1f))
            );
            // ����
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0, 1.0f / 3.0f * 1, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1, 1.0f / 3.0f * 1, camera.nearClipPlane + 0.1f))
            );
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0, 1.0f / 3.0f * 2, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1, 1.0f / 3.0f * 2, camera.nearClipPlane + 0.1f))
            );
        }

        // �Ίp��
        private void DrawDiagonalComposition(Camera camera) {
            // �c��
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane + 0.1f))
            );
            // ����
            Gizmos.DrawLine(
                camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane + 0.1f)),
                camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane + 0.1f))
            );
        }

    }


}