using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OtoGame.DTO {
    using OtoGame.Model;

    public static class MusicDTOFormatter {

        /// <summary>
        /// データ変換
        /// </summary>
        public static MusicData ToMusicData(AudioClip clip, MusicDTO.EditData dto) {
            if (clip == null) throw new System.ArgumentNullException(nameof(clip));
            if (dto == null) throw new System.ArgumentNullException(nameof(dto));

            // 1拍当たりの時間 [sec]
            float section = 60 / (float)dto.BPM;
            int lpb = dto.notes.First().LPB;        // ※全ノートで共通の値だという前提
            float beat = section / lpb;

            // 成形
            var musicData = new MusicData() {
                Clip = clip,
                BPM = dto.BPM,
                LPB = lpb,
                TimingArray = dto.notes.Select(n => n.num * beat).ToArray(),
                KeyArray = dto.notes.Select(n => n.block).ToArray(),
            };

            return musicData;
        }

    }

}