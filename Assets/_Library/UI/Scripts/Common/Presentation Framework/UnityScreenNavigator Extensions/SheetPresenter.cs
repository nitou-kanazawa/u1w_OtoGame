using System.Collections;
using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace nitou.UI.PresentationFramework {

    /// <summary>
    /// Page�v���[���^�[�̊��N���X
    /// </summary>
    public abstract class SheetPresenter<TSheet> : Presenter<TSheet>, ISheetPresenter where TSheet : Sheet {

        private TSheet View { get; }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        protected SheetPresenter(TSheet view) : base(view) {
            View = view;
        }


        /// ----------------------------------------------------------------------------
        // ISheetLifecycleEvent

#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.Initialize() {
            return ViewDidLoad(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.Initialize() {
            return ViewDidLoad(View);
        }
#endif

#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.WillEnter() {
            return ViewWillEnter(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.WillEnter() {
            return ViewWillEnter(View);
        }
#endif

        void ISheetLifecycleEvent.DidEnter() {
            ViewDidEnter(View);
        }

#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.WillExit() {
            return ViewWillExit(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.WillExit() {
            return ViewWillExit(View);
        }
#endif

        void ISheetLifecycleEvent.DidExit() {
            ViewDidExit(View);
        }

#if USN_USE_ASYNC_METHODS
        Task ISheetLifecycleEvent.Cleanup() {
            return ViewWillDestroy(View);
        }
#else
        IEnumerator ISheetLifecycleEvent.Cleanup() {
            return ViewWillDestroy(View);
        }
#endif


        /// ----------------------------------------------------------------------------
        // Inner LifecycleEvent

#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewDidLoad(TSheet view) {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewDidLoad(TSheet view) {
            yield break;
        }
#endif

#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillEnter(TSheet view) {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillEnter(TSheet view) {
            yield break;
        }
#endif

        protected virtual void ViewDidEnter(TSheet view) {
        }

#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillExit(TSheet view) {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillExit(TSheet view) {
            yield break;
        }
#endif

        protected virtual void ViewDidExit(TSheet view) {
        }

#if USN_USE_ASYNC_METHODS
        protected virtual Task ViewWillDestroy(TSheet view) {
            return Task.CompletedTask;
        }
#else
        protected virtual IEnumerator ViewWillDestroy(TSheet view) {
            yield break;
        }
#endif

        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// ����������
        /// </summary>
        protected override void Initialize(TSheet view) {
            // The lifecycle event of the view will be added with priority 0.
            // Presenters should be processed after the view so set the priority to 1.
            view.AddLifecycleEvent(this, 1);
        }

        /// <summary>
        /// �I������
        /// </summary>
        protected override void Dispose(TSheet view) {
            view.RemoveLifecycleEvent(this);
        }
    }
}
