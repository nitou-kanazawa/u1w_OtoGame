using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace nitou.Launcher {


    public interface ILauncherComponent {

        /// <summary>
        /// ゲーム起動時の初期化処理
        /// </summary>
        public UniTask RuntimeInitialize(CancellationToken token);

    }

}