/** 
 * Copyright (c) 2015 setchi
 * Released under the MIT license
 * https://github.com/setchi/NoteEditor/blob/master/LICENSE
 */
using System;
using System.Collections.Generic;

namespace OtoGame.DTO {

    /// <summary>
    /// "Notes Editor"の仕様に基づいたDTO
    /// </summary>
    [Serializable]
    public static class MusicDTO {

        [System.Serializable]
        public class EditData {

            /// <summary>
            /// 曲名
            /// </summary>
            public string name;
            
            /// <summary>
            /// 最大ブロック数（※縦軸の要素数）
            /// </summary>
            public int maxBlock;

            /// <summary>
            /// 曲のテンポ（※Beat per minute）
            /// </summary>
            public int BPM;

            /// <summary>
            /// 譜面の開始位置（※横軸のオフセット）
            /// </summary>
            public int offset;

            /// <summary>
            /// 配置されたノーツ
            /// </summary>
            public List<Note> notes;
        }


        /// <summary>
        /// ノート情報
        /// </summary>
        [System.Serializable]
        public class Note {

            /// <summary>
            /// 1拍の分割数(Line per beat)
            /// </summary>
            public int LPB;

            /// <summary>
            /// ライン番号（※横軸の位置）
            /// </summary>
            public int num;

            /// <summary>
            /// ブロック番号（※縦軸の位置）
            /// </summary>
            public int block;

            /// <summary>
            /// ノートの種類（※）
            /// </summary>
            public int type;
            
            /// <summary>
            /// 親リストへの参照
            /// </summary>
            //public List<Note> notes;
        }
    }
}