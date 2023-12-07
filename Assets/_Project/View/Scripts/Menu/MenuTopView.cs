using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class MenuTopView : AppView<MenuTopViewState>{


        private const string BUTTON_GROUP = "Control Buttons";


        /// ----------------------------------------------------------------------------
        // Field & Properity

        /// <summary>
        /// ゲーム開始ボタン
        /// </summary>
        [TitleGroup(BUTTON_GROUP), Indent]
        public Button playButton;

        /// <summary>
        /// オプション設定ボタン
        /// </summary>
        [TitleGroup(BUTTON_GROUP), Indent]
        public Button optionButton;
        
        /// <summary>
        /// インフォメーション表示ボタン
        /// </summary>
        [TitleGroup(BUTTON_GROUP), Indent]
        public Button infomationButton;      
        
        /// <summary>
        /// バックボタン
        /// </summary>
        [TitleGroup(BUTTON_GROUP), Indent]
        public Button backButton;



        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override UniTask Initialize(MenuTopViewState viewState){

            // 
            backButton.SetOnClickDestination(viewState.InvokeBackButtonClicked).AddTo(this);

            return UniTask.CompletedTask;
        }
    }
}
