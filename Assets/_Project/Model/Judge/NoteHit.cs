using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nitou;

namespace OtoGame.Model {

    /// <summary>
    /// ノーツのヒット判定用ロジック
    /// </summary>
    public static class NoteHit {

        // 判定の閾値
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

            // 成功（GREAT）の場合,
            if (error <= JUDGE_GREAT_TIME) {
                judge = Judgement.GREAT;
            }
            // 成功（GOOD）の場合,
            else if (error <= JUDGE_GOOD_TIME) {
                judge = Judgement.GOOD;
            }
            // 成功（BAD）の場合,
            else if (error <= JUDGE_BAD_TIME) {
                judge = Judgement.BAD;
            }

            // 失敗の場合,
            else {
                return (timing - audioTime > 0) ? Judgement.EARLY : Judgement.LATE;
            }

            // ロギング
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