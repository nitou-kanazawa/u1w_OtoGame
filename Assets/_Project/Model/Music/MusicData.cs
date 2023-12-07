using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.Model{

    public class MusicData{

        /// <summary>
        /// �Ȃ̉���
        /// </summary>
        public AudioClip Clip { get; set; }
        
        /// <summary>
        /// �Ȃ̃e���|�i��Beat per minute�j
        /// </summary>
        public int BPM { get; set; }

        /// <summary>
        /// 1���̕�����(Line per beat)
        /// </summary>
        public int LPB { get; set; }
        
        /// <summary>
        /// �m�[�c�̃^�C�~���O
        /// </summary>
        public float[] TimingArray { get; set; }
        
        /// <summary>
        /// �m�[�c�̎��
        /// </summary>
        public int[] KeyArray { get; set; }

        /// <summary>
        /// �m�[�c��
        /// </summary>
        public int NoteCount => TimingArray.Length;        
    }

}