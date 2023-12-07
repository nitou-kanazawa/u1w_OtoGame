using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nitou.UI.PresentationFramework {

    public abstract class PagePresenter<TPage, TRootView, TRootViewState> : PagePresenter<TPage>, IDisposableCollectionHolder
        where TPage : Page<TRootView, TRootViewState>
        where TRootView : AppView<TRootViewState>
        where TRootViewState : AppViewState, new() {

        private readonly List<IDisposable> _disposables = new();
        private TRootViewState _state;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        protected PagePresenter(TPage view) : base(view) {}

        ICollection<IDisposable> IDisposableCollectionHolder.GetDisposableCollection() {
            return _disposables;
        }


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// ����������
        /// </summary>
        protected sealed override void Initialize(TPage view) {
            base.Initialize(view);
        }

        /// <summary>
        /// �I������
        /// </summary>
        /// <param name="view"></param>
        protected sealed override void Dispose(TPage view) {
            base.Dispose(view);
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // LifecycleEvent

        protected sealed override async Task ViewDidLoad(TPage view) {
            await base.ViewDidLoad(view);

            // ViewState�̒���
            var state = new TRootViewState();
            _state = state;
            _disposables.Add(state);
            view.Setup(state);

            // �h���N���X�̏���
            await ViewDidLoad(view, _state);
        }

        protected sealed override async Task ViewWillPushEnter(TPage view) {
            await base.ViewWillPushEnter(view);
            await ViewWillPushEnter(view, _state);
        }

        protected sealed override void ViewDidPushEnter(TPage view) {
            base.ViewDidPushEnter(view);
            ViewDidPushEnter(view, _state);
        }

        protected sealed override async Task ViewWillPushExit(TPage view) {
            await base.ViewWillPushExit(view);
            await ViewWillPushExit(view, _state);
        }

        protected sealed override void ViewDidPushExit(TPage view) {
            base.ViewDidPushExit(view);
            ViewDidPushExit(view, _state);
        }

        protected sealed override async Task ViewWillPopEnter(TPage view) {
            await base.ViewWillPopEnter(view);
            await ViewWillPopEnter(view, _state);
        }

        protected sealed override void ViewDidPopEnter(TPage view) {
            base.ViewDidPopEnter(view);
            ViewDidPopEnter(view, _state);
        }

        protected sealed override async Task ViewWillPopExit(TPage view) {
            await base.ViewWillPopExit(view);
            await ViewWillPopExit(view, _state);
        }

        protected sealed override void ViewDidPopExit(TPage view) {
            base.ViewDidPopExit(view);
            ViewDidPopExit(view, _state);
        }

        protected override async Task ViewWillDestroy(TPage view) {
            await base.ViewWillDestroy(view);
            await ViewWillDestroy(view, _state);
        }



        /// ----------------------------------------------------------------------------
        // LifecycleEvent (���h���N���X�ł̍Ē�`�p)

        /// <summary>
        /// ���̃y�[�W�����[�h���ꂽ����ɌĂ΂��
        /// </summary>
        protected virtual Task ViewDidLoad(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push�J�ڂɂ�肱�̃y�[�W���\������钼�O�ɌĂ΂��
        /// </summary>
        protected virtual Task ViewWillPushEnter(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push�J�ڂɂ�肱�̃y�[�W���\�����ꂽ����ɌĂ΂��
        /// </summary>
        protected virtual void ViewDidPushEnter(TPage view, TRootViewState viewState) {
        }

        /// <summary>
        /// Push�J�ڂɂ�肱�̃y�[�W����\���ɂȂ钼�O�ɌĂ΂��
        /// </summary>
        protected virtual Task ViewWillPushExit(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Push�J�ڂɂ�肱�̃y�[�W����\���ɂȂ�������ɌĂ΂��
        /// </summary>
        protected virtual void ViewDidPushExit(TPage view, TRootViewState viewState) {
        }

        /// <summary>
        /// Pop�J�ڂɂ�肱�̃y�[�W���\������钼�O�ɌĂ΂��
        /// </summary>
        protected virtual Task ViewWillPopEnter(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Pop�J�ڂɂ�肱�̃y�[�W���\�����ꂽ����ɌĂ΂�� 
        /// </summary>
        protected virtual void ViewDidPopEnter(TPage view, TRootViewState viewState) {
        }

        /// <summary>
        /// Pop�J�ڂɂ�肱�̃y�[�W����\���ɂȂ钼�O�ɌĂ΂��
        /// </summary>
        protected virtual Task ViewWillPopExit(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Pop�J�ڂɂ�肱�̃y�[�W����\���ɂȂ�������ɌĂ΂�� 
        /// </summary>
        protected virtual void ViewDidPopExit(TPage view, TRootViewState viewState) {
        }

        /// <summary>
        /// ���̃y�[�W�������[�X����钼�O�ɌĂ΂�� 
        /// </summary>
        protected virtual Task ViewWillDestroy(TPage view, TRootViewState viewState) {
            return Task.CompletedTask;
        }


    }
}
