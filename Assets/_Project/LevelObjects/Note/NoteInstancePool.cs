using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

namespace OtoGame.LevelObjects {

    /// <summary>
    /// NoteInstanceのオブジェクトプール
    /// </summary>
    public class NoteInstancePool : ObjectPool<NoteInstance> {

        private readonly NoteInstance _prefab;
        private readonly Transform _parentTrans;
        private readonly Vector3 _defaultPos;


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NoteInstancePool(NoteInstance prefab, Transform parentTrans, Vector3 defaultPos) {
            if (prefab == null) throw new System.ArgumentNullException(nameof(prefab));

            _prefab = prefab;
            _parentTrans = parentTrans;
            _defaultPos = defaultPos;
        }


        /// ----------------------------------------------------------------------------
        // Override Method 

        /// <summary>
        /// インスタンス生成
        /// </summary>
        protected override NoteInstance CreateInstance() {
            var note = NoteInstance.Instantiate(_prefab, _parentTrans);
            note.Initialize();
            return note;
        }

        /// <summary>
        /// 貸し出し前の処理
        /// </summary>
        protected override void OnBeforeRent(NoteInstance instance) {
            
            instance.transform.position = _defaultPos;
            instance.Show();
            instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// 返却前の処理
        /// </summary>
        protected override void OnBeforeReturn(NoteInstance instance) {
            instance.Hide();
            instance.gameObject.SetActive(false);
        }
    }

}