using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

// [参考]
//  Hatena: パーティクル再利用クラス https://www.stmn.tech/entry/2016/03/01/004816 
//  qiita: UniRxのObjectPoolを使ってParticleSystemを管理する https://qiita.com/KeichiMizutani/items/fc22a6037447d840adc2
//  kanのメモ帳: UniRxでオブジェクトプール(ObjectPool)を簡単実装 https://kan-kikuchi.hatenablog.com/entry/UniRx_ObjectPool

namespace nitou.Particle {

    /// <summary>
    /// パーティクル本体にアタッチするクラス
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleObject : MonoBehaviour {

        private ParticleSystem particle;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 

        private void Awake() {
            particle = GetComponent<ParticleSystem>();
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// パーティクルを再生する
        /// </summary>
        public IObservable<Unit> PlayParticle(Vector3 position) {
            transform.position = position;
            particle.Play();

            // ParticleSystemのstartLifetimeに設定した秒数が経ったら終了通知
            return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
                .ForEachAsync(_ => particle.Stop());
        }

        /// <summary>
        /// パーティクルを再生する
        /// </summary>
        public IObservable<Unit> PlayParticle(Vector3 position, Quaternion rotation) {
            transform.position = position;
            transform.rotation = rotation;
            particle.Play();

            // ParticleSystemのstartLifetimeに設定した秒数が経ったら終了通知
            return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
                .ForEachAsync(_ => particle.Stop());
        }

        /// <summary>
        /// パーティクルを再生する(※イベント実行)
        /// </summary>
        public IObservable<Unit> PlayParticleWithEvent(Vector3 position) {
            transform.position = position;
            particle.Play();

            // イベント実行
            var particleEvent = GetComponent<IParticleEvent>();
            particleEvent?.OnParticlePlayed();  // ※開始イベント

            // ParticleSystemのstartLifetimeに設定した秒数が経ったら終了通知
            return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
                .ForEachAsync(_ => {
                    particle.Stop();
                    particleEvent?.OnParticlemStopped();    // ※終了イベント
                });
        }

        /// <summary>
        /// パーティクルを再生する(※イベント実行)
        /// </summary>
        public IObservable<Unit> PlayParticleWithEvent(Vector3 position, Quaternion rotation) {
            transform.position = position;
            transform.rotation = rotation;
            particle.Play();

            // イベント実行
            var particleEvent = GetComponent<IParticleEvent>();
            particleEvent?.OnParticlePlayed();  // ※開始イベント

            // ParticleSystemのstartLifetimeに設定した秒数が経ったら終了通知
            return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
                .ForEachAsync(_ => {
                    particle.Stop();
                    particleEvent?.OnParticlemStopped();    // ※終了イベント
                });
        }
    }
}