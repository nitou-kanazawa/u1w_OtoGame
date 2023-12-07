using System.Threading.Tasks;
using UnityEngine.Assertions;
using UnityScreenNavigator.Runtime.Core.Page;

namespace nitou.UI.PresentationFramework {

    public abstract class Page<TRootView, TViewState> : Page
        where TRootView : AppView<TViewState>
        where TViewState : AppViewState {

        public TRootView root;
        private TViewState _state;

        /// <summary>
        /// 初期化済みかどうか
        /// </summary>
        public bool IsInitialized => _isInitialized;
        private bool _isInitialized;

        /// <summary>
        /// 初期化タイミング
        /// （※InitializeだとViewStateが生成されていないので注意）
        /// </summary>
        protected virtual ViewInitializationTiming RootInitializationTiming =>
            ViewInitializationTiming.BeforeFirstEnter;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// View Stateの設定
        /// </summary>
        public void Setup(TViewState state) {
            _state = state;
        }

#if USN_USE_ASYNC_METHODS
        public override async Task Initialize() {
            Assert.IsNotNull(root);

            await base.Initialize();

            if (RootInitializationTiming == ViewInitializationTiming.Initialize && !_isInitialized) {
                await root.InitializeAsync(_state);
                _isInitialized = true;
            }
        }
#else
        public override IEnumerator Initialize() {
            Assert.IsNotNull(root);

            yield return base.Initialize();

            if (RootInitializationTiming == ViewInitializationTiming.Initialize && !_isInitialized){
                yield return root.InitializeAsync(_state).ToCoroutine();
                _isInitialized = true;
            }
        }
#endif

#if USN_USE_ASYNC_METHODS
        public override async Task WillPushEnter() {
            Assert.IsNotNull(root);

            await base.WillPushEnter();

            if (RootInitializationTiming == ViewInitializationTiming.BeforeFirstEnter && !_isInitialized) {
                await root.InitializeAsync(_state);
                _isInitialized = true;
            }
        }
#else
        public override IEnumerator WillPushEnter() {
            Assert.IsNotNull(root);

            yield return base.WillPushEnter();

            if (RootInitializationTiming == ViewInitializationTiming.BeforeFirstEnter && !_isInitialized){
                yield return root.InitializeAsync(_state).ToCoroutine();
                _isInitialized = true;
            }
        }
#endif
    }
}
