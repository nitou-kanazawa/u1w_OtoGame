using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using nitou;
using nitou.UI;
using nitou.DesignPattern;
using nitou.Launcher;

namespace OtoGame.Composition {
    using OtoGame.Model;
    using OtoGame.LevelObjects;
    using OtoGame.DTO;
    using OtoGame.View;

    public enum GameScene {
        Title,
        Stage,
        Result,
        BootScene,
    }



    /// <summary>
    /// Modelデータの提供
    /// </summary>
    public interface IDataProvider {
        public Settings Settings { get; }
        public ResultData ResultData { get; }
        public AudioManager AudioManager { get; }
    }

    public interface IGameStageManager {
        public void StartInGame();
        public void PauseGame();
        public void UnPauseGame();
        public void ResetartGame();
        public void QuitGame();
    }



    /// <summary>
    /// アプリケーションの最上位管理クラス (※Main関数に相当)
    /// </summary>
    public partial class AplicationMain : MonoBehaviour, ILauncherComponent,
        ILevelReferenceProvider, ISceneSwitcher, IDataProvider, IGameStageManager {

        public bool execute = true;

        /// ----------------------------------------------------------------------------
        // Field (参照)

        [Title("UI")]
        [SerializeField] private ScreenContainer _screenContainer;

        [Title("Audio")]
        [SerializeField] private AudioManager _audioManager;

        [Title("Audio Data")]
        [SerializeField] private AudioClip _clip;
        [SerializeField] private TextAsset _scoreJson;

        [Title("Others")]
        [SerializeField] private AudioClip _bgm;


        /// <summary>
        /// 
        /// </summary>
        public MusicData musicData { get; private set; }

        public AudioManager AudioManager => _audioManager;


        /// ----------------------------------------------------------------------------

        // 内部コンポーネント
        private TransitionService _transitionService;
        private AwaitableStateMachine<AplicationMain, GameStateEvent> _stateMachine;


        // Modelデータ
        public Settings Settings { get; private set; }
        public ResultData ResultData { get; private set; }


        // シーン参照
        private TitleLevelReference _titleLevelReference;
        private StageLevelReference _stageLevelReference;

        private StageController _stageController;


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// 初期化が完了しているかどうか
        /// </summary>
        public bool IsInitialized { get; private set; } = false;


        /// ----------------------------------------------------------------------------
        // Interface Method (起動処理)

        /// <summary>
        /// 起動時処理
        /// </summary>
        UniTask ILauncherComponent.RuntimeInitialize(CancellationToken token) {
            if (!execute) return UniTask.CompletedTask;

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_screenContainer);
            DontDestroyOnLoad(_audioManager);

            // ステートマシン
            _stateMachine = new AwaitableStateMachine<AplicationMain, GameStateEvent>(this);
            _stateMachine.RegisterState<OutGameState>();
            _stateMachine.RegisterState<InGameState>();

            // UI
            _transitionService = new TransitionService(_screenContainer, this, this, this, this);

            // Models
            Settings = new Settings();

            Settings.Sounds.Bgm.Volume = 0.3f;
            Settings.Sounds.Se.Volume = 0.2f;
            Settings.Sounds.Bgm.VolumeRP.Subscribe(x => _audioManager.Volume = x).AddTo(this);

            // ------

            //// 制御用コンポーネント
            _stateMachine.EnqueueTransitionRequest<OutGameState>();
            _stateMachine.Run();

            // フラグ更新
            IsInitialized = true;

            return UniTask.CompletedTask;
        }


        /// ----------------------------------------------------------------------------
        // Interface Method (レベル参照)

        bool ILevelReferenceProvider.TryGetTitleLevel(out TitleLevelReference levelReference) {
            levelReference = _titleLevelReference;
            return levelReference != null;
        }

        bool ILevelReferenceProvider.TryGetStageLevel(out StageLevelReference levelReference) {
            levelReference = _stageLevelReference;
            return levelReference != null;
        }


        /// ----------------------------------------------------------------------------
        // Interface Method (シーン切り替え)

        async UniTask ISceneSwitcher.SwitchToInGame() {

            // シーン読み込み
            await SceneManager.LoadSceneAsync(GameScene.Stage.ToString(), LoadSceneMode.Additive);
            
            // ステート切り替え
            await _stateMachine.ForceState<InGameState>();
        }

        async UniTask ISceneSwitcher.SwitchToOutGame() {

            // ステート切り替え
            await SceneManager.LoadSceneAsync(GameScene.Title.ToString(), LoadSceneMode.Additive);

            // ステート切り替え
            await _stateMachine.ForceState<OutGameState>();
        }


        /// ----------------------------------------------------------------------------
        // Interface Method (シーン切り替え)

        void IGameStageManager.StartInGame() {
            _stageController.StartGame();
        }

        void IGameStageManager.PauseGame() {
            // UI表示
            _transitionService.Push_PausePage();
            // 停止
            _stageController.PauseGame();
        }

        void IGameStageManager.UnPauseGame() {
            // 
            _stageController.UnPauseGame();
        }

        async void IGameStageManager.ResetartGame() {

            _stageController.StopGame();
            _stageController.Setup();

            // リスタート
            await UniTask.Delay(System.TimeSpan.FromSeconds(2f));
            _stageController.StartGame();
        }

        void IGameStageManager.QuitGame() {

        }


        private void OnApplicationFocus(bool focus) {

        }

        /// ----------------------------------------------------------------------------
        // Private Method

        private MusicData LoadScoreData(AudioClip clip, TextAsset scoreJson) {
            var dto = JsonUtility.FromJson<MusicDTO.EditData>(scoreJson.ToString());
            return MusicDTOFormatter.ToMusicData(clip, dto);
        }
    }

}