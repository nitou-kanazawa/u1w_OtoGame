using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OtoGame.APIGateway {

    public class SettingsAPIGateway {

        private const string BgmVolumePrefsKey = "Setting_Bgm_Volume";
        private const string SeVolumePrefsKey = "Setting_Se_Volume";

        private const string IsBgmMutedPrefsKey = "Setting_Bgm_Muted";
        private const string IsSeMutedPrefsKey = "Setting_Se_Muted";


        /// ----------------------------------------------------------------------------
        // Properity

        private static float BgmVolume {
            get => PlayerPrefs.GetFloat(BgmVolumePrefsKey, 0.3f);
            set => PlayerPrefs.SetFloat(BgmVolumePrefsKey, value);
        }

        private static float SeVolume {
            get => PlayerPrefs.GetFloat(SeVolumePrefsKey, 0.2f);
            set => PlayerPrefs.SetFloat(SeVolumePrefsKey, value);
        }

        private static bool IsBgmMuted {
            get => PlayerPrefs.GetInt(IsBgmMutedPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsBgmMutedPrefsKey, value ? 1 : 0);
        }

        private static bool IsSeMuted {
            get => PlayerPrefs.GetInt(IsSeMutedPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(IsSeMutedPrefsKey, value ? 1 : 0);
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 設定データの読み込み
        /// </summary>
        public UniTask<FetchSoundSettingsResponse> FetchSoundSettingsAsync() {
            var soundSettings = new FetchSoundSettingsResponse(
                BgmVolume,
                SeVolume,
                IsBgmMuted,
                IsSeMuted
            );
            return UniTask.FromResult(soundSettings);
        }

        /// <summary>
        /// 設定データの保存
        /// </summary>
        public UniTask SaveSoundSettingsAsync(SaveSoundSettingsRequest request) {
            IsBgmMuted = request.IsBgmMuted;
            IsSeMuted = request.IsSeMuted;
            BgmVolume = request.BgmVolume;
            SeVolume = request.SeVolume;
            return UniTask.CompletedTask;
        }


        /// ----------------------------------------------------------------------------
        #region Requests

        public readonly struct SaveSoundSettingsRequest {

            /// <summary>
            /// コンストラクタ
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


        /// ----------------------------------------------------------------------------
        #region Responses

        public readonly struct FetchSoundSettingsResponse {

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public FetchSoundSettingsResponse(float bgmVolume, float seVolume, bool isBgmMuted, bool isSeMuted) {
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