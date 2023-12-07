using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {

    public class ChildPresenter<TPage, TParent> where TParent : Presenter<TPage> {

        // 親プレゼンター
        protected TParent Parent { get; }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected ChildPresenter(TParent parent) {
            Parent = parent;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// Screenクラスがロードされた直後の処理
        /// </summary>
        public virtual Task DidLoad() {

            return Task.CompletedTask;
        }

        /// <summary>
        /// Screenクラスが表示される直前の処理
        /// </summary>
        public virtual Task WillEnter() {

            return Task.CompletedTask;
        }

        /// <summary>
        /// Screenクラスが非表示される直前の処理
        /// </summary>
        /// <returns></returns>
        public virtual Task WillExit() {

            return Task.CompletedTask;
        }

        /// <summary>
        /// Screenクラスが破棄される直前の処理
        /// </summary>
        /// <returns></returns>
        public virtual Task WillDestroy() {

            return Task.CompletedTask;
        }

    }
}