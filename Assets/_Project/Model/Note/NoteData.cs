using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.Model {

    /// <summary>
    /// 
    /// </summary>
    public class NoteData {

        /// <summary>
        /// �ō��^�C�~���O
        /// </summary>
        public float raiseTiming;

        /// <summary>
        /// �m�[�c�̎��
        /// </summary>
        public int type;


        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public NoteData(float timing, int type) {
            this.raiseTiming = timing;
            this.type = type;
        }
    }

}