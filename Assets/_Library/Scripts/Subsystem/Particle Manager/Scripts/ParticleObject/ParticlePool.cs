using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Toolkit;


// [参考]
//  Hatena: パーティクル再利用クラス https://www.stmn.tech/entry/2016/03/01/004816 
//  qiita: UniRxのObjectPoolを使ってParticleSystemを管理する https://qiita.com/KeichiMizutani/items/fc22a6037447d840adc2
//  kanのメモ帳: UniRxでオブジェクトプール(ObjectPool)を簡単実装 https://kan-kikuchi.hatenablog.com/entry/UniRx_ObjectPool


namespace nitou.Particle {

    /// <summary>
    /// パーティクルのオブジェクトプール
    /// </summary>
    public class ParticlePool : ObjectPool<ParticleObject> {

        private ParticleObject _prefab;         // プレハブ
        private Transform _parentTransform;     // 複製したオブジェクトの親

        /// <summary>
        /// パーティクル名
        /// </summary>
        public string ParticleName => _particleName;
        private readonly string _particleName;


        /// ----------------------------------------------------------------------------
        // Override Method 
        
        /// <summary>
        /// インスタンスを生成する
        /// </summary>
        protected override ParticleObject CreateInstance() {
            if (_prefab == null) SetDefalutPrefab();
            return Object.Instantiate(_prefab, _parentTransform, true);
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// コンストラクタ
        public ParticlePool(Transform transform, ParticleObject origin, string particleName = "") {
            this._parentTransform = transform;
            this._prefab = origin;
            this._particleName = particleName;
        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// デフォルトのパーティクルを登録
        /// </summary>
        private void SetDefalutPrefab() {
            var obj = new GameObject($"Defalut Particle");
            _prefab = obj.AddComponent<ParticleObject>();
        }

    }

}