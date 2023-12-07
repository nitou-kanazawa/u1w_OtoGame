using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace nitou.UI {

    public class OverlayScreen : MonoBehaviour {

        [SerializeField] CanvasGroup _canvasGroup;


        /// ----------------------------------------------------------------------------
        // Public Methord (ÉÅÉjÉÖÅ[âÊñ )

        public UniTask Open(float duration = 1f) {
            return _canvasGroup.DOFade(0, duration).ToUniTask();
        }

        public UniTask Close(float duration = 1f) {
            return _canvasGroup.DOFade(1, duration).ToUniTask();
        }
    }

}