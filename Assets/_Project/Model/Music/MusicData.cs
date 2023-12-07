using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.Model{

    public class MusicData{

        /// <summary>
        /// 曲の音源
        /// </summary>
        public AudioClip Clip { get; set; }
        
        /// <summary>
        /// 曲のテンポ（※Beat per minute）
        /// </summary>
        public int BPM { get; set; }

        /// <summary>
        /// 1拍の分割数(Line per beat)
        /// </summary>
        public int LPB { get; set; }
        
        /// <summary>
        /// ノーツのタイミング
        /// </summary>
        public float[] TimingArray { get; set; }
        
        /// <summary>
        /// ノーツの種類
        /// </summary>
        public int[] KeyArray { get; set; }

        /// <summary>
        /// ノーツ数
        /// </summary>
        public int NoteCount => TimingArray.Length;        
    }

}