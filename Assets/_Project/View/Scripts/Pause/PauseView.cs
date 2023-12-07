using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using UniRx;
using DG.Tweening;
using Sirenix.OdinInspector;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class PauseView : AppView<PauseViewState> {

        [BoxGroup("Title"), Indent] public TextMeshProUGUI TitleText;

        [BoxGroup("Basic"), Indent] public Button ContinueButton;
        [BoxGroup("Basic"), Indent] public Button RestartButton;
        [BoxGroup("Basic"), Indent] public Button QuitButton;


        /// ----------------------------------------------------------------------------
        // Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override UniTask Initialize(PauseViewState viewState) {
            //TitleText.DOFade(0.3f, 0.5f).From(1)
            //    .SetLoops(-1, LoopType.Yoyo)
            //    .SetLink(gameObject);

            return UniTask.CompletedTask;
        }

    }
}
