using System;
using UnityEngine;

namespace nitou.Tools {

    /// <summary>
    /// GUIカラー設定をスコープで管理するためのクラス
    /// </summary>
    public sealed class GUIColorScope : IDisposable {

        private readonly Color _oldColor;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GUIColorScope(Color color) {
            _oldColor = GUI.color;
            GUI.color = color;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            GUI.color = _oldColor;
        }
    }

}