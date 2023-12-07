using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OtoGame.Composition {

    /// <summary>
    /// �V�[���؂�ւ�������񋟂���C���^�[�t�F�[�X
    /// </summary>
    public interface ISceneSwitcher {

        public UniTask SwitchToInGame();

        public UniTask SwitchToOutGame();

    }

}