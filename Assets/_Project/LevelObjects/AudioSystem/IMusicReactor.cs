using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.LevelObjects {

    /// <summary>
    /// オーディオ再生に伴うイベント実行を行うインターフェース
    /// </summary>
    public interface IMusicReactor {


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// 対象の音源
        /// </summary>
        public AudioClip TargetClip { get; }

        /// <summary>
        /// セットアップが完了しているかどうか
        /// </summary>
        public bool IsSetuped { get; }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// オーディオ再生に伴う更新イベント
        /// </summary>
        public void UpdateWithAudio(float audioTime);

        /// <summary>
        /// ポーズ時の処理
        /// </summary>
        public void OnPause();

        /// <summary>
        /// リスタート時の処理
        /// </summary>
        public void OnUnPause();

        /// <summary>
        /// 終了時の処理
        /// </summary>
        public void OnStop();
    }

}