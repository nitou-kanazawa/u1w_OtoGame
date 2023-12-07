using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using UniRx;
using Sirenix.OdinInspector;
using nitou;

namespace OtoGame.LevelObjects {
    using OtoGame.Model;
    using OtoGame.Utility;

    /// <summary>
    /// �m�[�g���Ǘ�����R���|�[�l���g
    /// </summary>
    public class NotesManager : MonoBehaviour, IMusicReactor {

        /// ----------------------------------------------------------------------------
        // Field & Properity

        [Title("Prefab objects")]

        [AssetsOnly]
        [SerializeField] private NoteInstance _notePrefab;

        [AssetsOnly]
        [SerializeField] private GameObject _linePrefab;

        [AssetsOnly]
        [SerializeField] private GameObject _hitPosPrefab;


        /// ----------------------------------------------------------------------------
        // Field & Properity

        [Title("Notes path")]
        public SplineContainer _splineContainer;
        public float _preBeatCount = 4f;
        public float _postBeatCount = 1f;


        // �p�X�̑Ή�����
        private float _preDuration;     // �O���i���ňʒu�܂ł͈̔́j
        private float _postDuration;    // ����i���ňʒu���߂����͈́j

        // �p�X�̑Ή��p�����[�^
        private float _hitPosParam;     // �m�[�c��@���ʒu�̃p�����[�^�i���l��[0~1]�j
        private float _oneBeetParam;    // 1Beat�ɑΉ�����p�����[�^�i���l��[0~1]�j


        /// ----------------------------------------------------------------------------
        // Field & Properity

        /// <summary>
        /// �Ώۂ̉���
        /// </summary>
        public AudioClip TargetClip { get; private set; }

        // ���ʏ��
        private int _notesCount;
        private float[] _timingArray;
        private int[] _typeArray;

        /// <summary>
        /// ���݂̕��ʈʒu�i����ʂɕ\�����n�߂�m�[�c�j
        /// </summary>
        [ShowInInspector, ReadOnly]
        public int NextIndex { get; private set; }

        // �I�u�W�F�N�g�v�[��
        private NoteInstancePool _pool;
        private List<NoteInstance> _activeNoteList = new();

        // �⏕���I�u�W�F�N�g
        private List<GameObject> _lineList = new();


        /// <summary>
        /// �������������������ǂ���
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// ���s�ł����Ԃ��ǂ���
        /// </summary>
        public bool IsSetuped { get; private set; } = false;

        AudioClip IMusicReactor.TargetClip => throw new System.NotImplementedException();

        bool IMusicReactor.IsSetuped => throw new System.NotImplementedException();


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 


        private void OnDestroy() {
            _pool?.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// ����������
        /// </summary>
        public void Initialize() {
            if (IsInitialized) { return; }

            // �I�u�W�F�N�g�v�[���̐���
            _pool = new NoteInstancePool(_notePrefab, this.transform, GetSplinePos(0f));
            _pool.PreloadAsync(10, 10).Subscribe().AddTo(this);

            // �t���O�X�V
            IsInitialized = true;
        }


        /// <summary>
        /// �����f�[�^��^���ăZ�b�g�A�b�v����
        /// </summary>
        /// <param name="musicData"></param>
        public void Setup(MusicData musicData) {
            if (!IsInitialized) throw new System.Exception("�R���|�[�l���g������������Ă��܂���");

            // ���Ƀf�[�^�o�^�ς݂̏ꍇ�C��ɔj������
            if (IsSetuped) {
                Teardown();
            }

            // �����E���ʏ��
            TargetClip = musicData.Clip;
            _notesCount = musicData.TimingArray.Count();
            _timingArray = musicData.TimingArray;
            _typeArray = musicData.KeyArray;

            // �p�����[�^�i���ԁj
            var oneBeat = Util.Bpm2Sec(musicData.BPM);
            _preDuration = oneBeat * _preBeatCount;
            _postDuration = oneBeat * _postBeatCount;

            // �p�����[�^�i���K�l�j
            _oneBeetParam = 1.0f / (_preBeatCount + _postBeatCount);
            _hitPosParam = _preBeatCount / (_preBeatCount + _postBeatCount);

            // �⏕���I�u�W�F�N�g�̐���
            CreateAxulayObjects();

            // ���������p
            NextIndex = 0;


            // �t���O�X�V
            IsSetuped = true;
        }

        /// <summary>
        /// �����f�[�^���N���A����
        /// </summary>
        public void Teardown() {
            if (!IsInitialized) throw new System.Exception("�R���|�[�l���g������������Ă��܂���");

            // �����E���ʏ��
            TargetClip = null;
            _notesCount = 0;
            _timingArray = null;
            _typeArray = null;

            // ���ԋp�C���X�^���X������
            foreach(var note in _activeNoteList) {
                _pool.Return(note);
            }
            _activeNoteList.Clear();

            // �t���O�X�V
            IsSetuped = false;
        }


        /// <summary>
        /// �w��̃I�[�f�B�I�Đ����Ԃɍł��߂��m�[�c���擾����
        /// </summary>
        public bool TryGetNearestNote(float audioTime, out NoteInstance nearest) {

            if (_activeNoteList.Count == 0) {
                nearest = null;
                return false;
            }

            // ���S�T���Ȃ̂ŁCfor�őł��؂�ɂ��Ă��悢
            nearest = _activeNoteList
                .OrderBy(n => Mathf.Abs(n.RaiseTime - audioTime))
                .First();
            return true;
        }

        /// <summary>
        /// �w��̃I�[�f�B�I�Đ����Ԃ����O�̃m�[�c���m�b�N�ςݏ�Ԃɂ���
        /// </summary>
        public int CountUnKnockNotes(float audioTime) {
            if (_activeNoteList.Count == 0) {
                return 0;
            }

            return _activeNoteList.Count(n => n.RaiseTime < audioTime && !n.IsKnocked);
        }


        public Vector3 GetHitPos() {
            return GetSplinePos(_hitPosParam);
        }

        /// ----------------------------------------------------------------------------
        // Event Method 

        /// <summary>
        /// Audio�Đ��ɍ��킹���X�V����
        /// </summary>
        void IMusicReactor.UpdateWithAudio(float audioTime) {
            if (!IsSetuped) return;

            // �m�[�c�̍��W�X�V
            UpdateActiveNotes(audioTime);

            // �m�[�c�̒ǉ�
            if ((NextIndex < _notesCount)
            && (audioTime > _timingArray[NextIndex] - _preDuration)) {
                AddNote(_timingArray[NextIndex], _typeArray[NextIndex]);
                NextIndex++;
            }
        }


        void IMusicReactor.OnPause() {
            
        }

        void IMusicReactor.OnUnPause() {
        
        }

        void IMusicReactor.OnStop() {
            Teardown();
        }

        /// ----------------------------------------------------------------------------
        // Private Method �i�����������j

        /// <summary>
        /// �⏕���̐���
        /// </summary>
        private void CreateAxulayObjects() {

            // ���݂̃I�u�W�F�N�g��j��
            DeleteAxulayObjects();


            float delta = _oneBeetParam != 0 ? _oneBeetParam : throw new System.Exception($"{nameof(_oneBeetParam)}�l���s���ł�");
            var hitParam = _hitPosParam;

            // ��_�i���q�b�g�ʒu�j
            InstantiateLine(_hitPosPrefab, hitParam);

            // �O��
            for (float p = hitParam - delta; p >= 0; p -= delta) {
                InstantiateLine(_linePrefab, p);
            }
            // ���
            for (float p = hitParam + delta; p <= 1; p += delta) {
                InstantiateLine(_linePrefab, p);
            }

            ///
            void InstantiateLine(GameObject prefab, float param) {
                var lineObj = GameObject.Instantiate(prefab, _splineContainer.transform);
                lineObj.transform.position = GetSplinePos(param);
            }
        }

        /// <summary>
        /// �⏕���̍폜
        /// </summary>
        private void DeleteAxulayObjects() {
            foreach(var obj in _lineList) {
                obj.Destroy();
            }
            _lineList.Clear();
        }


        /// ----------------------------------------------------------------------------
        // Private Method 


        /// <summary>
        /// �\����Ԃ̃m�[�c�̍X�V
        /// </summary>
        private void UpdateActiveNotes(float currentTime) {

            bool darty = false;
            foreach (var note in _activeNoteList) {

                // Audio�̍Đ����ԂɑΉ������p�����[�^�ϐ�
                var min = note.RaiseTime - _preDuration;
                var max = note.RaiseTime + _postDuration;
                var progress = math.remap(min, max, 0, 1, currentTime);

                // �p�X��̍��W�l�ɕϊ�
                note.Progress = progress;

                var position = GetSplinePos(progress);
                var tangent = _splineContainer.EvaluateTangent(progress);
                var upVector = _splineContainer.EvaluateUpVector(progress);

                // 
                note.transform.SetPositionAndRotation(
                    position,
                    Quaternion.LookRotation(tangent, upVector)
                );

                if (progress >= 1) {
                    _pool.Return(note);
                    darty = true;
                }
            }

            // ���X�g���X�V
            if (darty) {
                _activeNoteList = _activeNoteList.Where(n => n.Progress < 1).ToList();
            }
        }

        /// <summary>
        /// �m�[�c��\����Ԃɐ؂�ւ���
        /// </summary>
        private void AddNote(float timing, int type) {
            var note = _pool.Rent();
            note.Setup(new NoteData(timing, type));

            _activeNoteList.Add(note);
        }

        /// <summary>
        /// �w��p�����[�^�i�l��:0~1�j�ɑΉ������X�v���C����̍��W���擾
        /// </summary>
        private Vector3 GetSplinePos(float progress) {
            return _splineContainer.EvaluatePosition(progress);
        }

    }

}