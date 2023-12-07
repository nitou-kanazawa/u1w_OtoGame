using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OtoGame.LevelObjects {
    using OtoGame.Model;

    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour, IMusicReactor {

        /// ----------------------------------------------------------------------------
        // Field & Properiy

        [TitleGroup("Reference components"), Indent]
        [SerializeField] private PlayerAnimation _playerAnim;

        // 参照
        private NotesManager _notesManager;
        private ResultData _resultData;
        private AudioSource _audioSource;

        [Title("AudioClip")]
        [SerializeField] private AudioClip _greatSlashClip;
        [SerializeField] private AudioClip _goodSlashClip;
        [SerializeField] private AudioClip _badSlashClip;

        [Title("")]
        [SerializeField] private ParticleSystem _particle;

        /// ----------------------------------------------------------------------------
        // Properiy

        public AudioClip TargetClip { get; private set; }

        /// <summary>
        /// 初期化が完了したかどうか
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// 実行できる状態かどうか
        /// </summary>
        public bool IsSetuped { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public event System.Action<Judgement> OnAttack = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize() {
            _audioSource = GetComponent<AudioSource>();
            _playerAnim.Initialize();



            IsInitialized = true;
        }

        public void Setup(NotesManager notesManager, ResultData resultData) {
            if (!IsInitialized) throw new System.Exception("コンポーネントが初期化されていません");

            _notesManager = notesManager;
            _resultData = resultData;

            _particle.transform.position = _notesManager.GetHitPos();

            // フラグ更新
            IsSetuped = true;
        }

        public void Teardown() {
            if (!IsInitialized) throw new System.Exception("コンポーネントが初期化されていません");

            _notesManager = null;
            _resultData = null;

            // フラグ更新
            IsSetuped = false;
        }

        /// ----------------------------------------------------------------------------
        // Event Method 

        /// <summary>
        /// Audio再生に合わせた更新処理
        /// </summary>
        void IMusicReactor.UpdateWithAudio(float audioTime) {
            if (!IsSetuped) return;

            // Check
            var cnt = _notesManager.CountUnKnockNotes(audioTime - 0.1f);
            if (cnt > 0) {
                
                _resultData.ClearCombo();
            }

            if (Input.GetKeyDown(KeyCode.Return)) {
                OnAttackProcess(audioTime);
            }
        }

        void IMusicReactor.OnPause() {
            _playerAnim.SetAnimationSpeed(0f);
        }

        void IMusicReactor.OnUnPause() {
            _playerAnim.SetAnimationSpeed(1f);
        }

        void IMusicReactor.OnStop() {
            Teardown();
        }

        /// ----------------------------------------------------------------------------
        // Private Method 

        private void OnAttackProcess(float audioTime) {

            // 一時変数
            Judgement judge = Judgement.NOJUDGE;

            // 対象ノーツに対する処理
            if (_notesManager.TryGetNearestNote(audioTime, out var note) && !note.IsKnocked) {

                // 判定
                judge = NoteHit.Judge(note.Data, audioTime);

                // ヒットした場合，
                if (judge.IsHit()) {
                    note.OnHit(judge);
                    _particle.Play();
                }
            }


            // 追加のエフェクト処理
            if (judge.IsSuccesHit()) {
                _audioSource.PlayOneShot(_greatSlashClip);
            } else {
                _audioSource.PlayOneShot(_badSlashClip);
            }

            // 
            _resultData.AddScore(judge);
            OnAttack.Invoke(judge);
        }
    }

}