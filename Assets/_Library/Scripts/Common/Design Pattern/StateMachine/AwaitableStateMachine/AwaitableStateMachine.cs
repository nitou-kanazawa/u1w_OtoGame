using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UniRx;

// [�Q�l]
//  qiita: UniTask Ver2, UniTaskAsyncEnumerable�܂Ƃ� https://qiita.com/toRisouP/items/8f66fd952eaffeaf3107
//  _: UniTask���p���[�A�b�v�I�wUniTask v2�x���g�����I https://hackmd.io/@-xLrSnFfROOeIeRnENCWcQ/HkVAMY5Sd


namespace nitou.DesignPattern {

    /// <summary>
    /// UniTask�ɑΉ������V���v���ȃX�e�[�g�}�V���D
    /// </summary>
    public partial class AwaitableStateMachine<TContext, TEvent> : IDisposable {

        /// <summary>
        /// �X�e�[�g�}�V���̏����t�F�[�Y
        /// </summary>
        public enum ProcessPhase {
            Idle,   // ��~��
            Enter,  // 
            Update,
            Exit,
        }

        /// <summary>
        /// �X�e�[�g�}�V�����ێ����Ă���R���e�L�X�g
        /// </summary>
        public TContext Context { get; private set; }


        /// ----------------------------------------------------------------------------
        // �X�e�[�g���

        /// <summary>
        /// ���݂̃X�e�[�g
        /// </summary>
        public State CurrentState { get; private set; }

        /// <summary>
        /// �O��̃X�e�[�g
        /// </summary>
        public State PreviousState { get; private set; }


        /// ----------------------------------------------------------------------------
        // �X�e�[�g�}�V���ғ����

        /// <summary>
        /// �X�e�[�g�}�V���̉ғ���
        /// </summary>
        public ProcessPhase CurrentPhase { get; private set; }

        /// <summary>
        /// �X�e�[�g�}�V�����ғ������ǂ���
        /// </summary>
        public bool IsRunningPhase => CurrentPhase != ProcessPhase.Idle;

        /// <summary>
        /// �X�e�[�g�}�V�����X�V���������ǂ���
        /// </summary>
        public bool IsUpdating => CurrentPhase == ProcessPhase.Update;

        /// ----------------------------------------------------------------------------

        // �o�^�X�e�[�g�̃��X�g
        private readonly Dictionary<Type, State> _states = new();

        // �J�ڃ��N�G�X�g
        private readonly Queue<State> _transitionsQueue = new();

        // �L�����Z���p
        private CancellationTokenSource _cancellationTokenSource;


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public AwaitableStateMachine(TContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            // 
            Context = context;
            CurrentPhase = ProcessPhase.Idle;
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
        }


        /// ----------------------------------------------------------------------------
        #region �X�e�[�g�}�V������n

        /// <summary>
        /// �X�e�[�g�}�V�������s����
        /// </summary>
        public void Run() {
            if (IsRunningPhase) {
                throw new InvalidOperationException("�X�e�[�g�}�V���́A�N�����ł�");
            }

            _cancellationTokenSource = new();
            UpdateLoop();
        }

        /// <summary>
        /// �X�e�[�g�}�V�����~����
        /// </summary>
        public void Stop() {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region �X�e�[�g�J�ڃe�[�u���\�z�n

        /// <summary>
        /// �X�e�[�g��o�^����
        /// </summary>
        public void RegisterState(State state) {
            if (IsRunningPhase) {
                throw new InvalidOperationException("�X�e�[�g�}�V���́A�N�����ł�");
            }

            if (state == null) {
                throw new ArgumentNullException(nameof(state));
            }

            Type type = state.GetType();
            if (_states.ContainsKey(type)) {
                // �㏑���o�^�������Ȃ��̂ŗ�O��f��
                throw new ArgumentException($"�X�e�[�g'{state.GetType().Name}'�́A���Ƀ��X�g�ɓo�^�ς݂ł�");
            }

            // ���X�g�֒ǉ�
            state.Init(this);
            _states.Add(type, state);
        }

        /// <summary>
        /// �X�e�[�g��o�^����
        /// </summary>
        public void RegisterState<TState>() where TState : State, new() {
            if (IsRunningPhase) {
                throw new InvalidOperationException("�X�e�[�g�}�V���́A�N�����ł�");
            }

            Type type = typeof(TState);
            if (_states.ContainsKey(type)) {
                // �㏑���o�^�������Ȃ��̂ŗ�O��f��
                throw new ArgumentException($"�X�e�[�g'{type}'�́A���Ƀ��X�g�ɓo�^�ς݂ł�");
            }

            // ���X�g�֒ǉ�
            var state = new TState();
            state.Init(this);
            _states.Add(type, state);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // Public Method (�X�e�[�g�J��)

        /// <summary>
        /// �J�ڃ��N�G�X�g��ς�
        /// </summary>
        public void EnqueueTransitionRequest<TState>() where TState : State {
            // �o�^���X�g�Ɋ܂܂��ꍇ�C
            if (_states.TryGetValue(typeof(TState), out State state)) {
                _transitionsQueue.Enqueue(state);
            }
        }

        /// <summary>
        /// �����I�Ɏw�肵���X�e�[�g�֑J�ڂ���
        /// </summary>
        public async UniTask ForceState(State nextState) {
            Debug_.Log($"Change State : [{CurrentState?.GetType().Name}]��[{nextState?.GetType().Name}]", Colors.SkyBlue);

            // ���݃X�e�[�g�̏I������
            CurrentPhase = ProcessPhase.Exit;
            if (CurrentState != null) {
                PreviousState = CurrentState;
                CurrentState = null;

                // �I������
                await PreviousState.OnExit(toState: nextState);
            }

            // �V�X�e�[�g�̊J�n����
            CurrentPhase = ProcessPhase.Enter;
            if (nextState != null) {
                CurrentState = nextState;

                // �J�n����
                await nextState.OnEnter(fromState: PreviousState);
            } else {
                throw new Exception($"State: {nextState.GetType().Name} is not registered to state machine.");
            }
        }

        public UniTask ForceState<TState>() {
            // �o�^���X�g�Ɋ܂܂��ꍇ�C
            if (_states.TryGetValue(typeof(TState), out State state)) {
                return ForceState(state);
            } else {
                throw new Exception($"State: {typeof(TState)} is not registered to state machine.");
            }

        }


            /// <summary>
            /// �C�x���g�����s���ꂽ�Ƃ��̑Ή�����
            /// </summary>
            public void OnReciveEvent(TEvent eventId) {
            if (!IsRunningPhase) return;

            // ���݃X�e�[�g�̑Ή����\�b�h�����s
            CurrentState?.HandleEvent(eventId);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �J�ڃt���O���m�F���đJ�ڐ�X�e�[�g��Ԃ�
        /// </summary>
        private State CheckTransitionRequests() {
            // �J�ڃ��N�G�X�g�̎擾
            CurrentState?.CheckExitTransition();     // �����݃X�e�[�g��

            // �J�ڃ��N�G�X�g�̌���
            while (_transitionsQueue.Count != 0) {
                State nextState = _transitionsQueue.Dequeue();
                if (nextState == null) continue;

                // �J�ډ\���̌���
                bool success = nextState.CheckEnterTransition(CurrentState);    // ���V�X�e�[�g��
                if (success) {
                    return nextState;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        private async void UpdateLoop() {

            // ���t���[������Update�̃^�C�~���O�܂�await����
            await foreach (var _ in UniTaskAsyncEnumerable
                               .EveryUpdate()
                               .WithCancellation(_cancellationTokenSource.Token)) {

                // �X�e�[�g�J��
                var nextState = CheckTransitionRequests();      // �J�ڐ�̎擾
                _transitionsQueue.Clear();                      // �J�ڃ��N�G�X�g�̃N���A
                if (nextState != null) {
                    await ForceState(nextState);
                }

                // �X�e�[�g�X�V����
                CurrentPhase = ProcessPhase.Update;
                CurrentState?.OnUpdate();
            }
        }
    }

}