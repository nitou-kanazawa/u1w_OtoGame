using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.Composition {

    /// <summary>
    /// ステートの分類
    /// </summary>
    public enum GameStateType {
        GameSetup,      // 初期化やデータ読み込み
        GameTutorial,   // チュートリアル
        GamePlay,       // プレイヤー操作
        UI,       // UI操作
        None,           // 
    }

}