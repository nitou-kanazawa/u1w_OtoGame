#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

// [�Q�l]
//  kan�̃�����: �w�肵���}�e���A��(Material)���t���Ă郌���_���[(Renderer)�̌�����u��������G�f�B�^�g�� https://kan-kikuchi.hatenablog.com/entry/MaterialSearcher

namespace nitou.Tools {

    /// <summary>
    /// �w�肵���}�e���A�����t���Ă郌���_���[�̌�����u��������E�C���h�E�g��
    /// �����݃V�[���̃q�G�����L�[���猟������
    /// </summary>
    public class MaterialSearcherWindow : EditorWindow {

        // �E�C���h�E�`��p
        private Vector2 _scrollPos;     // �X�N���[���ʒu

        // �}�e���A������p
        private Material _targetMaterial;           // �Ώۂ̃}�e���A��
        private Material _replaceTargetMaterial;    // �u����̃}�e���A��
        private readonly List<Renderer> _targetRenderers = new();   // �Ώۂ�Material���t���Ă郌���_���[


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        [MenuItem("Window/Nitou/Material/Material Searcher")]
        public static void Open() => GetWindow<MaterialSearcherWindow>("Material Searcher");

        private void OnGUI() {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUI.skin.scrollView);

            // �Ώۂ̃}�e���A���ݒ�
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            _targetMaterial = (Material)EditorGUILayout.ObjectField("�����Ώۂ�Material", _targetMaterial, typeof(Material), false);
            _replaceTargetMaterial = (Material)EditorGUILayout.ObjectField("�u���Ώۂ�Material", _replaceTargetMaterial, typeof(Material), false);
            EditorGUILayout.EndHorizontal();

            // �����A�u���{�^��
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            if (GUILayout.Button("����")) {
                Search();
            }
            if (GUILayout.Button("�u��")) {
                Replace();
            }
            EditorGUILayout.EndHorizontal();

            // �Ώۂ̃����_���[�\��
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("�Ώۂ�Material���t���Ă郌���_���[");
            foreach (var renderer in _targetRenderers) {
                // ���{�^������������Hierarchy��őI������
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
        /// �Ώۃ}�e���A���̌���
        /// </summary>
        private void Search() {
            _targetRenderers.Clear();

            // �V�[����̗L����Renderer���������A�Ώۂ̃}�e���A�����t���Ă���ΐݒ�
            foreach (var renderer in FindObjectsOfType<Renderer>()) {
                if (renderer.sharedMaterials.Any(material => material == _targetMaterial)) {
                    _targetRenderers.Add(renderer);
                }
            }

            // ���O���Ƀ\�[�g
            _targetRenderers.Sort((a, b) => String.CompareOrdinal(a.name, b.name));
        }

        /// <summary>
        /// �Ώۃ}�e���A���̒u��
        /// </summary>
        private void Replace() {
            Search();
            if (_targetRenderers.Count == 0) {
                EditorUtility.DisplayDialog("�u���Ώۂ̃����_���[������܂���", "�u���Ώۂ̃����_���[������܂���", "OK");
                return;
            }

            Undo.RecordObjects(_targetRenderers.ToArray(), "�}�e���A���̒u��");

            foreach (var targetRenderer in _targetRenderers) {
                Material[] newMaterials = targetRenderer.sharedMaterials;
                newMaterials[newMaterials.ToList().IndexOf(_targetMaterial)] = _replaceTargetMaterial;
                targetRenderer.materials = newMaterials;
            }

            EditorUtility.DisplayDialog($"�u�����������܂���", $"{_targetRenderers.Count}�̃����_���[�̒u�����s���܂���", "OK");
            _targetRenderers.Clear();
        }
    }


}
#endif