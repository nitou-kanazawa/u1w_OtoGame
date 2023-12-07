using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using UniRx;
using Sirenix.OdinInspector;
using nitou.UI.PresentationFramework;


namespace OtoGame.View {

    public sealed class CreditView : AppView<CreditViewState> {

        private const string BUTTON_GROUP = "Control Buttons";


        /// ----------------------------------------------------------------------------
        // Field & Properity

        /// <summary>
        /// バックボタン
        /// </summary>
        [TitleGroup(BUTTON_GROUP), Indent]
        public Button backButton;


        /// ----------------------------------------------------------------------------
        // Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override UniTask Initialize(CreditViewState viewState) {

            return UniTask.CompletedTask;
        }

    }
}
