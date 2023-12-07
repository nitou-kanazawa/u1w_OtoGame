using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using nitou.DesignPattern;

namespace OtoGame.Composition {
    using OtoGame.LevelObjects;

    public partial class AplicationMain {

        /// <summary>
        /// ゲームステート基底クラス
        /// </summary>
        private abstract class GameState : AwaitableStateMachine<AplicationMain, GameStateEvent>.State {

            /// <summary>
            /// ステートタイプ
            /// </summary>
            public abstract GameStateType Type { get; }


            /// ----------------------------------------------------------------------------
            // Trigger Method

            /// <summary>
            /// UIステートを終了するトリガー
            /// </summary>
            protected void TriggerCloseUI() {
                //if (Type != GameStateType.UI) return;

                //// UI管理側でロックされて入れば，何もしない
                //if (!Context._uiTransitionService.CanCloseUI) return;

                //// 遷移イベントの発火
                //StateMachine.EnqueueTransitionRequest<GamePlayState>();
            }


            /// ----------------------------------------------------------------------------
            // Common Process

            /// <summary>
            /// 入力の有効化
            /// </summary>
            protected T GetLevelReference<T>() where T : LevelReference {
                return GameObject.FindObjectOfType<T>();
            }

        }


    }
}