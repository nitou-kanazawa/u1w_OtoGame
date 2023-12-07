using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using nitou.DesignPattern;

namespace OtoGame.Composition {
    using OtoGame.LevelObjects;

    public partial class AplicationMain {

        /// <summary>
        /// �Q�[���X�e�[�g���N���X
        /// </summary>
        private abstract class GameState : AwaitableStateMachine<AplicationMain, GameStateEvent>.State {

            /// <summary>
            /// �X�e�[�g�^�C�v
            /// </summary>
            public abstract GameStateType Type { get; }


            /// ----------------------------------------------------------------------------
            // Trigger Method

            /// <summary>
            /// UI�X�e�[�g���I������g���K�[
            /// </summary>
            protected void TriggerCloseUI() {
                //if (Type != GameStateType.UI) return;

                //// UI�Ǘ����Ń��b�N����ē���΁C�������Ȃ�
                //if (!Context._uiTransitionService.CanCloseUI) return;

                //// �J�ڃC�x���g�̔���
                //StateMachine.EnqueueTransitionRequest<GamePlayState>();
            }


            /// ----------------------------------------------------------------------------
            // Common Process

            /// <summary>
            /// ���̗͂L����
            /// </summary>
            protected T GetLevelReference<T>() where T : LevelReference {
                return GameObject.FindObjectOfType<T>();
            }

        }


    }
}