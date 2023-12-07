#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

// [参考]
//  kanのメモ帳: 指定したマテリアル(Material)が付いてるレンダラー(Renderer)の検索や置換をするエディタ拡張 https://kan-kikuchi.hatenablog.com/entry/MaterialSearcher

namespace nitou.Tools {

    /// <summary>
    /// 指定したマテリアルが付いてるレンダラーの検索や置換をするウインドウ拡張
    /// ※現在シーンのヒエラルキーから検索する
    /// </summary>
    public class MaterialSearcherWindow : EditorWindow {

        // ウインドウ描画用
        private Vector2 _scrollPos;     // スクロール位置

        // マテリアル操作用
        private Material _targetMaterial;           // 対象のマテリアル
        private Material _replaceTargetMaterial;    // 置換後のマテリアル
        private readonly List<Renderer> _targetRenderers = new();   // 対象のMaterialが付いてるレンダラー


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        [MenuItem("Window/Nitou/Material/Material Searcher")]
        public static void Open() => GetWindow<MaterialSearcherWindow>("Material Searcher");

        private void OnGUI() {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUI.skin.scrollView);

            // 対象のマテリアル設定
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            _targetMaterial = (Material)EditorGUILayout.ObjectField("検索対象のMaterial", _targetMaterial, typeof(Material), false);
            _replaceTargetMaterial = (Material)EditorGUILayout.ObjectField("置換対象のMaterial", _replaceTargetMaterial, typeof(Material), false);
            EditorGUILayout.EndHorizontal();

            // 検索、置換ボタン
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            if (GUILayout.Button("検索")) {
                Search();
            }
            if (GUILayout.Button("置換")) {
                Replace();
            }
            EditorGUILayout.EndHorizontal();

            // 対象のレンダラー表示
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("対象のMaterialが付いてるレンダラー");
            foreach (var renderer in _targetRenderers) {
                // ※ボタンを押したらHierarchy上で選択する
                if (GUILayout.Button(renderer.name)) {
                    Selection.activeGameObject = renderer.gameObject;
                }
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 対象マテリアルの検索
        /// </summary>
        private void Search() {
            _targetRenderers.Clear();

            // シーン上の有効なRendererを検索し、対象のマテリアルが付いていれば設定
            foreach (var renderer in FindObjectsOfType<Renderer>()) {
                if (renderer.sharedMaterials.Any(material => material == _targetMaterial)) {
                    _targetRenderers.Add(renderer);
                }
            }

            // 名前順にソート
            _targetRenderers.Sort((a, b) => String.CompareOrdinal(a.name, b.name));
        }

        /// <summary>
        /// 対象マテリアルの置換
        /// </summary>
        private void Replace() {
            Search();
            if (_targetRenderers.Count == 0) {
                EditorUtility.DisplayDialog("置換対象のレンダラーがありません", "置換対象のレンダラーがありません", "OK");
                return;
            }

            Undo.RecordObjects(_targetRenderers.ToArray(), "マテリアルの置換");

            foreach (var targetRenderer in _targetRenderers) {
                Material[] newMaterials = targetRenderer.sharedMaterials;
                newMaterials[newMaterials.ToList().IndexOf(_targetMaterial)] = _replaceTargetMaterial;
                targetRenderer.materials = newMaterials;
            }

            EditorUtility.DisplayDialog($"置換が完了しました", $"{_targetRenderers.Count}個のレンダラーの置換を行いました", "OK");
            _targetRenderers.Clear();
        }
    }


}
#endif