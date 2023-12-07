using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [�Q�l]
//  Kan�̃�����: �G�f�B�^�g���ŃI�u�W�F�N�g�𐶐������ꍇ�ɃV�[����ۑ�������@ https://kan-kikuchi.hatenablog.com/entry/Save_Scene

namespace nitou.Tools {

    /// <summary>
    /// �V�[����Ƀe���v���[�g�I�u�W�F�N�g�𐶐�����
    /// </summary>
    public static class GameObjectCreater {

        [MenuItem("Tools/MyCustom/Execute/Create Templete Game Obj")]
        public static void Create() {

            CreateSeparatingObjects();
            CreateGroundPlane();
        }

        /// <summary>
        /// �q�G�����L�[�����p�̃_�~�[�I�u�W�F�N�g�𐶐�����
        /// </summary>
        private static void CreateSeparatingObjects() {
            
            // �I�u�W�F�N�g���̐ݒ�
            var objNameList = new List<string>{
                "------------- Manager -------------",
                "------------- Scene -------------",
                "------------- Static geometry -------------",
                "------------- Characters -------------",
            };

            // �I�u�W�F�N�g�̐���
            foreach (var name in objNameList) {
                var newObj = new GameObject(name);
                Undo.RegisterCreatedObjectUndo(newObj, name);
            }
        }

        /// <summary>
        /// ���p�̕��ʃI�u�W�F�N�g�𐶐�����
        /// </summary>
        private static void CreateGroundPlane() {
            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Undo.RegisterCreatedObjectUndo(plane, "Create New GameObject");
            plane.transform.localScale = Vector3.one * 30;
        }

    }


}