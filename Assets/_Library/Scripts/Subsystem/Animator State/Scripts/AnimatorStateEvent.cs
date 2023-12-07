using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


// [�Q�l]
//  Hatena: AnimatorController�̌��݂̃X�e�[�g�����擾������X�e�[�g�̐؂�ւ����Ď������肷��d�g�݂���� https://light11.hatenadiary.com/search?q=StateMachineBehaviour
//  qiita: UniRx�ŃA�j���[�V�����̊J�n�ƏI�����X�N���v�g�Ŏ󂯎�� https://qiita.com/nodokamome/items/a5a3610d7adef81d6b77
//  kan�̃�����: Animator�̃X�e�[�g(���)�̕ύX�����m���� StateMachineBehaviour https://kan-kikuchi.hatenablog.com/entry/StateMachineBehaviour


namespace nitou {

    /// <summary>
    /// �S�X�e�[�g���⌻�݂̃X�e�[�g����񋟂���StateMachineBehaviour
    /// (���G�f�B�^�g���Ŏ����I��Animator��BaseLayer�ɃA�^�b�`�����)
    /// </summary>
    public class AnimatorStateEvent : ObservableStateMachineTrigger {

        /// <summary>
        /// �Ώۂ̃��C���[
        /// </summary>
        public int Layer => _layer;
        [SerializeField] private int _layer;

        
        /// <summary>
        /// �S�X�e�[�g���(��AnimatorEditorUtility�ɂ���Ď����ŃZ�b�g�A�b�v)
        /// </summary>
        public string[] StateFullPaths => _stateFullPaths;
        [SerializeField] private string[] _stateFullPaths;

        /// <summary>
        /// ���݂̃X�e�[�g��
        /// </summary>
        public string CurrentStateName { get; private set; }

        /// <summary>
        /// ���݂̃T�u�X�e�[�g��
        /// </summary>
        public string CurrentSubStateName {
            get {
                var split = CurrentStateFullPath.Split('.');
                return (split.Length >= 3) ? split[1] : "";     // ��2�K�w�ڂ��T�u�X�e�[�g�Ƃ���
            }
        }

        /// <summary>
        /// ���݂̃X�e�[�g�̃t���p�X
        /// </summary>
        public string CurrentStateFullPath { get; private set; }

        /// <summary>
        /// �X�e�[�g���ɑΉ������n�b�V���l
        /// </summary>
        private int[] StateFullPathHashes {
            get {
                if (_stateFullPathHashes == null) {
                    _stateFullPathHashes = _stateFullPaths
                        .Select(x => Animator.StringToHash(x))
                        .ToArray();
                }
                return _stateFullPathHashes;
            }
        }
        private int[] _stateFullPathHashes;

        
        /// <summary>
        /// �X�e�[�g���ς�������̃R�[���o�b�N
        /// </summary>
        public event System.Action<(string stateName, string stateFullPath)> stateEntered;



        /// ----------------------------------------------------------------------------
        // Overide Method 

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

            // ���݂̃X�e�[�g�����擾
            bool isSuccece = false;
            for (int i = 0; i < StateFullPathHashes.Length; i++) {
                var stateFullPathHash = StateFullPathHashes[i];

                // �X�e�[�g������������
                if (stateInfo.fullPathHash == stateFullPathHash) {

                    // �X�e�[�g��
                    CurrentStateFullPath = _stateFullPaths[i];
                    CurrentStateName = CurrentStateFullPath.Split('.').Last();

                    // �R�[���o�b�N
                    stateEntered?.Invoke((CurrentStateName, CurrentStateFullPath));


                    isSuccece = true;
                    break;
                }
            }

            // �}�b�`����X�e�[�g��������΃G���[����
            if (!isSuccece) throw new System.Exception();

            // �x�[�X�̃C�x���g���� (��CurrentState���X�V���Ă�����s)
            base.OnStateEnter(animator, stateInfo, layerIndex);         // �������͎g���₷���X�g���[�����`����ق����ǂ�����
        }



        /// ----------------------------------------------------------------------------
        // Static Method 

        /// <summary>
        /// Animator����C���X�^���X���擾����
        /// </summary>
        public static AnimatorStateEvent Get(Animator animator, int layer) {
            return animator.GetBehaviours<AnimatorStateEvent>().First(x => x.Layer == layer);
        }


    }


}