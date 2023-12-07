using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {


    public sealed class LoadingView : AppView<LoadingViewState> {

        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// ‰Šú‰»ˆ—
        /// </summary>
        protected override UniTask Initialize(LoadingViewState viewState) {
            return UniTask.CompletedTask;
        }
    }
}
