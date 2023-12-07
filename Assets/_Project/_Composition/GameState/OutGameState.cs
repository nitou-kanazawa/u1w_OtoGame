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
        /// タイトル画面のゲームステート
        /// </summary>
        private class OutGameState : GameState {

            /// ----------------------------------------------------------------------------
            // Properity

            /// <summary>
            /// ステートタイプ
            /// </summary>
            public override GameStateType Type => GameStateType.UI;


            /// ----------------------------------------------------------------------------
            // Private Method (ステート処理)

            /// <summary>
            /// ステートの開始処理
            /// </summary>
            public async override UniTask OnEnter(State fromState) {

                // -------------------
                // シーン参照の取得
                Context._titleLevelReference = GetLevelReference<TitleLevelReference>();


                // -------------------
                // タイトルUIを表示
                Context._transitionService.ApplivationStarted();

                Context.AudioManager.Clip = Context._bgm;
                Context.AudioManager.Play();

                await UniTask.CompletedTask;
            }

            /// <summary>
            /// ステートの終了処理
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