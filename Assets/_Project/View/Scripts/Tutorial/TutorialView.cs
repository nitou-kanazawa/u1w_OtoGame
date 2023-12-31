using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {
    using OtoGame.View.Components;

    public sealed class TutorialView : AppView<TutorialViewState> {

        [Title("Score")]
        public ScoreTextView ScoreText;
        
        [Title("Counter")]
        public CounterView GreateCounter;
        public CounterView GoodCounter;
        public CounterView BadCounter;

        [Title("Combo")]
        public ComboTextView ComboText;

        [Title("Time Slider")]
        public TimeSliderView TimeSlider;



        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override UniTask Initialize(TutorialViewState viewState) {
            // 
            
            return UniTask.CompletedTask;
        }
    }
}
