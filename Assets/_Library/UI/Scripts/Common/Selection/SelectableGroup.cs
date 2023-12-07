using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace nitou.UI {

    /// <summary>
    /// "Selectable"を管理するクラス
    /// </summary>
    [Serializable]
    public partial class SelectableGroup : IDisposable {

        /// <summary>
        /// 対象UIのリスト
        /// </summary>
        [ShowInInspector, ReadOnly]
        public IReadOnlyList<Selectable> Selectables => _selectables;
        private List<Selectable> _selectables = null;

        /// <summary>
        /// 最初の要素
        /// </summary>
        public Selectable First => _selectables.First();

        /// <summary>
        /// 現在の選択情報
        /// </summary>
        public Selectable Current { get; private set; }


        // イベント通知
        public event Action<GameObject> OnPointerEnter = delegate { };
        public event Action<GameObject> OnPointerExit = delegate { };
        public event Action<GameObject> OnSelect = delegate { };

        // リストクリア用
        private event Action OnListRelesed = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectableGroup(Selectable selectable) {
            if(selectable==null) throw new System.ArgumentNullException(nameof(selectable));

            _selectables = new List<Selectable>() { selectable };
            ResetSelection();
            AttachComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SelectableGroup(List<Selectable> selectables) {
            _selectables = selectables ?? throw new System.ArgumentNullException(nameof(selectables));
            ResetSelection();
            AttachComponent();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose() {
            OnListRelesed?.Invoke();

            _selectables?.Clear();
            _selectables = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        public void SetIntarability(bool value) {
            _selectables.ForEach(s => s.interactable = value);
        }

        /// <summary>
        /// 選択情報を設定する
        /// </summary>
        /// <param name="target"></param>
        public bool SetSelection(Selectable target) {

            if (target != null && Selectables.Contains(target)) {
                Current = target;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 選択情報をリセットする
        /// </summary>
        public void ResetSelection() {
                Current = First;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 監視用コンポーネントをアタッチする
        /// </summary>
        private void AttachComponent() {
            foreach (var selectable in _selectables) {
                // コンポーネント付与
                var hooker = selectable.gameObject.GetOrAddComponent<SelectableGroup.SelectionHooker>();
                hooker.Register(this);

                // クリア時のコールバック
                OnListRelesed += hooker.Unegister;
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method (※監視コンポーネントから呼び出すメソッド)

        /// <summary>
        /// UIイベントを外部へ通知する
        /// </summary>
        private void NotifyPointerEnter(GameObject selected) => OnPointerEnter.Invoke(selected);

        /// <summary>
        /// UIイベントを外部へ通知する
        /// </summary>
        private void NotifyPointerExit(GameObject selected) => OnPointerExit.Invoke(selected);

        /// <summary>
        /// UIイベントを外部へ通知する
        /// </summary>
        private void NotifySelected(GameObject selected) => OnSelect.Invoke(selected);

    }



}