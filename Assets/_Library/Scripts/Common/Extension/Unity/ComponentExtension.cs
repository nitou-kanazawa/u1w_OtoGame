using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [�Q�l]
//  qiita: Unity�Ŏg����֗��֐�(�g�����\�b�h)�B https://qiita.com/nmss208/items/9846525cf523fb961b48

namespace nitou {

    /// <summary>
    /// Component�̊g�����\�b�h�N���X
    /// </summary>
    public static partial class ComponentExtensions {

        /// ----------------------------------------------------------------------------
        // ����

        /// <summary>
        /// GameObject���Ώۂ̃R���|�[�l���g���ꍇ�͂�����擾���C�Ȃ���Βǉ����ĕԂ��g�����\�b�h
        /// </summary>
        public static T GetOrAddComponent<T>(this Component @this) where T : Component {
            return GameObjectExtensions.GetOrAddComponent<T>(@this.gameObject);
        }

        /// ----------------------------------------------------------------------------
        // �j��

        public static void Destroy(this Component @this) {
            Object.Destroy(@this);
        }

        /// <summary>
        /// Component���A�^�b�`����Ă���GameObject��j������
        /// </summary>
        public static void DestroyGameObject(this Component @this) {
            Object.Destroy(@this.gameObject);
        }


        /// ----------------------------------------------------------------------------
        // ���C���[

        /// <summary>
        /// GameObject���Ώۂ̃��C���[�Ɋ܂܂�Ă��邩�𒲂ׂ�g�����\�b�h
        /// </summary>
        public static bool IsInLayerMask(this Component component, LayerMask layerMask) {
            return GameObjectExtensions.IsInLayerMask(component.gameObject, layerMask);
        }
    }

}