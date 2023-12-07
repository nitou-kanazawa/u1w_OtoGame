using System;
using UniRx;
using UnityEngine.UI;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class MenuTopViewState : AppViewState  {

        public IObservable<Unit> OnBackButtonClicked => _onBackButtonClickedSubject;
        private readonly Subject<Unit> _onBackButtonClickedSubject = new Subject<Unit>();
       
        
        /// ----------------------------------------------------------------------------
        // Public Method

        public void InvokeBackButtonClicked(){
            _onBackButtonClickedSubject.OnNext(Unit.Default);
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        protected override void DisposeInternal(){
            _onBackButtonClickedSubject.Dispose();
        }
    }

}
