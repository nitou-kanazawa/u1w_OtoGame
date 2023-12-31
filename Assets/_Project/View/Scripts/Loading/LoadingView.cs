using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {


    public sealed class LoadingView : AppView<LoadingViewState> {

        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override UniTask Initialize(LoadingViewState viewState) {
            return UniTask.CompletedTask;
        }
    }
}
