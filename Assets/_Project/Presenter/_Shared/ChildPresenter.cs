using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {

    public class ChildPresenter<TPage, TParent> where TParent : Presenter<TPage> {

        // �e�v���[���^�[
        protected TParent Parent { get; }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        protected ChildPresenter(TParent parent) {
            Parent = parent;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// Screen�N���X�����[�h���ꂽ����̏���
        /// </summary>
        public virtual Task DidLoad() {

            return Task.CompletedTask;
        }

        /// <summary>
        /// Screen�N���X���\������钼�O�̏���
        /// </summary>
        public virtual Task WillEnter() {

            return Task.CompletedTask;
        }

        /// <summary>
        /// Screen�N���X����\������钼�O�̏���
        /// </summary>
        /// <returns></returns>
        public virtual Task WillExit() {

            return Task.CompletedTask;
        }

        /// <summary>
        /// Screen�N���X���j������钼�O�̏���
        /// </summary>
        /// <returns></returns>
        public virtual Task WillDestroy() {

            return Task.CompletedTask;
        }

    }
}