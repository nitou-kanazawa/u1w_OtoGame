using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

namespace nitou.Subsystem {

    /// <summary>
    /// �͈͓��̊Ď��ΏۃI�u�W�F�N�g�ɃA�^�b�`����R���|�[�l���g
    /// �i��OnTriggerExit�͔͈͓��Ŕ�A�N�e�B�u�ɂȂ����I�u�W�F�N�g�ɑ΂��ČĂ΂�Ȃ����߁C���̊ȈՑ΍�j
    /// </summary>
    internal class SignalSender : MonoBehaviour{
        // �C�x���g
        public event System.Action<GameObject> OnDisableEvent = delegate { };
        
        // �C�x���g����
        private void OnDisable() => OnDisableEvent.Invoke(gameObject);
    }


    /// <summary>
    /// �͈͓��̑ΏۃI�u�W�F�N�g�����m����R���|�[�l���g
    /// </summary>
    public abstract class SearchRange<T> : MonoBehaviour {

        #region Inner Class

        /// <summary>
        /// �Ώۂ��������߂̃N���X
        /// </summary>
        protected class TargetInfo {
            
            /// <summary>
            /// �Ώی^�i���C���^�[�t�F�[�X���z�肳���j
            /// </summary>
            public T target;
            
            /// <summary>
            /// �Ώی^�����Q�[���I�u�W�F�N�g
            /// </summary>
            public GameObject gameObject;

            /// <summary>
            /// �Ď��p�R���|�[�l���g
            /// </summary>
            internal SignalSender sender;

            /// ----------------------------------------------------------------------------

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            internal TargetInfo(T target, GameObject obj, SignalSender sender) {
                this.target = target;
                this.gameObject = obj;
                this.sender = sender;
            }
        }
        #endregion


        [Title("�T���͈�")]

        /// <summary>
        /// �T���͈̓R���C�_�[
        /// </summary>
        [ShowInInspector, ReadOnly]
        public SphereCollider Collider => _collidr ?? (_collidr = GetComponentInChildren<SphereCollider>());
        protected SphereCollider _collidr;

        /// <summary>
        /// �T���͈͂̔��a
        /// </summary>
        [ShowInInspector, ReadOnly]
        public float Radius {
            get => Collider.radius;
            set => Collider.radius = Mathf.Max(value, 0.01f);   // ���K���ȉ����l
        }

        [Title("�ΏۃI�u�W�F�N�g")]

        /// <summary>
        /// �͈͓��Ƀ^�[�Q�b�g�����݂��邩�ǂ���
        /// </summary>
        [ShowInInspector, ReadOnly]
        public bool ExistTarget => _targetList.Count > 0;

        /// <summary>
        /// �͈͓��̃^�[�Q�b�g
        /// </summary>
        [ShowInInspector, ReadOnly]
        public IEnumerable<T> Targets => _targetList.Select(t=>t.target).ToList();

        /// <summary>
        /// ���X�g�̍ŏ��̃^�[�Q�b�g
        /// </summary>
        public T FirstTarget => ExistTarget ? _targetList[0].target : default;



        // �^�[�Q�b�g�����i�[�������X�g
        protected List<TargetInfo> _targetList = new();
        // 
        CompositeDisposable _disposables = new();


        /// ----------------------------------------------------------------------------
        // �C�x���g

        /// <summary>
        /// �^�[�Q�b�g���X�V���ꂽ�Ƃ��̃C�x���g�ʒm
        /// �i�������̓��X�g�Ƀ^�[�Q�b�g�����݂��邩�ǂ����j
        /// </summary>
        public System.IObservable<bool> OnTargetListUpdated => _onTargetListUpdated;
        protected Subject<bool> _onTargetListUpdated = new();

        /// <summary>
        /// �^�[�Q�b�g���ǉ����ꂽ���̃C�x���g�ʒm
        /// </summary>
        public event System.Action<T> OnAddTarget = delegate { };

        /// <summary>
        /// �^�[�Q�b�g���폜���ꂽ���̃C�x���g�ʒm
        /// </summary>
        public event System.Action<T> OnRemoveTarget = delegate { };


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 

        private void OnEnable() {
            // �R���C�_�[��������΁C��A�N�e�B�u������
            if (Collider == null) this.enabled = false;

            Collider.isTrigger = true;
            Collider.enabled = true;
        }

        private void OnDisable() {
            Collider.enabled = false;
        }

        private void OnDestroy() {
            // ���X�g�̃N���A
            RemoveAllTarget();

            // �������
            _onTargetListUpdated.Dispose();
            _disposables.Dispose();
        }

        private void OnTriggerEnter(Collider other) => AddTarget(other.gameObject);

        private void OnTriggerExit(Collider other) => RemoveTarget(other.gameObject);


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �^�[�Q�b�g�����X�g�֒ǉ�����
        /// </summary>
        protected void AddTarget(GameObject targetObj) {

            if (targetObj.TryGetComponent<T>(out var target)) {

                // ���Ƀ��X�g�o�^����Ă���ꍇ�C�������Ȃ�
                var info = _targetList.Find(t => t.gameObject == targetObj);
                if (info != null) { return; }

                // ���X�g�֒ǉ�
                var sender = GetOrAttachSignalSender(targetObj);  // �Ď��p�R���|�[�l���g
                _targetList.Add(new TargetInfo(target, targetObj, sender));

                // �h���N���X�̏���
                OnUpdateTarget();

                // �C�x���g�ʒm
                OnAddTarget.Invoke(target);
                _onTargetListUpdated.OnNext(ExistTarget);
            }
        }

        /// <summary>
        /// �^�[�Q�b�g�����X�g���珜�O����
        /// </summary>
        protected void RemoveTarget(GameObject targetObj) {

            if (targetObj.TryGetComponent<T>(out var target)) {

                // ���Ƀ��X�g�o�^��������Ă���ꍇ�C�������Ȃ�
                var info = _targetList.Find(t => t.gameObject == targetObj);
                if (info == null) { return; }

                // ���X�g����폜
                targetObj.RemoveComponent<SignalSender>();
                _targetList.Remove(info);

                // �h���N���X�̏���
                OnUpdateTarget();

                // �C�x���g�ʒm
                OnRemoveTarget.Invoke(target);
                _onTargetListUpdated.OnNext(ExistTarget);
            }
        }

        /// <summary>
        /// �^�[�Q�b�g�����X�g���珜�O����
        /// </summary>
        public bool RemoveTarget(T target) {

            // ���X�g�ɓo�^����Ă��Ȃ��ꍇ�C�I��
            var info = FindInfo(target);
            if (info == null) {
                return false;
            }

            // ���X�g����폜
            RemoveTarget(info.gameObject);

            return true;
        }

        /// <summary>
        /// �S�Ẵ^�[�Q�b�g�����X�g���珜�O����
        /// </summary>
        protected void RemoveAllTarget() {
            // �Ď��p�I�u�W�F�N�g�̔j��
            _targetList.ForEach(t => t.sender.Destroy());

            // ���X�g�̉��
            _targetList.Clear();
            _onTargetListUpdated.OnNext(false);
        }

        /// <summary>
        /// T�^�̃^�[�Q�b�g���w�肵�đΉ����郊�X�g�����擾����
        /// �i��T�^��==��r���ł��Ȃ��̂ŁC�h���N���X�Ŏ�������j
        /// </summary>
        protected abstract TargetInfo FindInfo(T target);


        /// ----------------------------------------------------------------------------
        // Private Method (�Ď��p�R���|�[�l���g)

        /// <summary>
        /// �Ď��p�R���|�[�l���g���A�^�b�`����
        /// </summary>
        private SignalSender GetOrAttachSignalSender(GameObject targetObj) {

            // �A�^�b�`�ς݂̏ꍇ�́C���̂܂ܕԂ�
            if (targetObj.HasComponent<SignalSender>()) {
                return targetObj.GetComponent<SignalSender>();
            }
            // ��A�^�b�`��ԂȂ�C�Z�b�g�A�b�v���s��
            else {
                var sender = targetObj.AddComponent<SignalSender>();
                sender.OnDisableEvent += RemoveTarget;
                return sender;
            }
        }


        /// ----------------------------------------------------------------------------
        // Protected Method 

        /// <summary>
        /// �^�[�Q�b�g���X�V���ꂽ���̏���
        /// </summary>
        protected virtual void OnUpdateTarget() { }

        /// <summary>
        /// �h���N���X�ł̃C�x���g����
        /// </summary>
        protected void RaiseAddTargetEvent(T target) => OnAddTarget.Invoke(target);

        /// <summary>
        /// �h���N���X�ł̃C�x���g����
        /// </summary>
        protected void RaiseRemoveTargetEvent(T target) => OnRemoveTarget.Invoke(target);


        /// ----------------------------------------------------------------------------
        // Debug

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            if (Collider == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Collider.transform.position, Collider.radius);
        }
#endif


        /// ----------------------------------------------------------------------------
        // Public Method 

        ///// <summary>
        ///// �ł��߂��^�[�Q�b�g��Ԃ� 
        ///// </summary>
        //public T GetNearestTarget() {
        //    if (!ExistTarget) return default(T);

        //    var pos = transform.position;
        //    return _targets
        //        .OrderBy(t => (t.Transform.position - pos).sqrMagnitude)
        //        .FirstOrDefault();
        //}

        ///// <summary>
        ///// ��ʂɉf���Ă���ł��߂��^�[�Q�b�g��Ԃ� 
        ///// </summary>
        //public IAimable GetNearestVisbleTarget() {
        //    if (!ExistTarget) return null;

        //    var pos = transform.position;
        //    return _targets
        //        .Where(t=>t.IsVisible)
        //        .OrderBy(t => (t.Transform.position - pos).sqrMagnitude)
        //        .FirstOrDefault();
        //}


    }

}