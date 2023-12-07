using System;

namespace nitou.UI.PresentationFramework {

    /// <summary>
    /// プレゼンター基底クラス
    /// </summary>
    public abstract class Presenter<TView> : IDisposable {

        public bool IsDisposed { get; private set; }

        public bool IsInitialized { get; private set; }

        private TView View { get; }


        /// --------------------------------------------------------------------
        // Public Methord

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize() {
            if (IsInitialized)
                throw new InvalidOperationException($"{GetType().Name} is already initialized.");

            if (IsDisposed)
                throw new ObjectDisposedException(nameof(Presenter<TView>));

            Initialize(View);
            IsInitialized = true;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public virtual void Dispose() {
            if (!IsInitialized)
                return;

            if (IsDisposed)
                return;

            Dispose(View);
            IsDisposed = true;
        }


        /// --------------------------------------------------------------------
        // Protected Methord

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Presenter(TView view) {
            View = view;
        }

        /// <summary>
        /// Initializes the presenter.
        /// </summary>
        protected abstract void Initialize(TView view);

        /// <summary>
        ///     Disposes the presenter.
        /// </summary>
        protected abstract void Dispose(TView view);
    }


    /// <summary>
    /// プレゼンター基底クラス
    /// </summary>
    public abstract class Presenter<TView, TDataSource> : IDisposable {

        public bool IsDisposed { get; private set; }

        public bool IsInitialized { get; private set; }

        private TView View { get; }

        private TDataSource DataSource { get; }


        /// --------------------------------------------------------------------
        // Public Methord

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize() {
            if (IsInitialized)
                throw new InvalidOperationException($"{GetType().Name} is already initialized.");

            if (IsDisposed)
                throw new ObjectDisposedException(nameof(Presenter<TView, TDataSource>));

            Initialize(View, DataSource);
            IsInitialized = true;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public virtual void Dispose() {
            if (!IsInitialized) 
                return;

            if (IsDisposed) 
                return;

            Dispose(View, DataSource);
            IsDisposed = true;
        }


        /// --------------------------------------------------------------------
        // Protected Methord

        protected Presenter(TView view, TDataSource dataSource) {
            View = view;
            DataSource = dataSource;
        }

        /// <summary>
        ///     Initializes the presenter.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="dataStore"></param>
        protected abstract void Initialize(TView view, TDataSource dataStore);

        /// <summary>
        ///     Disposes the presenter.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="dataSource"></param>
        protected abstract void Dispose(TView view, TDataSource dataSource);
    }
}
