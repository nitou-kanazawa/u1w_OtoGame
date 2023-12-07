using System;

namespace nitou.UI.PresentationFramework {


    public abstract class AppViewState : IDisposable {

        /// <summary>
        /// �I���������������Ă��邩�ǂ���
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
            if (_isDisposed) throw new ObjectDisposedException(nameof(AppViewState));

            DisposeInternal();

            _isDisposed = true;
        }

        /// <summary>
        /// �I������ (���h���N���X�ł̒�`�p)
        /// </summary>
        protected abstract void DisposeInternal();
    }
}
