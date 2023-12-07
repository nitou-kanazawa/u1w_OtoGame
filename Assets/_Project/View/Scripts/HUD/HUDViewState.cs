using System;
using UniRx;
using UnityEngine.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class HUDViewState : AppViewState {
                
        /// <summary>
        /// 
        /// </summary>
        public IObservable<Unit> OnClicked => _onClickedSubject;
        private readonly Subject<Unit> _onClickedSubject = new ();


        /// ----------------------------------------------------------------------------
        // Public Method

        public void InvokeBackButtonClicked() {
            _onClickedSubject.OnNext(Unit.Default);
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// èIóπèàóù
        /// </summary>
        protected override void DisposeInternal() {
            _onClickedSubject.Dispose();
        }
    }
}
