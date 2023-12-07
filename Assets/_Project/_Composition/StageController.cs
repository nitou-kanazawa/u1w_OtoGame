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
    /// �X�e�[�W�i�s��S���N���X
    /// </summary>
    public class StageController : System.IDisposable {

        /// <summary>
        /// �X�e�[�W�̏��
        /// </summary>
        public enum State {
            Waiting,
            Playing,
            Paused,
            Finished,
        }

        // Model�f�[�^
        public MusicData MusicData { get; }
        public ResultData ResultData { get; }

        // �V�[���I�u�W�F�N�g
        public AudioManager AudioManager { get; }
        public NotesManager NotesManager { get; }
        public Player Player { get; }

        /// <summary>
        /// ���݂̃X�e�[�g
        /// </summary>
        public State CurrentState { get; private set; } = State.Waiting;

        public bool IsFinalizing { get; private set; } = false;

        public IObservable<Unit> OnFinalizedAsync => _finalizedAsyncSubject;
        private AsyncSubject<Unit> _finalizedAsyncSubject = new ();


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StageController(MusicData musicData, ResultData resultData
            , AudioManager audioManager, NotesManager notesManager, Player player) {

            MusicData = musicData;
            ResultData = resultData;
            AudioManager = audioManager;
            NotesManager = notesManager;
            Player = player;

            // ����������
            NotesManager.Initialize();
            Player.Initialize();
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {

            // AudioManager�̃��Z�b�g�i���j���͂��Ȃ��j
            AudioManager.Stop();
            AudioManager.UnregisterChildren();


        }



        /// ----------------------------------------------------------------------------
        // Private Method ()

        public void Setup() {
            // �Z�b�g�A�b�v
            ResultData.Init();
            AudioManager.Clip = MusicData.Clip;
            NotesManager.Setup(MusicData);
            Player.Setup(NotesManager, ResultData);

            // �o�^
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

            // �t���O�X�V
            CurrentState = State.Paused;
        }

        public void UnPauseGame() {
            if (CurrentState != State.Paused) return;

            AudioManager.UnPause();

            // �t���O�X�V
            CurrentState = State.Playing;
        }

        public void StopGame() {

            AudioManager.Stop();
            AudioManager.UnregisterChildren();

            // �t���O�X�V
            CurrentState = State.Waiting;
        }


        public void CheckFinishMusic() {
            if(AudioManager.Progress >= 0.9) {
                IsFinalizing = true;

                if (AudioManager.Progress >= 0.98) {
                    CurrentState = State.Finished;

                    //�����������ʒm
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
///// ���݂̃X�e�[�g
///// </summary>
//public IReadOnlyReactiveProperty<State> CurrentState => _currentState;
//private ReactiveProperty<State> _currentState = new(State.Waiting);