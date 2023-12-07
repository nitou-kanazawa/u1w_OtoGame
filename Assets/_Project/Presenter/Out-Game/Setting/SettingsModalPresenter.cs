using System.Threading.Tasks;
using UniRx;
using nitou.UI;

namespace OtoGame.Presentation {
    using OtoGame.Model;
    using OtoGame.View;

    public sealed class SettingsModalPresenter : ModalPresenterBase<SettingsModal, SettingsView, SettingsViewState> {

        // Model
        private Settings _model;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public SettingsModalPresenter(SettingsModal view, ITransitionService transitionService, Settings settings)
            : base(view, transitionService) {
            _model = settings;
        }

        
        /// ----------------------------------------------------------------------------
        // Protected Method        
        
        /// <summary>
        /// ���[�_�������[�h���ꂽ����ɌĂ΂�鏈��
        /// </summary>
        protected override Task ViewDidLoad(SettingsModal view, SettingsViewState viewState) {

            // �I�����UI
            view.SelectableGroup = new SelectableGroup(view.root.CloseButton);

            // viewState�̏����l�ݒ�
            SetBgmSettingsViewState(viewState, _model.Sounds.Bgm.Volume, _model.Sounds.Bgm.Muted);
            SetSeSettingsViewState(viewState, _model.Sounds.Se.Volume, _model.Sounds.Se.Muted);

            // model�X�V�̊Ď��i���ʁj
            _model.Sounds.Bgm.VolumeRP.Subscribe(x => viewState.SoundSettings.BgmVolume.Value = x).AddTo(this);
            _model.Sounds.Se.VolumeRP.Subscribe(x => viewState.SoundSettings.SeVolume.Value = x).AddTo(this);
            // model�X�V�̊Ď��iON/OFF�j
            _model.Sounds.Bgm.MutedRP.Subscribe(x => viewState.SoundSettings.IsBgmEnabled.Value = !x).AddTo(this);
            _model.Sounds.Se.MutedRP.Subscribe(x => viewState.SoundSettings.IsSeEnabled.Value = !x).AddTo(this);


            // viewState�X�V�̊Ď��i���ʁj
            viewState.SoundSettings.BgmVolume.Subscribe(x => _model.Sounds.Bgm.Volume = x).AddTo(this);
            viewState.SoundSettings.SeVolume.Subscribe(x => _model.Sounds.Se.Volume = x).AddTo(this);
            // viewState�X�V�̊Ď��iON/OFF�j
            viewState.SoundSettings.IsBgmEnabled.Subscribe(x => _model.Sounds.Bgm.Muted = !x).AddTo(this);
            viewState.SoundSettings.IsSeEnabled.Subscribe(x => _model.Sounds.Se.Muted = !x).AddTo(this);


            // UI��ʑJ��
            viewState.CloseButtonClicked.Subscribe(_ => TransitionService.PopCommandExecuted()).AddTo(this);

            return Task.CompletedTask;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void SetVoiceSettingsViewState(SettingsViewState viewState, float volume, bool isMuted) {
            viewState.SoundSettings.VoiceVolume.Value = volume;
            viewState.SoundSettings.IsVoiceEnabled.Value = !isMuted;
        }

        private void SetBgmSettingsViewState(SettingsViewState viewState, float volume, bool isMuted) {
            viewState.SoundSettings.BgmVolume.Value = volume;
            viewState.SoundSettings.IsBgmEnabled.Value = !isMuted;
        }

        private void SetSeSettingsViewState(SettingsViewState viewState, float volume, bool isMuted) {
            viewState.SoundSettings.SeVolume.Value = volume;
            viewState.SoundSettings.IsSeEnabled.Value = !isMuted;
        }





    }
}
