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
        /// �C���Q�[��
        /// </summary>
        private class InGameState : GameState {


            private StageController Controller {
                get => Context._stageController;
                set => Context._stageController = value;
            }

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
                var startTime = Time.time;

                // -------------------
                // �V�[���Q��
                Context._stageLevelReference = GetLevelReference<StageLevelReference>();
                if (Context._stageLevelReference == null) Debug_.LogError($"�V�[���Q�Ƃ̎擾�Ɏ��s���܂����D");

                // �Z�b�g�A�b�v
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
                
                // �I�����̏���
                Controller.OnFinalizedAsync.Subscribe(_ => {

                    Debug_.Log("--------------------", Colors.Aqua);
                    Debug_.Log("Finish", Colors.Aqua);

                    // �����L���O�o�^
                    UnityroomApiClient.Instance.SendScore(1, Context.ResultData.Score.Value, ScoreboardWriteMode.HighScoreDesc);

                    // UI�\��
                    Context._transitionService.Push_ResultPage();
                });


                // �Œ[�J�ڎ���
                await UniTask.WaitUntil(() => Time.time - startTime > 1.5f);
            }

            /// <summary>
            /// �X�e�[�g�̏I������
            /// </summary>
            public async override UniTask OnExit(State toState) {

                // �I������
                Controller.Dispose();
                Controller = null;

                await SceneManager.UnloadSceneAsync(GameScene.Stage.ToString());
            }

            /// <summary>
            /// �X�e�[�g�X�V����
            /// </summary>
            public override void OnUpdate() {
                switch (Controller.CurrentState) {
                    case StageController.State.Waiting:

                        break;

                    case StageController.State.Playing: // ----- �Đ����̏ꍇ�C

                        // �I������
                        Controller.CheckFinishMusic();
                        if (Controller.IsFinalizing) return;
                        
                        // �|�[�Y
                        if (Input.GetKeyDown(KeyCode.Tab)) {
                            Debug_.Log("Pause", Colors.Cornsilk);
                            ((IGameStageManager)Context).PauseGame();
                        }

                        break;

                    case StageController.State.Paused:  // ----- �|�[�Y���̏ꍇ�C

                        //// �|�[�Y����
                        //if (Input.GetKeyDown(KeyCode.Space)) {
                        //    Debug_.Log("UnPause", Colors.Cornsilk);
                        //    ((IGameStageManager)Context).UnPauseGame();
                        //}
                        break;

                    case StageController.State.Finished:    // ----- �Đ��I���̏ꍇ�C
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