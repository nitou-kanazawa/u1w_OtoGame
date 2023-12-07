using UnityEngine;

// [�Q�l]
//  qiita: �V���O���g�����g���Ă݂悤�@https://qiita.com/Teach/items/c146c7939db7acbd7eee
//  kan�̃�����: MonoBehaviour���p�������V���O���g�� https://kan-kikuchi.hatenablog.com/entry/SingletonMonoBehaviour

namespace nitou.DesiginPattern {

    /// <summary>
    /// MonoBehaviour���p�������V���O���g��
    /// ��DontDestroyOnLoad�͌Ă΂Ȃ��i�h���N���X���ōs���j
    /// </summary>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

        // 
        private static T instance;
        public static T Instance {
            get {
                if (instance == null) {
                    // �V�[��������C���X�^���X���擾
                    instance = (T)FindObjectOfType(typeof(T));      // ��Find�ŎQ�Ƃ��Ă��邽��,Awake�̃^�C�~���O�ɂ��Q�Ɖ\!

                    // �V�[�����ɑ��݂��Ȃ��ꍇ�̓G���[
                    if (instance == null) {
                        Debug.LogError(typeof(T) + " ���A�^�b�`���Ă���GameObject�͂���܂���");
                    }
                }
                return instance;
            }
        }

        // 
        protected virtual void Awake() {
            // ���ɃC���X�^���X�����݂���Ȃ�j��
            if (!CheckInstance())
                Destroy(this.gameObject);
        }

        /// <summary>
        /// ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        /// �A�^�b�`����Ă���ꍇ�͔j������B
        /// </summary>
        protected bool CheckInstance() {
            // ���݂��Ȃ��ior�������g�j�ꍇ
            if (instance == null) {
                instance = this as T;
                return true;
            } else if (Instance == this) {
                return true;
            }
            // ���ɑ��݂���ꍇ
            return false;
        }
    }

}