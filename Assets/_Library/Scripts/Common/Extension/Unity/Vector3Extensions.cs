using UnityEngine;
using UnityEngine.UI;

// [�Q�l]
//  PG���� : Vector3(�\����)�Ɏ������g�̒l��ύX����g�����\�b�h���`���� https://takap-tech.com/entry/2022/12/24/175039

namespace nitou {

    /// <summary>
    /// Vector3�̊g�����\�b�h�N���X
    /// </summary>
    public static partial class Vector3Extensions {

        /// ----------------------------------------------------------------------------
        // �l�̐ݒ�

        /// <summary>
        /// X�l��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetX(ref this Vector3 @this, float x) => @this.x = x;

        /// <summary>
        /// Y�l��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetY(ref this Vector3 @this, float y) => @this.y = y;

        /// <summary>
        /// Z�l��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetZ(ref this Vector3 @this, float z) => @this.z = z;


        /// ----------------------------------------------------------------------------
        // �l�̉��Z

        /// <summary>
        /// X�l�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddX(ref this Vector3 @this, float x) => @this.x += x;

        /// <summary>
        /// Y�l�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddY(ref this Vector3 @this, float y) => @this.y += y;

        /// <summary>
        /// Z�l�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddZ(ref this Vector3 @this, float z) => @this.z += z;
    }

}