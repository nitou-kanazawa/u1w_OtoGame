using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OtoGame.DTO {
    using OtoGame.Model;

    public static class MusicDTOFormatter {

        /// <summary>
        /// �f�[�^�ϊ�
        /// </summary>
        public static MusicData ToMusicData(AudioClip clip, MusicDTO.EditData dto) {
            if (clip == null) throw new System.ArgumentNullException(nameof(clip));
            if (dto == null) throw new System.ArgumentNullException(nameof(dto));

            // 1��������̎��� [sec]
            float section = 60 / (float)dto.BPM;
            int lpb = dto.notes.First().LPB;        // ���S�m�[�g�ŋ��ʂ̒l���Ƃ����O��
            float beat = section / lpb;

            // ���`
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