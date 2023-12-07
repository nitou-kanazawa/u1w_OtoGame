using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nitou;

namespace OtoGame.Model {

    /// <summary>
    /// �m�[�c�̃q�b�g����p���W�b�N
    /// </summary>
    public static class NoteHit {

        // �����臒l
        public static float JUDGE_GREAT_TIME = 0.03f;
        public static float JUDGE_GOOD_TIME = 0.07f;
        public static float JUDGE_BAD_TIME = 0.1f;

        /// <summary>
        /// 
        /// </summary>
        public static Judgement Judge(NoteData note, float audioTime) {

            Judgement judge = Judgement.NOJUDGE;

            var timing = note.raiseTiming;
            var error = Mathf.Abs(timing - audioTime);

            // �����iGREAT�j�̏ꍇ,
            if (error <= JUDGE_GREAT_TIME) {
                judge = Judgement.GREAT;
            }
            // �����iGOOD�j�̏ꍇ,
            else if (error <= JUDGE_GOOD_TIME) {
                judge = Judgement.GOOD;
            }
            // �����iBAD�j�̏ꍇ,
            else if (error <= JUDGE_BAD_TIME) {
                judge = Judgement.BAD;
            }

            // ���s�̏ꍇ,
            else {
                return (timing - audioTime > 0) ? Judgement.EARLY : Judgement.LATE;
            }

            // ���M���O
            var color = judge switch {
                Judgement.GREAT => Color.green,
                Judgement.GOOD => Color.yellow,
                Judgement.BAD => Color.red,
                _ => Color.black,
            };
            Debug_.Log($"[{audioTime}]  {judge.ToString()} :  {error} [sec]", color);

            // 
            return judge;
        }
    }

}