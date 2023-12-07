using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// [�Q�l]
//  PG����: JsonUtility�Ŕz��ƃ��X�g���������� https://takap-tech.com/entry/2021/02/02/222406

namespace nitou {

    /// <summary>
    /// <see cref="JsonUtility"/> �ɕs�����Ă���@�\��񋟂���w���p�[�N���X
    /// </summary>
    public class JsonHelper : MonoBehaviour {

        /// ----------------------------------------------------------------------------
        // Public Method (�ϊ�)

        /// <summary>
        /// �w�肵�� string �� Root �I�u�W�F�N�g�������Ȃ� JSON �z��Ɖ��肵�ăf�V���A���C�Y���܂��B
        /// </summary>
        public static T[] FromJson<T>(string json) {
            // ���[�g�v�f������Δz�񓙂̕ϊ����ł��邽�߁C
            // ���͂��ꂽJSON�ɑ΂���(��)�̍s��ǉ�����
            //
            // e.g.
            // �� {
            // ��     "array":
            //        [
            //            ...
            //        ]
            // �� }
            //
            string dummy_json = $"{{\"{DummyNode<T>.ROOT_NAME}\": {json}}}";

            // �_�~�[�̃��[�g�Ƀf�V���A���C�Y���Ă��璆�g�̔z���Ԃ�
            var obj = JsonUtility.FromJson<DummyNode<T>>(dummy_json);
            return obj.array;
        }

        /// <summary>
        /// �w�肵���z��⃊�X�g�Ȃǂ̃R���N�V������ Root �I�u�W�F�N�g�������Ȃ� JSON �z��ɂ���
        /// </summary>
        /// <remarks>
        /// 'prettyPrint' �ɂ͔�Ή��B���`������������ʓr�ϊ�����
        /// </remarks>
        public static string ToJson<T>(IEnumerable<T> collection) {
            string json = JsonUtility.ToJson(new DummyNode<T>(collection)); // �_�~�[���[�g���ƃV���A��������
            int start = DummyNode<T>.ROOT_NAME.Length + 4;
            int len = json.Length - start - 1;
            return json.Substring(start, len); // �ǉ����[�g�̕�������菜���ĕԂ�
        }


        /// ----------------------------------------------------------------------------
        // Inner class 

        // �����Ŏg�p����_�~�[�̃��[�g�v�f
        [Serializable]
        private struct DummyNode<T> {
            // �⑫:
            // �������Ɉꎞ�g�p�������J�N���X�̂��ߑ����݌v���ςł��C�ɂ��Ȃ�

            // JSON�ɕt�^����_�~�[���[�g�̖���
            public const string ROOT_NAME = nameof(array);
            // �^���I�Ȏq�v�f
            public T[] array;
            // �R���N�V�����v�f���w�肵�ăI�u�W�F�N�g���쐬����
            public DummyNode(IEnumerable<T> collection) => this.array = collection.ToArray();
        }
        
    }
}