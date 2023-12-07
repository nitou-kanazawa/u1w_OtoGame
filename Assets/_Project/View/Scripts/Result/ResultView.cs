using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;
using UniRx;
using DG.Tweening;
using Sirenix.OdinInspector;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class ResultView : AppView<ResultViewState> {


        public TextMeshProUGUI ScreText;

        [BoxGroup("Basic"), Indent] public Button QuitButton;
        //[BoxGroup("Basic"), Indent] public Button TweetButton;


        /// ----------------------------------------------------------------------------
        // Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override UniTask Initialize(ResultViewState viewState) {


            return UniTask.CompletedTask;
        }

    }
}
