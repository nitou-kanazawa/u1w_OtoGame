using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace nitou.DesignPattern {
    public partial class AwaitableStateMachine<TContext, TEvent> {

        /// <summary>
        /// �X�e�[�g���N���X
        /// </summary>
        public abstract class State {

            /// <summary>
            /// ���̃X�e�[�g����������X�e�[�g�}�V��
            /// </summary>
            public AwaitableStateMachine<TContext,TEvent> StateMachine { get; protected set; }

            /// <summary>
            /// ���̃X�e�[�g����������X�e�[�g�}�V���������Ă���R���e�L�X�g
            /// </summary>
            public TContext Context { get; protected set; }


            /// ----------------------------------------------------------------------------

            /// <summary>
            /// �X�e�[�g����������
            /// </summary>
            internal void Init(AwaitableStateMachine<TContext, TEvent> stateMachine) {

                // �R���|�[�l���g
                StateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
                Context = stateMachine.Context;

                OnInit();
            }

            /// <summary>
            /// �h���N���X�p�̏���������
            /// </summary>
            protected virtual void OnInit() { }


            /// ----------------------------------------------------------------------------
            // Public Method (�X�e�[�g�J�ڏ���)

            /// <summary>
            /// Checks if the required conditions to exit this state are true. If so it returns the desired state (null otherwise). After this the state machine will
            /// proceed to evaluate the "enter transition" condition on the target state.
            /// </summary>
            public virtual void CheckExitTransition() {

                // [�����̈��]
                //if (conditionA) {
                //    EnqueueTransition<TargetStateA>();
                //} else if (conditionB) {
                //    EnqueueTransition<TargetStateB>();
                //    EnqueueTransition<TargetStateC>();
                //}
            }

            /// <summary>
            /// Checks if the required conditions to enter this state are true. If so the state machine will automatically change the current state to the desired one.
            /// </summary>  
            public virtual bool CheckEnterTransition(State fromState) => true;


            /// ----------------------------------------------------------------------------
            // Public Method (�C�x���g����)

            /// <summary>
            /// �C�x���g���󂯎�����Ƃ��̑Ή�����
            /// </summary>
            public virtual void HandleEvent(TEvent eventId) {}


            /// ----------------------------------------------------------------------------
            // Public Method (�X�e�[�g�J�ڏ���)

            /// <summary>
            /// �X�e�[�g�J�n����
            /// </summary>
            public virtual async UniTask OnEnter(State fromState) {
                await UniTask.Yield();
            }

            /// <summary>
            /// �X�e�[�g�I������
            /// </summary>
            public virtual async UniTask OnExit(State toState) {
                await UniTask.Yield();
            }


            /// ----------------------------------------------------------------------------
            // Public Method (�X�e�[�g�X�V����)

            /// <summary>
            /// �X�e�[�g�X�V����
            /// </summary>
            public virtual void OnUpdate() {
            }


        }
    }
}

