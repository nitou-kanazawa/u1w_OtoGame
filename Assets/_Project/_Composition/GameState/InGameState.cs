using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using unityroom.Api;
using UniRx;
using nitou;
using nitou.DesignPattern;

namespace OtoGame.Composition {
    using OtoGame.Model;
    using OtoGame.LevelObjects;
    using State = AwaitableStateMachine<AplicationMain, GameStateEvent>.State;

    public partial class AplicationMain {

        /// <summary>
        /// インゲーム
        /// </summary>
        private class InGameState : GameState {


            private StageController Controller {
                get => Context._stageController;
                set => Context._stageController = value;
            }

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
                var startTime = Time.time;

                // -------------------
                // シーン参照
                Context._stageLevelReference = GetLevelReference<StageLevelReference>();
                if (Context._stageLevelReference == null) Debug_.LogError($"シーン参照の取得に失敗しました．");

                // セットアップ
                var musicData = Context.LoadScoreData(Context._clip, Context._scoreJson);
                Context.ResultData = new ResultData(musicData.NoteCount);


                // -------------------
                // 
                Controller = new StageController(
                    musicData,
                    Context.ResultData,
                    Context._audioManager,
                    Context._stageLevelReference.NotesManager,
                    Context._stageLevelReference.Player
                );
                Controller.Setup();
                
                // 終了時の処理
                Controller.OnFinalizedAsync.Subscribe(_ => {

                    Debug_.Log("--------------------", Colors.Aqua);
                    Debug_.Log("Finish", Colors.Aqua);

                    // ランキング登録
                    UnityroomApiClient.Instance.SendScore(1, Context.ResultData.Score.Value, ScoreboardWriteMode.HighScoreDesc);

                    // UI表示
                    Context._transitionService.Push_ResultPage();
                });


                // 最端遷移時間
                await UniTask.WaitUntil(() => Time.time - startTime > 1.5f);
            }

            /// <summary>
            /// ステートの終了処理
            /// </summary>
            public async override UniTask OnExit(State toState) {

                // 終了処理
                Controller.Dispose();
                Controller = null;

                await SceneManager.UnloadSceneAsync(GameScene.Stage.ToString());
            }

            /// <summary>
            /// ステート更新処理
            /// </summary>
            public override void OnUpdate() {
                switch (Controller.CurrentState) {
                    case StageController.State.Waiting:

                        break;

                    case StageController.State.Playing: // ----- 再生中の場合，

                        // 終了判定
                        Controller.CheckFinishMusic();
                        if (Controller.IsFinalizing) return;
                        
                        // ポーズ
                        if (Input.GetKeyDown(KeyCode.Tab)) {
                            Debug_.Log("Pause", Colors.Cornsilk);
                            ((IGameStageManager)Context).PauseGame();
                        }

                        break;

                    case StageController.State.Paused:  // ----- ポーズ中の場合，

                        //// ポーズ解除
                        //if (Input.GetKeyDown(KeyCode.Space)) {
                        //    Debug_.Log("UnPause", Colors.Cornsilk);
                        //    ((IGameStageManager)Context).UnPauseGame();
                        //}
                        break;

                    case StageController.State.Finished:    // ----- 再生終了の場合，
                        break;

                    default:
                        break;
                }
            }

            public void OnApplicationFocus() {

                
            }


            /// ----------------------------------------------------------------------------
            // Private Method ()


        }
    }

}