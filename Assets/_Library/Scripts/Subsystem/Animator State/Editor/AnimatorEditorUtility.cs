using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEditor;
using System.Linq;

// [�Q�l]
//  Hatena: AnimatorController�̌��݂̃X�e�[�g�����擾������X�e�[�g�̐؂�ւ����Ď������肷��d�g�݂���� https://light11.hatenadiary.com/search?q=StateMachineBehaviour


namespace nitou {

    /// <summary>
    /// 
    /// </summary>
    public static class AnimatorEditorUtility {

        // �I�𒆂̃I�u�W�F�N�g
        private static string _selectedObjectPath = null;

        /// <summary>
        /// �I�����O�ꂽ����AnimatorStateEvent���X�V����
        /// </summary>
        [InitializeOnLoadMethod]
        private static void SetupAnimatorStateEventOnDeselect() {

            // ��AnimatorController�₻�̃T�u�A�Z�b�g�iLayer��State�j�̑I�����������ꂽ�Ƃ��Ɏ��s
            Selection.selectionChanged += () => {
                if (!string.IsNullOrEmpty(_selectedObjectPath) && _selectedObjectPath.EndsWith(".controller")) {
                    var animatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(_selectedObjectPath);
                    SetupAnimatorStateEvent(animatorController);
                }
                _selectedObjectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            };
        }

        /// <summary>
        /// AnimatorStateEvent���Z�b�g�A�b�v����
        /// </summary>
        public static void SetupAnimatorStateEvent(AnimatorController animatorController) {

            for (int i = 0; i < animatorController.layers.Length; i++) {
                var layer = animatorController.layers[i];
                var rootStateMachine = layer.stateMachine;

                // ���C���[��StateMachineBehaviour���A�^�b�`����
                var animatorStateEvent = layer.stateMachine.behaviours.FirstOrDefault(x => x is AnimatorStateEvent);
                if (animatorStateEvent == null) {
                    animatorStateEvent = layer.stateMachine.AddStateMachineBehaviour<AnimatorStateEvent>();
                }

                var so = new SerializedObject(animatorStateEvent);
                so.Update();
                so.FindProperty("_layer").intValue = i;
                var statesProperty = so.FindProperty("_stateFullPaths");

                // �T�u�X�e�[�g�}�V�����܂߂��S�ẴX�e�[�g�}�V�����擾
                var allStatesAndFullPaths = new List<(AnimatorState state, string fullPath)>();
                GetAllStatesAndFullPaths(rootStateMachine, null, allStatesAndFullPaths);

                // �X�e�[�g�̃t���p�X���i�[����
                statesProperty.arraySize = allStatesAndFullPaths.Count;
                for (int j = 0; j < statesProperty.arraySize; j++) {
                    statesProperty.GetArrayElementAtIndex(j).stringValue = allStatesAndFullPaths[j].fullPath;
                }

                so.ApplyModifiedProperties();
                so.Dispose();
            }
        }

        /// <summary>
        /// �S�ẴX�e�[�g�Ƃ��̃t���p�X���擾����
        /// </summary>
        private static void GetAllStatesAndFullPaths(AnimatorStateMachine stateMachine, string parentPath, List<(AnimatorState state, string fullPath)> result) {
            if (!string.IsNullOrEmpty(parentPath)) {
                parentPath += ".";
            }
            parentPath += stateMachine.name;

            // �S�ẴX�e�[�g������
            foreach (var state in stateMachine.states) {
                var stateFullPath = $"{parentPath}.{state.state.name}";
                result.Add((state.state, stateFullPath));
            }

            // �T�u�X�e�[�g�}�V�����ċA�I�ɏ���
            foreach (var subStateMachine in stateMachine.stateMachines) {
                GetAllStatesAndFullPaths(subStateMachine.stateMachine, parentPath, result);
            }
        }
    }

}