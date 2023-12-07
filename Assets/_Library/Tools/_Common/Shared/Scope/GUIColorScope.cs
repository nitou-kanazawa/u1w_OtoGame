using System;
using UnityEngine;

namespace nitou.Tools {

    /// <summary>
    /// GUI�J���[�ݒ���X�R�[�v�ŊǗ����邽�߂̃N���X
    /// </summary>
    public sealed class GUIColorScope : IDisposable {

        private readonly Color _oldColor;


        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public GUIColorScope(Color color) {
            _oldColor = GUI.color;
            GUI.color = color;
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
            GUI.color = _oldColor;
        }
    }

}