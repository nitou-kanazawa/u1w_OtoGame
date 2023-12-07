using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace nitou.Launcher {


    public interface ILauncherComponent {

        /// <summary>
        /// ƒQ[ƒ€‹N“®‚Ì‰Šú‰»ˆ—
        /// </summary>
        public UniTask RuntimeInitialize(CancellationToken token);

    }

}