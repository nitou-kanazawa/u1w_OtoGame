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

        // �Q��
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
        /// �������������������ǂ���
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// ���s�ł����Ԃ��ǂ���
        /// </summary>
        public bool IsSetuped { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public event System.Action<Judgement> OnAttack = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// ����������
        /// </summary>
        public void Initialize() {
            _audioSource = GetComponent<AudioSource>();
            _playerAnim.Initialize();



            IsInitialized = true;
        }

        public void Setup(NotesManager notesManager, ResultData resultData) {
            if (!IsInitialized) throw new System.Exception("�R���|�[�l���g������������Ă��܂���");

            _notesManager = notesManager;
            _resultData = resultData;

            _particle.transform.position = _notesManager.GetHitPos();

            // �t���O�X�V
            IsSetuped = true;
        }

        public void Teardown() {
            if (!IsInitialized) throw new System.Exception("�R���|�[�l���g������������Ă��܂���");

            _notesManager = null;
            _resultData = null;

            // �t���O�X�V
            IsSetuped = false;
        }

        /// ----------------------------------------------------------------------------
        // Event Method 

        /// <summary>
        /// Audio�Đ��ɍ��킹���X�V����
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

            // �ꎞ�ϐ�
            Judgement judge = Judgement.NOJUDGE;

            // �Ώۃm�[�c�ɑ΂��鏈��
            if (_notesManager.TryGetNearestNote(audioTime, out var note) && !note.IsKnocked) {

                // ����
                judge = NoteHit.Judge(note.Data, audioTime);

                // �q�b�g�����ꍇ�C
                if (judge.IsHit()) {
                    note.OnHit(judge);
                    _particle.Play();
                }
            }


            // �ǉ��̃G�t�F�N�g����
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