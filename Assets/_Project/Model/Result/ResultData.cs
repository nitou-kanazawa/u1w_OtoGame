using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OtoGame.Model {

    /// <summary>
    /// 
    /// </summary>
    public class ResultData : System.IDisposable {

        /// ----------------------------------------------------------------------------
        // Field & Properity (�X�R�A)

        public IReadOnlyReactiveProperty<int> Score => _score;
        private IntReactiveProperty _score = new();


        /// ----------------------------------------------------------------------------
        // Field & Properity (�J�E���g��)

        public IReadOnlyReactiveProperty<int> GreatCount => _greatCount;
        public IReadOnlyReactiveProperty<int> GoodCount => _goodCount;
        public IReadOnlyReactiveProperty<int> BadCount => _badCount;
        public IReadOnlyReactiveProperty<int> LateCount => _lateCount;

        private IntReactiveProperty _greatCount = new();
        private IntReactiveProperty _goodCount = new();
        private IntReactiveProperty _badCount = new();
        private IntReactiveProperty _lateCount = new();



        /// ----------------------------------------------------------------------------
        // Field & Properity (�R���{��)

        public IReadOnlyReactiveProperty<int> Combo => _combo;
        private IntReactiveProperty _combo = new();

        public IReadOnlyReactiveProperty<float> Multipiler => _multipier;
        private FloatReactiveProperty _multipier = new();

        public int MaxComnbo { get; private set; }

        // �m�[�c����
        public int MaxCount { get; private set; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^ 
        /// </summary>
        public ResultData(int maxCount) {
            MaxCount = maxCount;
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
            _score.Dispose();

            _greatCount.Dispose();
            _goodCount.Dispose();
            _badCount.Dispose();
            _lateCount.Dispose();

            _combo.Dispose();
        }

        /// <summary>
        /// �X�R�A�̏�����
        /// </summary>
        public void Init() {
            _score.Value = 0;

            _greatCount.Value = 0;
            _goodCount.Value = 0;
            _badCount.Value = 0;
            _lateCount.Value = 0;

            _combo.Value = 0;
        }

        /// <summary>
        /// �R���{�����N���A����
        /// </summary>
        public void ClearCombo() {
            _combo.Value = 0;
        }

        /// <summary>
        /// �X�R�A�����Z����
        /// </summary>
        public void AddScore(Judgement judge) {
            // �J�E���g
            switch (judge) {
                case Judgement.GREAT: _greatCount.Value++; break;
                case Judgement.GOOD: _goodCount.Value++; break;
                case Judgement.BAD: _badCount.Value++; break;
                case Judgement.LATE: _lateCount.Value++; break;
                default: break;
            }

            // �X�V
            ComboCount(judge);
            _score.Value += GetIncreaseValue(judge);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �X�R�A�l�̍X�V����
        /// </summary>
        private int GetIncreaseValue(Judgement judge) {
            return (int)( _multipier.Value *
                judge switch {
                    Judgement.GREAT => 100,
                    Judgement.GOOD => 50,
                    Judgement.BAD => 10,
                    _ => 0
                });
        }     


        /// <summary>
        /// �R���{�����v�Z����
        /// </summary>
        private void ComboCount(Judgement judge) {

            // ���������ꍇ
            if (judge == Judgement.GREAT || judge == Judgement.GOOD) {
                _combo.Value++;
                if (_combo.Value > MaxComnbo) {
                    MaxComnbo = _combo.Value;
                }
            }
            // ���s�����ꍇ
            else {
                _combo.Value = 0;
            }

            // �W���̍X�V
            _multipier.Value = _combo.Value switch {
                int x when x >= 10 => 5f,
                int x when x >= 5 => 3f,
                _ => 1f
            };
        }

        /// ----------------------------------------------------------------------------
        // Private Method

        private int CalcFixedScore() {
            float clearCount = _greatCount.Value + _goodCount.Value * 0.5f + _badCount.Value * 0.1f;
            return (int)((clearCount / MaxCount) * 10000);
        }
    }

}