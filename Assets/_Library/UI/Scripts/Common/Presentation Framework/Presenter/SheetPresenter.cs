using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nitou.UI.PresentationFramework {

    public abstract class SheetPresenter<TSheet, TRootView, TRootViewState> : SheetPresenter<TSheet>, IDisposableCollectionHolder
        where TSheet : Sheet<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        private TRootViewState _state;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        protected SheetPresenter(TSheet view) : base(view) { }

        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection() {
            return _disposables;
        }


        /// ----------------------------------------------------------------------------
        // LifecycleEvent

        protected sealed override void Initialize(TSheet view) {
            base.Initialize(view);
        }

        protected sealed override async Task ViewDidLoad(TSheet view) {
            await base.ViewDidLoad(view);
            var state = new TRootViewState();
            _state = state;
            _disposables.Add(state);
            view.Setup(state);
            await ViewDidLoad(view, _state);
        }

        protected sealed override async Task ViewWillEnter(TSheet view) {
            await base.ViewWillEnter(view);
            await ViewWillEnter(view, _state);
        }

        protected sealed override void ViewDidEnter(TSheet view) {
            base.ViewDidEnter(view);
            ViewDidEnter(view, _state);
        }

        protected sealed override async Task ViewWillExit(TSheet view) {
            await base.ViewWillExit(view);
            await ViewWillExit(view, _state);
        }

        protected sealed override void ViewDidExit(TSheet view) {
            base.ViewDidExit(view);
            ViewDidExit(view, _state);
        }

        protected override async Task ViewWillDestroy(TSheet view) {
            await base.ViewWillDestroy(view);
            await ViewWillDestroy(view, _state);
        }

        protected sealed override void Dispose(TSheet view) {
            base.Dispose(view);
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // LifecycleEvent (��)

        /// <summary>
        /// ���̃V�[�g�����[�h���ꂽ����ɌĂ΂��
        /// </summary>
        protected virtual Task ViewDidLoad(TSheet view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ���̃V�[�g���\������钼�O�ɌĂ΂��
        /// </summary>
        protected virtual Task ViewWillEnter(TSheet view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ���̃V�[�g���\�����ꂽ����ɌĂ΂��
        /// </summary>
        protected virtual void ViewDidEnter(TSheet view, TRootViewState viewState) {
        }

        /// <summary>
        /// ���̃V�[�g����\������钼�O�ɌĂ΂��
        /// </summary>
        protected virtual Task ViewWillExit(TSheet view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// ���̃V�[�g����\�����ꂽ����ɌĂ΂��
        /// </summary>
        protected virtual void ViewDidExit(TSheet view, TRootViewState viewState) {
        }

        /// <summary>
        /// ���̃V�[�g�������[�X����钼�O�ɌĂ΂��
        /// �i���e�y�[�W��pop�J�ڂ���ꍇ�Ȃǂɂ͌Ă΂�Ȃ��̂Œ��ӁI�j
        /// </summary>
        protected virtual Task ViewWillDestroy(TSheet view, TRootViewState viewState) {
            return Task.CompletedTask;
        }


    }
}
