using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.Presentation {
    using OtoGame.Model;
    using OtoGame.View;
    using OtoGame.LevelObjects;

    public class HUDPageLevelPresenter : PagePresenter<HUDPage> {

        // ���f��
        private ResultData _model;

        // �V�[���Q��
        private readonly StageLevelReference _levelReference;

        private IntReactiveProperty _showNum = new(0);
        CompositeDisposable disposables;

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public HUDPageLevelPresenter(HUDPage page, StageLevelReference levelReference, ResultData resultData) : base(page) {
            if (levelReference == null) {
                throw new ArgumentNullException(nameof(levelReference));
            }
            this._levelReference = levelReference;
            this._model = resultData;
        }


        /// <summary>
        /// �y�[�W�����[�h���ꂽ����ɌĂ΂�鏈��
        /// </summary>
        protected override Task ViewDidLoad(HUDPage view) {


            // �ϋq
            disposables = new CompositeDisposable();
            _model.Score.Subscribe(x => _showNum.Value = x/10000).AddTo(disposables);
            _showNum.Subscribe(x => _levelReference.ThardParty.ShowBoat(x)).AddTo(disposables);

            return base.ViewDidLoad(view);
        }

        protected override Task ViewWillDestroy(HUDPage view) {
            disposables.Dispose();
            return base.ViewWillDestroy(view);
        }

        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// Push�J�ڂŕ\������钼�O�̏���
        /// </summary>
        protected async override Task ViewWillPushEnter(HUDPage view) {

            await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));

            await _levelReference.HideFrontWater();

        }

        /// <summary>
        /// Push�J�ڂŔ�\���ɂȂ钼�O�̏���
        /// </summary>
        protected async override Task ViewWillPushExit(HUDPage view) {
        }

    }

}