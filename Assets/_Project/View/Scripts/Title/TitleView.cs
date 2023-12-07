using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class TitleView : AppView<TitleViewState> {


        public Button _button;

        public IObservable<Unit> OnClicked => _button.OnClickAsObservable();


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// èâä˙âªèàóù
        /// </summary>
        protected override UniTask Initialize(TitleViewState viewState) {
            // 
            _button.SetOnClickDestination(viewState.InvokeBackButtonClicked).AddTo(this);
            
            return UniTask.CompletedTask;
        }
    }
}
