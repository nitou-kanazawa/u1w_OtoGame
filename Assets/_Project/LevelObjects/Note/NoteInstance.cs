using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Sirenix.OdinInspector;
using nitou;

namespace OtoGame.LevelObjects {
    using OtoGame.Model;

    public class NoteInstance : MonoBehaviour {

        /// ----------------------------------------------------------------------------
        // Field & Properiy

        [TitleGroup("Reference components"), Indent]
        [SerializeField] private NoteMeshController _meshController;

        /// <summary>
        /// ノーツデータ
        /// </summary>
        public NoteData Data { get; private set; }

        /// <summary>
        /// 打刻タイミング [sec]
        /// </summary>
        public float RaiseTime => Data.raiseTiming;

        /// <summary>
        /// 
        /// </summary>
        public int Type => Data.type;


        /// ----------------------------------------------------------------------------
        // Field & Properiy (状態)

        /// <summary>
        /// 進捗パラメータ
        /// </summary>
        public float Progress { get; set; }

        /// <summary>
        /// ノーツが既に打たれたかどうか
        /// </summary>
        public bool IsKnocked { get; private set; }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize() {
            _meshController.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Setup(NoteData data) {
            Data = data;
            
            Progress = 0f;
            IsKnocked = false;
        }

        public void Show() {
            _meshController.SetSphereColor(Colors.WhiteSmoke);
            _meshController.ShowFishModel();
        }

        public void Hide() { 
        }


        /// <summary>
        /// ヒットした時の処理
        /// </summary>
        public void OnHit(Judgement judge) {
            // 処理済みor判定結果が不正な場合，何もしない
            if (IsKnocked || !judge.IsHit()) {
                return;
            }

            // 球体のカラー設定
            switch (judge) {
                case Judgement.GREAT:
                    _meshController.SetSphereColor(Colors.Green);
                    break;

                case Judgement.GOOD:
                    _meshController.SetSphereColor(Colors.Yellow);
                    break;

                case Judgement.BAD:
                    _meshController.SetSphereColor(Colors.Red);
                    break;
            }

            // 魚モデルを非表示化
            _meshController.HideFishModel();

            IsKnocked = true;
        }

    }

}