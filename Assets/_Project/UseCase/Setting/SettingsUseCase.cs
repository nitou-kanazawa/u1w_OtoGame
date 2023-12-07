using Cysharp.Threading.Tasks;

namespace OtoGame.UseCase {
    using OtoGame.Model;
    using OtoGame.APIGateway;

    public class SettingsUseCase {

        private readonly SettingsAPIGateway _apiGateway;

        public Settings Model { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public SettingsUseCase(Settings model, SettingsAPIGateway apiGateway) {

            Model = model;
            _apiGateway = apiGateway;
        }

        /// <summary>
        /// �ݒ�f�[�^�̓ǂݍ���
        /// </summary>
        public async UniTask FetchSoundSettingsAsync() {
            
            var response = await _apiGateway.FetchSoundSettingsAsync();

            var sounds = Model.Sounds;
            sounds.Bgm.SetValues(response.BgmVolume, response.IsBgmMuted);
            sounds.Se.SetValues(response.SeVolume, response.IsSeMuted);
        }


        /// ----------------------------------------------------------------------------
        #region Requests

        public readonly struct SaveSoundSettingsRequest {

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            public SaveSoundSettingsRequest(float bgmVolume, float seVolume, bool isBgmMuted, bool isSeMuted) {
                BgmVolume = bgmVolume;
                SeVolume = seVolume;
                IsBgmMuted = isBgmMuted;
                IsSeMuted = isSeMuted;
            }

            public float BgmVolume { get; }
            public float SeVolume { get; }
            public bool IsBgmMuted { get; }
            public bool IsSeMuted { get; }
        }

        #endregion
    }

}