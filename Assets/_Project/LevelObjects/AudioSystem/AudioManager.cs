using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

namespace OtoGame.LevelObjects {

    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour {

        /// <summary>
        /// ステート
        /// </summary>
        public enum State {
            Stop,
            Playing,
            Paused,
        }

        /// <summary>
        /// 
        /// </summary>
        public AudioSource Audio => _audio != null ? _audio : _audio = GetComponent<AudioSource>();
        private AudioSource _audio;

        /// <summary>
        /// 対象のクリップ
        /// </summary>
        public AudioClip Clip {
            get => Audio.clip;
            set => Audio.clip = value;
        }

        /// <summary>
        /// 現在のステート
        /// </summary>
        [ShowInInspector, ReadOnly]
        public State CurrentState { get; private set; }

        /// <summary>
        /// 再生中かどうか
        /// </summary>
        public bool IsPlaying => CurrentState == State.Playing;

        /// <summary>
        /// 現在の再生時間
        /// </summary>
        public IReadOnlyReactiveProperty<float> Time => _time;
        public FloatReactiveProperty _time = new(0);

        /// <summary>
        /// 
        /// </summary>
        public float MaxTime => (Clip != null) ? Clip.length : 0;

        /// <summary>
        /// 進捗度
        /// </summary>
        public float Progress => (MaxTime != 0) ? Time.Value / MaxTime : 0;

        public float Volume {
            get => Audio.volume;
            set => Audio.volume = value;
        }


        private IDisposable _disposable;
        private List<IMusicReactor> _childList = new();


        /// ----------------------------------------------------------------------------
        // Public Method       

        /// <summary>
        /// 再生
        /// </summary>
        public void Play() {
            if (IsPlaying) {
                Debug.LogWarning("既に再生中です．");
                return;
            }

            if (Audio.clip == null) {
                Debug.LogWarning("クリップが設定されていません．");
                return;
            }

            // Audio再生
            Audio.Play();

            // イベントループの開始
            _disposable = Observable.EveryUpdate()
                .Where(_ => IsPlaying)
                .Subscribe(_ => UpdateProcess())
                .AddTo(this);

            // フラグ更新
            CurrentState = State.Playing;
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause() {
            if (!IsPlaying) return;

            Audio.Pause();

            // 停止処理
            foreach (var child in _childList) {
                child.OnPause();
            }

            // フラグ更新
            CurrentState = State.Paused;
        }

        /// <summary>
        /// リスタート
        /// </summary>
        public void UnPause() {
            if (CurrentState != State.Paused) return;

            Audio.UnPause();

            // 再開処理
            foreach (var child in _childList) {
                child.OnUnPause();
            }

            // フラグ更新
            CurrentState = State.Playing;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop() {
            // Audio停止
            Audio.Stop();
            _disposable?.Dispose();

            // 終了
            foreach (var child in _childList) {
                child.OnStop();
            }

            CurrentState = State.Stop;
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// 
        /// </summary>
        public bool RegisterChild(IMusicReactor manager) {
            if (IsPlaying || manager == null) return false;

            _childList.Add(manager);

            return true;
        }

        public void UnregisterChildren() {
            _childList.Clear();
        }

        /// ----------------------------------------------------------------------------
        // Private Method 

        private void UpdateProcess() {
            // 時間の更新
            _time.Value = Audio.time;

            // 
            foreach (var child in _childList) {
                child.UpdateWithAudio(_time.Value);
            }
        }
    }
}