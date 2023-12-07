using System.Linq;
using UniRx;
using UnityEngine;

// [�Q�l]
//  �R�K�l�u���O: GetComponentsInChildren�Ŏ������g���܂܂Ȃ��悤�ɂ���g�����\�b�h https://baba-s.hatenablog.com/entry/2014/06/05/220224
//  qiita: ������Ƃ����֗��ɂȂ邩������Ȃ��g�����\�b�h�W https://qiita.com/tanikura/items/ed5d56ebbfcad19c488d

namespace nitou {

    /// <summary>
    /// GameObject�̊g�����\�b�h�N���X
    /// </summary>
    public static partial class GameObjectExtensions {

        /// ----------------------------------------------------------------------------
        // �R���|�[�l���g�i�m�F�j

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g���A�^�b�`����Ă��邩�ǂ������m�F����g�����\�b�h
        /// </summary>
        public static bool HasComponent<T>(this GameObject @this) 
            where T : Component {
            return @this.GetComponent<T>();
        }

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g���A�^�b�`����Ă��邩�ǂ������m�F����g�����\�b�h
        /// </summary>
        public static bool HasComponent(this GameObject @this, System.Type type) {
            return @this.GetComponent(type);
        }


        /// ----------------------------------------------------------------------------
        // �R���|�[�l���g�i�폜�j

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g���폜����g�����\�b�h
        /// </summary>
        public static GameObject RemoveComponent<T>(this GameObject @this)
            where T : Component {
            T component = @this.GetComponent<T>();
            if (component != null) Object.Destroy(component);
            return @this;
        }

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g���폜����g�����\�b�h
        /// </summary>
        public static GameObject RemoveComponent<T1, T2>(this GameObject @this)
            where T1 : Component where T2 : Component {
            @this.RemoveComponent<T1>();
            @this.RemoveComponent<T2>();
            return @this;
        }

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g���폜����g�����\�b�h
        /// </summary>
        public static GameObject RemoveComponent<T1, T2, T3>(this GameObject @this)
            where T1 : Component where T2 : Component where T3 : Component {
            @this.RemoveComponent<T1, T2>();
            @this.RemoveComponent<T3>();
            return @this;
        }

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g���폜����g�����\�b�h
        /// </summary>
        public static GameObject RemoveComponent<T1, T2, T3, T4>(this GameObject @this)
            where T1 : Component where T2 : Component where T3 : Component where T4 : Component {
            @this.RemoveComponent<T1, T2, T3>();
            @this.RemoveComponent<T4>();
            return @this;
        }


        /// ----------------------------------------------------------------------------
        // �R���|�[�l���g�i�ǉ��j

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g��ǉ�����g�����\�b�h
        /// </summary>
        public static GameObject AddComponents<T1, T2>(this GameObject @this) 
            where T1 : Component where T2 : Component {
            @this.AddComponent<T1>();
            @this.AddComponent<T2>();
            return @this;
        }


        /// ----------------------------------------------------------------------------
        // �R���|�[�l���g�i�擾�j

        /// <summary>
        /// �������g���܂܂Ȃ�GetComponentsInChaidren�̊g�����\�b�h
        /// </summary>
        public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject @this) 
            where T : Component {
            return @this.GetComponentsInChildren<T>().Where(c => @this != c.gameObject).ToArray();
        }

        /// <summary>
        /// �Ώۂ̃R���|�[�l���g���ꍇ�͂�����擾���C�Ȃ���Βǉ����ĕԂ��g�����\�b�h
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject @this) 
            where T : Component {
            var component = @this.GetComponent<T>();
            return component ?? @this.AddComponent<T>();
        }


        /// ----------------------------------------------------------------------------
        // �R���|�[�l���g�i�L����ԁj

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g��L��������g�����\�b�h
        /// </summary>
        public static GameObject EnableComponent<T>(this GameObject @this) 
            where T : Behaviour {
            if (@this.HasComponent<T>()) {
                @this.GetComponent<T>().enabled = true;
            }
            return @this;
        }

        /// <summary>
        /// �w�肳�ꂽ�R���|�[�l���g���L��������g�����\�b�h
        /// </summary>
        public static GameObject DisableComponent<T>(this GameObject @this
            ) 
            where T : Behaviour {
            if (@this.HasComponent<T>()) {
                @this.GetComponent<T>().enabled = false;
            }
            return @this;
        }


        /// ----------------------------------------------------------------------------
        // ����

        /// <summary>
        /// �Ώۂ�GameObject�𕡐�(����)���ĕԂ��g�����\�b�h
        /// </summary>
        public static GameObject Instantiate(this GameObject @this) {
            return Object.Instantiate(@this);
        }

        /// <summary>
        /// ������ɐe�ƂȂ�Transform���w�肵�āA�Ώۂ�GameObject�𕡐�(����)���ĕԂ��g�����\�b�h
        /// </summary>
        public static GameObject Instantiate(this GameObject @this, Transform parent) {
            return Object.Instantiate(@this, parent);
        }

        /// <summary>
        /// ������̍��W�y�юp�����w�肵�āA�Ώۂ�GameObject�𕡐�(����)���ĕԂ��g�����\�b�h
        /// </summary>
        public static GameObject Instantiate(this GameObject @this, Vector3 pos, Quaternion rot) {
            return Object.Instantiate(@this, pos, rot);
        }

        /// <summary>
        /// ������ɐe�ƂȂ�Transform�A�܂�������̍��W�y�юp�����w�肵�āA�Ώۂ�GameObject�𕡐�(����)���ĕԂ��g�����\�b�h
        /// </summary>
        public static GameObject Instantiate(this GameObject @this, Vector3 pos, Quaternion rot, Transform parent) {
            return Object.Instantiate(@this, pos, rot, parent);
        }

        /// <summary>
        /// ������ɐe�ƂȂ�Transform�A�܂�������̃��[�J�����W���w�肵�āA�Ώۂ�GameObject�𕡐�(����)���ĕԂ��g�����\�b�h
        /// </summary>
        public static GameObject InstantiateWithLocalPosition(this GameObject @this, Transform parent, Vector3 localPos) {
            var instance = Object.Instantiate(@this, parent);
            instance.transform.localPosition = localPos;
            return instance;
        }


        /// ----------------------------------------------------------------------------
        // �A�N�e�B�u���

        /// <summary>
        /// �A�N�e�B�u��Ԃ̐؂�ւ��ݒ���s���g�����\�b�h
        /// </summary>
        public static System.IDisposable SetActiveSelfSource(this GameObject @this, System.IObservable<bool> source, bool invert = false) {
            return source
                .Subscribe(x => {
                    x = invert ? !x : x;
                    @this.SetActive(x);
                })
                .AddTo(@this);
        }


        /// ----------------------------------------------------------------------------
        // �j��

        /// <summary>
        /// Destroy�̊g�����\�b�h
        /// </summary>
        public static void Destroy(this GameObject @this) {
            Object.Destroy(@this);
        }

        /// <summary>
        /// DontDestroyOnLoad�̊g�����\�b�h
        /// </summary>
        public static void DontDestroyOnLoad(this GameObject @this) {
            Object.DontDestroyOnLoad(@this);
        }



        /// ----------------------------------------------------------------------------
        // ���C���[

        /// <summary>
        /// �Ώۂ̃��C���[�Ɋ܂܂�Ă��邩�𒲂ׂ�g�����\�b�h
        /// </summary>
        public static bool IsInLayerMask(this GameObject @this, LayerMask layerMask) {
            int objLayerMask = (1 << @this.layer);
            return (layerMask.value & objLayerMask) > 0;
        }

    }




}