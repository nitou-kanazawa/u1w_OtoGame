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
    /// Model�f�[�^�̒�
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
    /// �A�v���P�[�V�����̍ŏ�ʊǗ��N���X (��Main�֐��ɑ���)
    /// </summary>
    public partial class AplicationMain : MonoBehaviour, ILauncherComponent,
        ILevelReferenceProvider, ISceneSwitcher, IDataProvider, IGameStageManager {

        public bool execute = true;

        /// ----------------------------------------------------------------------------
        // Field (�Q��)

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

        // �����R���|�[�l���g
        private TransitionService _transitionService;
        private AwaitableStateMachine<AplicationMain, GameStateEvent> _stateMachine;


        // Model�f�[�^
        public Settings Settings { get; private set; }
        public ResultData ResultData { get; private set; }


        // �V�[���Q��
        private TitleLevelReference _titleLevelReference;
        private StageLevelReference _stageLevelReference;

        private StageController _stageController;


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// ���������������Ă��邩�ǂ���
        /// </summary>
        public bool IsInitialized { get; private set; } = false;


        /// ----------------------------------------------------------------------------
        // Interface Method (�N������)

        /// <summary>
        /// �N��������
        /// </summary>
        UniTask ILauncherComponent.RuntimeInitialize(CancellationToken token) {
            if (!execute) return UniTask.CompletedTask;

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_screenContainer);
            DontDestroyOnLoad(_audioManager);

            // �X�e�[�g�}�V��
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

            //// ����p�R���|�[�l���g
            _stateMachine.EnqueueTransitionRequest<OutGameState>();
            _stateMachine.Run();

            // �t���O�X�V
            IsInitialized = true;

            return UniTask.CompletedTask;
        }


        /// ----------------------------------------------------------------------------
        // Interface Method (���x���Q��)

        bool ILevelReferenceProvider.TryGetTitleLevel(out TitleLevelReference levelReference) {
            levelReference = _titleLevelReference;
            return levelReference != null;
        }

        bool ILevelReferenceProvider.TryGetStageLevel(out StageLevelReference levelReference) {
            levelReference = _stageLevelReference;
            return levelReference != null;
        }


        /// ----------------------------------------------------------------------------
        // Interface Method (�V�[���؂�ւ�)

        async UniTask ISceneSwitcher.SwitchToInGame() {

            // �V�[���ǂݍ���
            await SceneManager.LoadSceneAsync(GameScene.Stage.ToString(), LoadSceneMode.Additive);
            
            // �X�e�[�g�؂�ւ�
            await _stateMachine.ForceState<InGameState>();
        }

        async UniTask ISceneSwitcher.SwitchToOutGame() {

            // �X�e�[�g�؂�ւ�
            await SceneManager.LoadSceneAsync(GameScene.Title.ToString(), LoadSceneMode.Additive);

            // �X�e�[�g�؂�ւ�
            await _stateMachine.ForceState<OutGameState>();
        }


        /// ----------------------------------------------------------------------------
        // Interface Method (�V�[���؂�ւ�)

        void IGameStageManager.StartInGame() {
            _stageController.StartGame();
        }

        void IGameStageManager.PauseGame() {
            // UI�\��
            _transitionService.Push_PausePage();
            // ��~
            _stageController.PauseGame();
        }

        void IGameStageManager.UnPauseGame() {
            // 
            _stageController.UnPauseGame();
        }

        async void IGameStageManager.ResetartGame() {

            _stageController.StopGame();
            _stageController.Setup();

            // ���X�^�[�g
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