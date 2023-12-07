using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

// [参考] メッシュを反転する方法 https://edunity.hatenablog.com/entry/20201113/1605221523

namespace OtoGame.LevelObjects {

    public class NoteMeshController : MonoBehaviour, System.IDisposable{

        [TitleGroup("Graphics objects"), Indent]
        [SerializeField] private GameObject _sphereObj;

        [TitleGroup("Graphics objects"), Indent]
        [SerializeField] private GameObject _fishObj;


        // 球体
        private MeshFilter _sphereFilter;
        private MeshRenderer _sphereRenderer;

        // 魚モデル
        private MeshRenderer _fishRenderer;

        // マテリアル
        private Material _sphereMat;


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize() {

            // コンポーネント取得
            _sphereFilter = _sphereObj.GetComponent<MeshFilter>();
            _sphereRenderer = _sphereObj.GetComponent<MeshRenderer>();
            _fishRenderer = _fishObj.GetComponent<MeshRenderer>();

            // マテリアル
            _sphereMat = _sphereRenderer.material;

            // ノーツ球体のメッシュを反転させる
            Mesh Mesh = Instantiate(_sphereFilter.sharedMesh);
            Mesh.triangles = Mesh.triangles.Reverse().ToArray();
            _sphereFilter.sharedMesh = Mesh;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            Destroy(_sphereMat);
        }


        /// ----------------------------------------------------------------------------
        // Public Method (グラフィック操作)

        /// <summary>
        /// 球体のカラー設定
        /// </summary>
        public void SetSphereColor(Color color) {
            _sphereMat.color = color;
        }

        /// <summary>
        /// 魚モデルを表示する
        /// </summary>
        public void ShowFishModel() {
            _fishRenderer.enabled = true;
        }

        /// <summary>
        /// 魚モデルを非表示にする
        /// </summary>
        public void HideFishModel() {
            _fishRenderer.enabled = false;

        }


        /// ----------------------------------------------------------------------------
        // Private Method 
    }

}