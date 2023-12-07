using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

// [参考]
//  Hatena: パーティクル再利用クラス https://www.stmn.tech/entry/2016/03/01/004816 
//  qiita: UniRxのObjectPoolを使ってParticleSystemを管理する https://qiita.com/KeichiMizutani/items/fc22a6037447d840adc2
//  kanのメモ帳: UniRxでオブジェクトプール(ObjectPool)を簡単実装 https://kan-kikuchi.hatenablog.com/entry/UniRx_ObjectPool

namespace nitou.Particle {
using nitou.DesiginPattern;

    /// <summary>
    /// パーティクルを管理するグローバルマネージャ
    /// （※小さなプロジェクトまたはプロトタイプとしての使用を想定）
    /// </summary>
    public class ParticleManager : MonoBehaviour {
    //public class ParticleManager : SingletonMonoBehaviour<ParticleManager> {

        // 各パーティクルプールのリスト
        private List<ParticlePool> _poolList = new();
        private List<GameObject> _containerObjects = new();

        // リソース情報
        private const string RESOUCE_PATH = "Particles/";

        /// <summary>
        /// イベントチャンネル
        /// </summary>
        [SerializeField] private ParticleEventChannelSO _eventChannel = null;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void Start() {
            _eventChannel.OnEventRaised += PlayParticleInternal;            
        }

        private void OnDestroy() {
            _eventChannel.OnEventRaised -= PlayParticleInternal;

            // 破棄されたとき（Disposeされたとき）にObjectPoolを解放する
            ClearList();
            _poolList = null;
            _containerObjects = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (解放)

        /// <summary>
        /// オブジェクトプールのリスト解放
        /// </summary>
        public void ClearList() {

            // プールの解放
            _poolList.ForEach(p => p.Dispose());
            _poolList.Clear();

            // 親オブジェクトの解放
            _containerObjects.ForEach(o => o.Destroy());
            _containerObjects.Clear();
        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// 指定した名前のパーティクル再生 (※内部呼び出し用)
        /// </summary>
        private void PlayParticleInternal(ParticleRequestData data) {

            // プールの取得
            ParticlePool pool = _poolList.Where(p => p.ParticleName == data.particleName).FirstOrDefault();

            // プールが未生成の場合，
            if (pool == null) {

                // 格納用の親オブジェクトを生成 (※デバッグ可視化用)
                var parentObj = new GameObject($"Pool [{data.particleName}]");
                parentObj.SetParent(this.transform);
                _containerObjects.Add(parentObj);

                // 生成元のオブジェクトを取得
                var prefab = LoadOrCreateOrigin(data.particleName).GetOrAddComponent<ParticleObject>();

                // プールの生成
                pool = new ParticlePool(parentObj.transform, prefab, data.particleName);
                _poolList.Add(pool);
            }

            // オブジェクトの取得
            var effect = pool.Rent();


            // エフェクトの再生
            var diposable = data.withEvent ?
                effect.PlayParticleWithEvent(data.position, data.rotation) :  // ※イベント処理あり
                effect.PlayParticle(data.position, data.rotation);            // ※イベント処理なし

            // ※再生終了でpoolに返却
            diposable.Subscribe(_ => { pool.Return(effect); });     // ※再生終了でpoolに返却
        }

        /// <summary>
        /// 生成元のオブジェクトをロード
        /// ※ロード失敗時はデフォルトのパーティクルを生成
        /// </summary>
        private GameObject LoadOrCreateOrigin(string particleName) {
            // リソースの読み込み
            var origin = Resources.Load(RESOUCE_PATH + particleName) as GameObject;

            if (origin == null) {   // ----- 失敗した場合，
                // ※ダミーを入れておく
                origin = new GameObject($"Defalut Particle");
                Debug.Log($"[{particleName}]というパーティクルの読み込みに失敗しました");

            } else {                // ----- 成功した場合，
                origin.name = particleName;
            }
            return origin;
        }

    }
}