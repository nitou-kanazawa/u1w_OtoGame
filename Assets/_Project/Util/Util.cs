using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.Utility {

    public static class Util {

        /// <summary>
        /// �P�ʕϊ��i[bpm]=>[sec]�j
        /// </summary>
        public static float Bpm2Sec(float bpm) =>
            (bpm != 0) ? 60 / bpm : throw new System.ArgumentException();
    }

}