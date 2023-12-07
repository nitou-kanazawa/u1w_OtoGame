using Cysharp.Threading.Tasks;
using UnityEngine;


namespace nitou.UI.PresentationFramework {

    public abstract class AppView<TState> : MonoBehaviour where TState : AppViewState {
        
        /// <summary>
        /// ���������������Ă��邩�ǂ���
        /// </summary>
        private bool _isInitialized;

        /// <summary>
        /// ����������
        /// </summary>
        public async UniTask InitializeAsync(TState state) {
            if (_isInitialized) return;

            _isInitialized = true;

            await Initialize(state);
        }

        /// <summary>
        /// ���������� (���h���N���X�ł̒�`�p)
        /// </summary>
        protected abstract UniTask Initialize(TState state);
    }

}
