using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using nitou;
using nitou.DesignPattern;

namespace OtoGame.Composition {
    using OtoGame.LevelObjects;
    using OtoGame.Presentation;
    using State = AwaitableStateMachine<AplicationMain, GameStateEvent>.State;

    public partial class AplicationMain {

        /// <summary>
        /// �^�C�g����ʂ̃Q�[���X�e�[�g
        /// </summary>
        private class OutGameState : GameState {

            /// ----------------------------------------------------------------------------
            // Properity

            /// <summary>
            /// �X�e�[�g�^�C�v
            /// </summary>
            public override GameStateType Type => GameStateType.UI;


            /// ----------------------------------------------------------------------------
            // Private Method (�X�e�[�g����)

            /// <summary>
            /// �X�e�[�g�̊J�n����
            /// </summary>
            public async override UniTask OnEnter(State fromState) {

                // -------------------
                // �V�[���Q�Ƃ̎擾
                Context._titleLevelReference = GetLevelReference<TitleLevelReference>();


                // -------------------
                // �^�C�g��UI��\��
                Context._transitionService.ApplivationStarted();

                Context.AudioManager.Clip = Context._bgm;
                Context.AudioManager.Play();

                await UniTask.CompletedTask;
            }

            /// <summary>
            /// �X�e�[�g�̏I������
            /// </summary>
            public async override UniTask OnExit(State toState) {

                var volume = Context.AudioManager.Volume;
                await DOTween.To(
                    () => Context.AudioManager.Volume,
                    x => Context.AudioManager.Volume = x,
                    0,
                    1f
                    ).ToUniTask();
                Context.AudioManager.Stop();

                Context.AudioManager.Volume = volume;
                await SceneManager.UnloadSceneAsync(GameScene.Title.ToString());
            }


            /// ----------------------------------------------------------------------------
            // Private Method 
        }
    }

}