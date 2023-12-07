using System;
using System.Collections.Generic;
using UnityEngine;

// [�Q�l]
//  github: neuneu9/unity-gizmos-utility https://github.com/neuneu9/unity-gizmos-utility/blob/master/GizmosUtility.cs
//  github: code-beans/GizmoExtensions https://github.com/code-beans/GizmoExtensions/blob/master/src/GizmosExtensions.cs

namespace nitou {

    /// <summary>
    /// Gizmo�`��Ɋւ���ėp�@�\��񋟂��郉�C�u����
    /// </summary>
    public static class GizmoUtility {     

        /// ----------------------------------------------------------------------------
         #region 3D�}�`

        /// <summary>
        /// �L���[�u��`�悷��
        /// </summary>
        public static void DrawWireCube(Vector3 center, Vector3 size, Quaternion rotation = default) {

            var old = Gizmos.matrix;

            if (rotation.Equals(default)) {
                rotation = Quaternion.identity;
            }
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, size);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            
			Gizmos.matrix = old;
        }

        /// <summary>
        /// ����`�悷��
        /// </summary>
        public static void DrawArrow(Vector3 from, Vector3 to, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f) {
            Gizmos.DrawLine(from, to);
            var direction = to - from;
            var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawLine(to, to + right * arrowHeadLength);
            Gizmos.DrawLine(to, to + left * arrowHeadLength);
        }
        
        /// <summary>
        /// �X�t�B�A��`�悷��
        /// </summary>
        public static void DrawWireSphere(Vector3 center, float radius, Quaternion rotation = default) {

            var old = Gizmos.matrix;
            
			if (rotation.Equals(default))
                rotation = Quaternion.identity;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            Gizmos.DrawWireSphere(Vector3.zero, radius);
            
			Gizmos.matrix = old;
        }
        
        /// <summary>
        /// �~��`�悷��
        /// </summary>
        public static void DrawCircle(Vector3 center, float radius, int segments = 20, Quaternion rotation = default) {
            DrawWireArc(center, radius, 360, segments, rotation);
        }

		/// <summary>
		/// �~�ʂ�`�悷��
		/// </summary>
		public static void DrawWireArc(Vector3 center, float radius, float angle, int segments = 20, Quaternion rotation = default) {

			var old = Gizmos.matrix;

			Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
			Vector3 from = Vector3.forward * radius;
			var step = Mathf.RoundToInt(angle / segments);
			for (int i = 0; i <= angle; i += step) {
				var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad));
				Gizmos.DrawLine(from, to);
				from = to;
			}

			Gizmos.matrix = old;
		}

		/// <summary>
		/// �~�ʂ�`�悷��
		/// </summary>
		public static void DrawWireArc(Vector3 center, float radius, float angle, int segments, Quaternion rotation, Vector3 centerOfRotation) {

			var old = Gizmos.matrix;

			if (rotation.Equals(default)) {
				rotation = Quaternion.identity;
            }
			Gizmos.matrix = Matrix4x4.TRS(centerOfRotation, rotation, Vector3.one);
			var deltaTranslation = centerOfRotation - center;
			Vector3 from = deltaTranslation + Vector3.forward * radius;
			var step = Mathf.RoundToInt(angle / segments);
			for (int i = 0; i <= angle; i += step) {
				var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad)) + deltaTranslation;
				Gizmos.DrawLine(from, to);
				from = to;
			}

			Gizmos.matrix = old;
		}

		/// <summary>
		/// �~�ʂ�`�悷��
		/// </summary>
		public static void DrawWireArc(Matrix4x4 matrix, float radius, float angle, int segments) {
			
			var old = Gizmos.matrix;
			
			Gizmos.matrix = matrix;
			Vector3 from = Vector3.forward * radius;
			var step = Mathf.RoundToInt(angle / segments);
			for (int i = 0; i <= angle; i += step) {
				var to = new Vector3(radius * Mathf.Sin(i * Mathf.Deg2Rad), 0, radius * Mathf.Cos(i * Mathf.Deg2Rad));
				Gizmos.DrawLine(from, to);
				from = to;
			}

			Gizmos.matrix = old;
		}

		/// <summary>
		/// �V�����_�[��`�悷��
		/// </summary>
		public static void DrawWireCylinder(Vector3 center, float radius, float height, Quaternion rotation = default) {
			
			var old = Gizmos.matrix;
			
			if (rotation.Equals(default)) {
				rotation = Quaternion.identity;
            }
			Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
			var half = height / 2;

			//draw the 4 outer lines
			Gizmos.DrawLine(Vector3.right * radius - Vector3.up * half, Vector3.right * radius + Vector3.up * half);
			Gizmos.DrawLine(-Vector3.right * radius - Vector3.up * half, -Vector3.right * radius + Vector3.up * half);
			Gizmos.DrawLine(Vector3.forward * radius - Vector3.up * half, Vector3.forward * radius + Vector3.up * half);
			Gizmos.DrawLine(-Vector3.forward * radius - Vector3.up * half, -Vector3.forward * radius + Vector3.up * half);

			//draw the 2 cricles with the center of rotation being the center of the cylinder, not the center of the circle itself
			DrawWireArc(center + Vector3.up * half, radius, 360, 20, rotation, center);
			DrawWireArc(center + Vector3.down * half, radius, 360, 20, rotation, center);

			Gizmos.matrix = old;
		}

		/// <summary>
		/// �J�v�Z����`�悷��
		/// </summary>
		public static void DrawWireCapsule(Vector3 center, float radius, float height, Quaternion rotation = default) {

			var old = Gizmos.matrix;
			
			if (rotation.Equals(default)) {
				rotation = Quaternion.identity;
			}
			Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
			var half = height / 2 - radius;

			//draw cylinder base
			DrawWireCylinder(center, radius, height - radius * 2, rotation);

			// draw upper cap
			// do some cool stuff with orthogonal matrices
			var mat = Matrix4x4.Translate(center + rotation * Vector3.up * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.forward));
			DrawWireArc(mat, radius, 180, 20);
			mat = Matrix4x4.Translate(center + rotation * Vector3.up * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(90, Vector3.forward));
			DrawWireArc(mat, radius, 180, 20);

			// draw lower cap
			mat = Matrix4x4.Translate(center + rotation * Vector3.down * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.forward));
			DrawWireArc(mat, radius, 180, 20);
			mat = Matrix4x4.Translate(center + rotation * Vector3.down * half) * Matrix4x4.Rotate(rotation * Quaternion.AngleAxis(-90, Vector3.forward));
			DrawWireArc(mat, radius, 180, 20);

			Gizmos.matrix = old;
		}

		#endregion



		/// ----------------------------------------------------------------------------
		// Public Method

		// 
		private static int _circleVertexCount = 64;

		/// <summary>
		/// �~��`��(2D)
		/// </summary>
		/// <param name="center">���S�ʒu</param>
		/// <param name="radius">���a</param>
		public static void DrawWireCircle(Vector3 center, float radius) {
            DrawWireRegularPolygon(_circleVertexCount, center, Quaternion.identity, radius);
        }

        /// <summary>
        /// �����p�`��`��(2D)
        /// </summary>
        /// <param name="vertexCount">�p�̐�</param>
        /// <param name="center">���S�ʒu</param>
        /// <param name="radius">���a</param>
        public static void DrawWireRegularPolygon(int vertexCount, Vector3 center, float radius) {
            DrawWireRegularPolygon(vertexCount, center, Quaternion.identity, radius);
        }

        /// <summary>
        /// �~��`��(3D)
        /// </summary>
        /// <param name="center">���S�ʒu</param>
        /// <param name="rotation">��]</param>
        /// <param name="radius">���a</param>
        public static void DrawWireCircle(Vector3 center, Quaternion rotation, float radius) {
            DrawWireRegularPolygon(_circleVertexCount, center, rotation, radius);
        }

        /// <summary>
        /// �����p�`��`��(3D)
        /// </summary>
        /// <param name="vertexCount">�p�̐�</param>
        /// <param name="center">���S�ʒu</param>
        /// <param name="rotation">��]</param>
        /// <param name="radius">���a</param>
        public static void DrawWireRegularPolygon(int vertexCount, Vector3 center, Quaternion rotation, float radius) {
            if (vertexCount < 3) {
                return;
            }

            Vector3 previousPosition = Vector3.zero;

            // ��������1�X�e�b�v�̊p�x
            float step = 2f * Mathf.PI / vertexCount;
            // ���������J�n�p�x(�����Ȃ甼�X�e�b�v���炷)
            float offset = Mathf.PI * 0.5f + ((vertexCount % 2 == 0) ? step * 0.5f : 0f);

            for (int i = 0; i <= vertexCount; i++) {
                float theta = step * i + offset;

                float x = radius * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(theta);

                Vector3 nextPosition = center + rotation * new Vector3(x, y, 0f);

                if (i == 0) {
                    previousPosition = nextPosition;
                } else {
                    Gizmos.DrawLine(previousPosition, nextPosition);
                }

                previousPosition = nextPosition;
            }
        }

    }

}

