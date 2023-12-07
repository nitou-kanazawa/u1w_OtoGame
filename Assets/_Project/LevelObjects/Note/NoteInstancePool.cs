using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

namespace OtoGame.LevelObjects {

    /// <summary>
    /// NoteInstance�̃I�u�W�F�N�g�v�[��
    /// </summary>
    public class NoteInstancePool : ObjectPool<NoteInstance> {

        private readonly NoteInstance _prefab;
        private readonly Transform _parentTrans;
        private readonly Vector3 _defaultPos;


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �R���X�g���N�^
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
        /// �C���X�^���X����
        /// </summary>
        protected override NoteInstance CreateInstance() {
            var note = NoteInstance.Instantiate(_prefab, _parentTrans);
            note.Initialize();
            return note;
        }

        /// <summary>
        /// �݂��o���O�̏���
        /// </summary>
        protected override void OnBeforeRent(NoteInstance instance) {
            
            instance.transform.position = _defaultPos;
            instance.Show();
            instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// �ԋp�O�̏���
        /// </summary>
        protected override void OnBeforeReturn(NoteInstance instance) {
            instance.Hide();
            instance.gameObject.SetActive(false);
        }
    }

}