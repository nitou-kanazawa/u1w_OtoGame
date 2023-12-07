using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

// [�Q�l]
//  Hatena: �p�[�e�B�N���ė��p�N���X https://www.stmn.tech/entry/2016/03/01/004816 
//  qiita: UniRx��ObjectPool���g����ParticleSystem���Ǘ����� https://qiita.com/KeichiMizutani/items/fc22a6037447d840adc2
//  kan�̃�����: UniRx�ŃI�u�W�F�N�g�v�[��(ObjectPool)���ȒP���� https://kan-kikuchi.hatenablog.com/entry/UniRx_ObjectPool

namespace nitou.Particle {
using nitou.DesiginPattern;

    /// <summary>
    /// �p�[�e�B�N�����Ǘ�����O���[�o���}�l�[�W��
    /// �i�������ȃv���W�F�N�g�܂��̓v���g�^�C�v�Ƃ��Ă̎g�p��z��j
    /// </summary>
    public class ParticleManager : MonoBehaviour {
    //public class ParticleManager : SingletonMonoBehaviour<ParticleManager> {

        // �e�p�[�e�B�N���v�[���̃��X�g
        private List<ParticlePool> _poolList = new();
        private List<GameObject> _containerObjects = new();

        // ���\�[�X���
        private const string RESOUCE_PATH = "Particles/";

        /// <summary>
        /// �C�x���g�`�����l��
        /// </summary>
        [SerializeField] private ParticleEventChannelSO _eventChannel = null;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void Start() {
            _eventChannel.OnEventRaised += PlayParticleInternal;            
        }

        private void OnDestroy() {
            _eventChannel.OnEventRaised -= PlayParticleInternal;

            // �j�����ꂽ�Ƃ��iDispose���ꂽ�Ƃ��j��ObjectPool���������
            ClearList();
            _poolList = null;
            _containerObjects = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (���)

        /// <summary>
        /// �I�u�W�F�N�g�v�[���̃��X�g���
        /// </summary>
        public void ClearList() {

            // �v�[���̉��
            _poolList.ForEach(p => p.Dispose());
            _poolList.Clear();

            // �e�I�u�W�F�N�g�̉��
            _containerObjects.ForEach(o => o.Destroy());
            _containerObjects.Clear();
        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ� (�������Ăяo���p)
        /// </summary>
        private void PlayParticleInternal(ParticleRequestData data) {

            // �v�[���̎擾
            ParticlePool pool = _poolList.Where(p => p.ParticleName == data.particleName).FirstOrDefault();

            // �v�[�����������̏ꍇ�C
            if (pool == null) {

                // �i�[�p�̐e�I�u�W�F�N�g�𐶐� (���f�o�b�O�����p)
                var parentObj = new GameObject($"Pool [{data.particleName}]");
                parentObj.SetParent(this.transform);
                _containerObjects.Add(parentObj);

                // �������̃I�u�W�F�N�g���擾
                var prefab = LoadOrCreateOrigin(data.particleName).GetOrAddComponent<ParticleObject>();

                // �v�[���̐���
                pool = new ParticlePool(parentObj.transform, prefab, data.particleName);
                _poolList.Add(pool);
            }

            // �I�u�W�F�N�g�̎擾
            var effect = pool.Rent();


            // �G�t�F�N�g�̍Đ�
            var diposable = data.withEvent ?
                effect.PlayParticleWithEvent(data.position, data.rotation) :  // ���C�x���g��������
                effect.PlayParticle(data.position, data.rotation);            // ���C�x���g�����Ȃ�

            // ���Đ��I����pool�ɕԋp
            diposable.Subscribe(_ => { pool.Return(effect); });     // ���Đ��I����pool�ɕԋp
        }

        /// <summary>
        /// �������̃I�u�W�F�N�g�����[�h
        /// �����[�h���s���̓f�t�H���g�̃p�[�e�B�N���𐶐�
        /// </summary>
        private GameObject LoadOrCreateOrigin(string particleName) {
            // ���\�[�X�̓ǂݍ���
            var origin = Resources.Load(RESOUCE_PATH + particleName) as GameObject;

            if (origin == null) {   // ----- ���s�����ꍇ�C
                // ���_�~�[�����Ă���
                origin = new GameObject($"Defalut Particle");
                Debug.Log($"[{particleName}]�Ƃ����p�[�e�B�N���̓ǂݍ��݂Ɏ��s���܂���");

            } else {                // ----- ���������ꍇ�C
                origin.name = particleName;
            }
            return origin;
        }

    }
}