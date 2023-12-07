using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace nitou.Launcher {


    public interface ILauncherComponent {

        /// <summary>
        /// �Q�[���N�����̏���������
        /// </summary>
        public UniTask RuntimeInitialize(CancellationToken token);

    }

}