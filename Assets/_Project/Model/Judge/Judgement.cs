using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.Model {

    /// <summary>
    /// �m�[�c�ō��̔��茋��
    /// </summary>
    public enum Judgement {
        // 
        EARLY,

        // �q�b�g
        GREAT,
        GOOD,
        BAD,

        // 
        LATE,

        NOJUDGE,
    }

    /// <summary>
    /// ���茋�ʂ�]�����邽�߂̔ėp���\�b�h
    /// </summary>
    public static class JudgementExtensitons {

        /// <summary>
        /// �m�[�c�ɓ����������ǂ���
        /// </summary>
        public static bool IsHit(this Judgement @this) {
            return @this == Judgement.GREAT || @this == Judgement.GOOD || @this == Judgement.BAD;
        }

        /// <summary>
        /// �m�[�c�ɏ�肭�����������ǂ���
        /// </summary>
        public static bool IsSuccesHit(this Judgement @this) {
            return @this == Judgement.GREAT || @this == Judgement.GOOD;
        }
    }

}