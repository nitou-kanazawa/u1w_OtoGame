using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.Model {

    /// <summary>
    /// 
    /// </summary>
    public class NoteData {

        /// <summary>
        /// 打刻タイミング
        /// </summary>
        public float raiseTiming;

        /// <summary>
        /// ノーツの種類
        /// </summary>
        public int type;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NoteData(float timing, int type) {
            this.raiseTiming = timing;
            this.type = type;
        }
    }

}