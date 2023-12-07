using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Toolkit;


// [�Q�l]
//  Hatena: �p�[�e�B�N���ė��p�N���X https://www.stmn.tech/entry/2016/03/01/004816 
//  qiita: UniRx��ObjectPool���g����ParticleSystem���Ǘ����� https://qiita.com/KeichiMizutani/items/fc22a6037447d840adc2
//  kan�̃�����: UniRx�ŃI�u�W�F�N�g�v�[��(ObjectPool)���ȒP���� https://kan-kikuchi.hatenablog.com/entry/UniRx_ObjectPool


namespace nitou.Particle {

    /// <summary>
    /// �p�[�e�B�N���̃I�u�W�F�N�g�v�[��
    /// </summary>
    public class ParticlePool : ObjectPool<ParticleObject> {

        private ParticleObject _prefab;         // �v���n�u
        private Transform _parentTransform;     // ���������I�u�W�F�N�g�̐e

        /// <summary>
        /// �p�[�e�B�N����
        /// </summary>
        public string ParticleName => _particleName;
        private readonly string _particleName;


        /// ----------------------------------------------------------------------------
        // Override Method 
        
        /// <summary>
        /// �C���X�^���X�𐶐�����
        /// </summary>
        protected override ParticleObject CreateInstance() {
            if (_prefab == null) SetDefalutPrefab();
            return Object.Instantiate(_prefab, _parentTransform, true);
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// �R���X�g���N�^
        public ParticlePool(Transform transform, ParticleObject origin, string particleName = "") {
            this._parentTransform = transform;
            this._prefab = origin;
            this._particleName = particleName;
        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// �f�t�H���g�̃p�[�e�B�N����o�^
        /// </summary>
        private void SetDefalutPrefab() {
            var obj = new GameObject($"Defalut Particle");
            _prefab = obj.AddComponent<ParticleObject>();
        }

    }

}