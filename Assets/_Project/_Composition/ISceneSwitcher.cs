using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OtoGame.Composition {

    /// <summary>
    /// シーン切り替え処理を提供するインターフェース
    /// </summary>
    public interface ISceneSwitcher {

        public UniTask SwitchToInGame();

        public UniTask SwitchToOutGame();

    }

}