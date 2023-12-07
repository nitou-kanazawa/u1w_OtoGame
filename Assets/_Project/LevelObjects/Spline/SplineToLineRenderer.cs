using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Sirenix.OdinInspector;

// [参考]
//  Hatena: UnityのSplinesを使って動的に生成される曲線の道を作る https://ayousanz.hatenadiary.jp/entry/Unity%E3%81%AESpline%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%A6%E5%8B%95%E7%9A%84%E3%81%AB%E7%94%9F%E6%88%90%E3%81%95%E3%82%8C%E3%82%8B%E6%9B%B2%E7%B7%9A%E3%81%AE%E9%81%93%E3%82%92%E4%BD%9C%E3%82%8B

namespace OtoGame.LevelObjects {

    /// <summary>
    /// スプラインに沿って線を描画するコンポーネント
    /// </summary>
    [RequireComponent(typeof(SplineContainer), typeof(LineRenderer))]
    public class SplineToLineRenderer : MonoBehaviour {

        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private LineRenderer _lineRenderer;

        // セグメントの分割数
        [SerializeField] private int _segments = 4;


        private bool _isDirty;

        private void OnEnable() {
            // スプラインの更新時にLineRendererにパスを反映する設定
            _splineContainer.Spline.changed += Rebuild;

            // 初期化時は必ず反映
            Rebuild();
        }

        private void OnDisable() {
            _splineContainer.Spline.changed -= Rebuild;
        }

        private void Update() {
            // ワールド空間での描画の場合、Transformの更新もチェックしておく
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
        /// スプラインからLineRendererにパスを反映する 
        /// </summary>
        [Button]
        public void Rebuild() {
            // テンポラリバッファを確保
            var points = new NativeArray<Vector3>(_segments, Allocator.Temp);

            // スプラインの読み取り専用情報
            using var spline = new NativeSpline(
                _splineContainer.Spline,
                _lineRenderer.useWorldSpace
                    ? _splineContainer.transform.localToWorldMatrix
                    : float4x4.identity
            );

            float total = _segments - 1;

            // セグメント数だけ線分を作成
            for (var i = 0; i < _segments; ++i) {
                points[i] = spline.EvaluatePosition(i / total);
            }

            // LineRendererに点情報を反映
            _lineRenderer.positionCount = _segments;
            _lineRenderer.SetPositions(points);

            // バッファを解放
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