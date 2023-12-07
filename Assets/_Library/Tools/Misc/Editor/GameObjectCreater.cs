using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [参考]
//  Kanのメモ帳: エディタ拡張でオブジェクトを生成した場合にシーンを保存する方法 https://kan-kikuchi.hatenablog.com/entry/Save_Scene

namespace nitou.Tools {

    /// <summary>
    /// シーン上にテンプレートオブジェクトを生成する
    /// </summary>
    public static class GameObjectCreater {

        [MenuItem("Tools/MyCustom/Execute/Create Templete Game Obj")]
        public static void Create() {

            CreateSeparatingObjects();
            CreateGroundPlane();
        }

        /// <summary>
        /// ヒエラルキー整理用のダミーオブジェクトを生成する
        /// </summary>
        private static void CreateSeparatingObjects() {
            
            // オブジェクト名の設定
            var objNameList = new List<string>{
                "------------- Manager -------------",
                "------------- Scene -------------",
                "------------- Static geometry -------------",
                "------------- Characters -------------",
            };

            // オブジェクトの生成
            foreach (var name in objNameList) {
                var newObj = new GameObject(name);
                Undo.RegisterCreatedObjectUndo(newObj, name);
            }
        }

        /// <summary>
        /// 床用の平面オブジェクトを生成する
        /// </summary>
        private static void CreateGroundPlane() {
            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Undo.RegisterCreatedObjectUndo(plane, "Create New GameObject");
            plane.transform.localScale = Vector3.one * 30;
        }

    }


}