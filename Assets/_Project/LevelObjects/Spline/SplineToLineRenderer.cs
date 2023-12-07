using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Sirenix.OdinInspector;

// [�Q�l]
//  Hatena: Unity��Splines���g���ē��I�ɐ��������Ȑ��̓������ https://ayousanz.hatenadiary.jp/entry/Unity%E3%81%AESpline%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%A6%E5%8B%95%E7%9A%84%E3%81%AB%E7%94%9F%E6%88%90%E3%81%95%E3%82%8C%E3%82%8B%E6%9B%B2%E7%B7%9A%E3%81%AE%E9%81%93%E3%82%92%E4%BD%9C%E3%82%8B

namespace OtoGame.LevelObjects {

    /// <summary>
    /// �X�v���C���ɉ����Đ���`�悷��R���|�[�l���g
    /// </summary>
    [RequireComponent(typeof(SplineContainer), typeof(LineRenderer))]
    public class SplineToLineRenderer : MonoBehaviour {

        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private LineRenderer _lineRenderer;

        // �Z�O�����g�̕�����
        [SerializeField] private int _segments = 4;


        private bool _isDirty;

        private void OnEnable() {
            // �X�v���C���̍X�V����LineRenderer�Ƀp�X�𔽉f����ݒ�
            _splineContainer.Spline.changed += Rebuild;

            // ���������͕K�����f
            Rebuild();
        }

        private void OnDisable() {
            _splineContainer.Spline.changed -= Rebuild;
        }

        private void Update() {
            // ���[���h��Ԃł̕`��̏ꍇ�ATransform�̍X�V���`�F�b�N���Ă���
            if (_lineRenderer.useWorldSpace && !_isDirty) {
                var splineTransform = _splineContainer.transform;
                _isDirty = splineTransform.hasChanged;
                splineTransform.hasChanged = false;
            }

            if (_isDirty)
                Rebuild();
        }

        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �X�v���C������LineRenderer�Ƀp�X�𔽉f���� 
        /// </summary>
        [Button]
        public void Rebuild() {
            // �e���|�����o�b�t�@���m��
            var points = new NativeArray<Vector3>(_segments, Allocator.Temp);

            // �X�v���C���̓ǂݎ���p���
            using var spline = new NativeSpline(
                _splineContainer.Spline,
                _lineRenderer.useWorldSpace
                    ? _splineContainer.transform.localToWorldMatrix
                    : float4x4.identity
            );

            float total = _segments - 1;

            // �Z�O�����g�������������쐬
            for (var i = 0; i < _segments; ++i) {
                points[i] = spline.EvaluatePosition(i / total);
            }

            // LineRenderer�ɓ_���𔽉f
            _lineRenderer.positionCount = _segments;
            _lineRenderer.SetPositions(points);

            // �o�b�t�@�����
            points.Dispose();

            _isDirty = false;
        }


        /// ----------------------------------------------------------------------------
        // Editor
#if UNITY_EDITOR
        private void OnValidate() {
            _splineContainer = GetComponent<SplineContainer>();
            _lineRenderer = GetComponent<LineRenderer>();

            Rebuild();
        }
#endif
    }

}