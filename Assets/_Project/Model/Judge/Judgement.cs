using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.Model {

    /// <summary>
    /// ノーツ打刻の判定結果
    /// </summary>
    public enum Judgement {
        // 
        EARLY,

        // ヒット
        GREAT,
        GOOD,
        BAD,

        // 
        LATE,

        NOJUDGE,
    }

    /// <summary>
    /// 判定結果を評価するための汎用メソッド
    /// </summary>
    public static class JudgementExtensitons {

        /// <summary>
        /// ノーツに当たったかどうか
        /// </summary>
        public static bool IsHit(this Judgement @this) {
            return @this == Judgement.GREAT || @this == Judgement.GOOD || @this == Judgement.BAD;
        }

        /// <summary>
        /// ノーツに上手く当たったかどうか
        /// </summary>
        public static bool IsSuccesHit(this Judgement @this) {
            return @this == Judgement.GREAT || @this == Judgement.GOOD;
        }
    }

}