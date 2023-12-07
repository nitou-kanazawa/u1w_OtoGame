using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using nitou;

namespace OtoGame.Composition {
    using OtoGame.Model;
    using OtoGame.LevelObjects;

    /// <summary>
    /// ステージ進行を担うクラス
    /// </summary>
    public class StageController : System.IDisposable {

        /// <summary>
        /// ステージの状態
        /// </summary>
        public enum State {
            Waiting,
            Playing,
            Paused,
            Finished,
        }

        // Modelデータ
        public MusicData MusicData { get; }
        public ResultData ResultData { get; }

        // シーンオブジェクト
        public AudioManager AudioManager { get; }
        public NotesManager NotesManager { get; }
        public Player Player { get; }

        /// <summary>
        /// 現在のステート
        /// </summary>
        public State CurrentState { get; private set; } = State.Waiting;

        public bool IsFinalizing { get; private set; } = false;

        public IObservable<Unit> OnFinalizedAsync => _finalizedAsyncSubject;
        private AsyncSubject<Unit> _finalizedAsyncSubject = new ();


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StageController(MusicData musicData, ResultData resultData
            , AudioManager audioManager, NotesManager notesManager, Player player) {

            MusicData = musicData;
            ResultData = resultData;
            AudioManager = audioManager;
            NotesManager = notesManager;
            Player = player;

            // 初期化処理
            NotesManager.Initialize();
            Player.Initialize();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {

            // AudioManagerのリセット（※破棄はしない）
            AudioManager.Stop();
            AudioManager.UnregisterChildren();


        }



        /// ----------------------------------------------------------------------------
        // Private Method ()

        public void Setup() {
            // セットアップ
            ResultData.Init();
            AudioManager.Clip = MusicData.Clip;
            NotesManager.Setup(MusicData);
            Player.Setup(NotesManager, ResultData);

            // 登録
            AudioManager.RegisterChild(NotesManager);
            AudioManager.RegisterChild(Player);
        }

        /// ----------------------------------------------------------------------------
        // Private Method ()

        public void StartGame() {
            if (CurrentState != State.Waiting) return;
            AudioManager.Play();

            CurrentState = State.Playing;
        }

        public void PauseGame() {
            if (CurrentState != State.Playing) return;

            AudioManager.Pause();

            // フラグ更新
            CurrentState = State.Paused;
        }

        public void UnPauseGame() {
            if (CurrentState != State.Paused) return;

            AudioManager.UnPause();

            // フラグ更新
            CurrentState = State.Playing;
        }

        public void StopGame() {

            AudioManager.Stop();
            AudioManager.UnregisterChildren();

            // フラグ更新
            CurrentState = State.Waiting;
        }


        public void CheckFinishMusic() {
            if(AudioManager.Progress >= 0.9) {
                IsFinalizing = true;

                if (AudioManager.Progress >= 0.98) {
                    CurrentState = State.Finished;

                    //初期化完了通知
                    Debug_.Log("Finalizing");
                    _finalizedAsyncSubject.OnNext(Unit.Default);
                    _finalizedAsyncSubject.OnCompleted();
                }

            } else {
                IsFinalizing = false;
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method ()

        public void UpdateProcess() {
            
        }
    }

}





///// <summary>
///// 現在のステート
///// </summary>
//public IReadOnlyReactiveProperty<State> CurrentState => _currentState;
//private ReactiveProperty<State> _currentState = new(State.Waiting);