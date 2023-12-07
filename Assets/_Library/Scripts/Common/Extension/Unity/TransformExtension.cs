using UnityEngine;


// [�Q�l]
//  �R�K�l�u���O: Transform�^�̈ʒu���]�p�A�T�C�Y�̐ݒ���y�ɂ��� https://baba-s.hatenablog.com/entry/2014/02/28/000000
//  _:  Transform�Ƀ��Z�b�g������ǉ����Ă݂� https://ookumaneko.wordpress.com/2015/10/01/unity%E3%83%A1%E3%83%A2-transform%E3%81%AB%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E5%87%A6%E7%90%86%E3%82%92%E8%BF%BD%E5%8A%A0%E3%81%97%E3%81%A6%E3%81%BF%E3%82%8B/#:~:text=%E3%83%AF%E3%83%BC%E3%83%AB%E3%83%89%E3%82%92%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E3%81%97%E3%81%9F%E3%81%84%E6%99%82,%E5%80%A4%E3%82%92%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E5%87%BA%E6%9D%A5%E3%81%BE%E3%81%99%E3%80%82

namespace nitou {

    /// <summary>
    /// GameObject�̊g�����\�b�h�N���X
    /// </summary>
    public static partial class TransformExtension {

        /// ----------------------------------------------------------------------------
        // �ʒu�̐ݒ�

        /// <summary>
        /// X���W��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetPositionX(this Transform @this, float x) =>
            @this.position = new Vector3(x, @this.position.y, @this.position.z);

        /// <summary>
        /// Y���W��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetPositionY(this Transform @this, float y) =>
            @this.position = new Vector3(@this.position.x, y, @this.position.z);

        /// <summary>
        /// Z���W��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetPositionZ(this Transform @this, float z) =>
            @this.position = new Vector3(@this.position.x, @this.position.y, z);

        /// <summary>
        /// X���W�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddPositionX(this Transform @this, float x) =>
            @this.SetPositionX(x + @this.position.x);

        /// <summary>
        /// Y���W�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddPositionY(this Transform @this, float y) =>
            @this.SetPositionY(y + @this.position.y);

        /// <summary>
        /// Z���W�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddPositionZ(this Transform @this, float z) =>
            @this.SetPositionZ(z + @this.position.z);

        /// <summary>
        /// ���[�J����X���W��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetLocalPositionX(this Transform @this, float x) =>
            @this.localPosition = new Vector3(x, @this.localPosition.y, @this.localPosition.z);

        /// <summary>
        /// ���[�J����Y���W��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetLocalPositionY(this Transform @this, float y) =>
            @this.localPosition = new Vector3(@this.localPosition.x, y, @this.localPosition.z);

        /// <summary>
        /// ���[�J����Z���W��ݒ肷��g�����\�b�h
        /// </summary>
        public static void SetLocalPositionZ(this Transform @this, float z) =>
            @this.localPosition = new Vector3(@this.localPosition.x, @this.localPosition.y, z);

        /// <summary>
        /// ���[�J����X���W�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddLocalPositionX(this Transform @this, float x) =>
            @this.SetLocalPositionX(x + @this.localPosition.x);

        /// <summary>
        /// ���[�J����Y���W�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddLocalPositionY(this Transform @this, float y) =>
            @this.SetLocalPositionY(y + @this.localPosition.y);

        /// <summary>
        /// ���[�J����Z���W�ɉ��Z����g�����\�b�h
        /// </summary>
        public static void AddLocalPositionZ(this Transform @this, float z) =>
            @this.SetLocalPositionZ(z + @this.localPosition.z);


        /// ----------------------------------------------------------------------------
        // �p�x�̐ݒ�

        /// <summary>
        /// X�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetEulerAngleX(this Transform @this, float x) =>
            @this.eulerAngles = new Vector3(x, @this.eulerAngles.y, @this.eulerAngles.z);

        /// <summary>
        /// Y�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetEulerAngleY(this Transform @this, float y) =>
            @this.eulerAngles = new Vector3(@this.eulerAngles.x, y, @this.eulerAngles.z);

        /// <summary>
        /// Z�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetEulerAngleZ(this Transform @this, float z) =>
            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, z);

        /// <summary>
        /// X�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddEulerAngleX(this Transform @this, float x) =>
            @this.SetEulerAngleX(@this.eulerAngles.x + x);

        /// <summary>
        /// Y�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddEulerAngleY(this Transform @this, float y) =>
            @this.SetEulerAngleY(@this.eulerAngles.y + y);

        /// <summary>
        /// Z�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddEulerAngleZ(this Transform @this, float z) =>
            @this.SetEulerAngleZ(@this.eulerAngles.z + z);

        /// <summary>
        /// ���[�J����X�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetLocalEulerAngleX(this Transform @this, float x) =>
            @this.localEulerAngles = new Vector3(x, @this.localEulerAngles.y, @this.localEulerAngles.z);

        /// <summary>
        /// ���[�J����Y�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetLocalEulerAngleY(this Transform @this, float y) =>
            @this.localEulerAngles = new Vector3(@this.localEulerAngles.x, y, @this.localEulerAngles.z);

        /// <summary>
        /// ���[�J����Z�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetLocalEulerAngleZ(this Transform @this, float z) =>
            @this.localEulerAngles = new Vector3(@this.localEulerAngles.x, @this.localEulerAngles.y, z);

        /// <summary>
        /// ���[�J����X�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddLocalEulerAngleX(this Transform @this, float x) =>
            @this.SetLocalEulerAngleX(@this.localEulerAngles.x + x);

        /// <summary>
        /// ���[�J����Y�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddLocalEulerAngleY(this Transform @this, float y) =>
            @this.SetLocalEulerAngleY(@this.localEulerAngles.y + y);

        /// <summary>
        /// ���[�J����X�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddLocalEulerAngleZ(this Transform @this, float z) =>
            @this.SetLocalEulerAngleZ(@this.localEulerAngles.z + z);


        /// ----------------------------------------------------------------------------
        // �X�P�[���̐ݒ�

        /// <summary>
        /// X�������̃��[�J�����W�n�̃X�P�[�����O�l��ݒ肵�܂�
        /// </summary>
        public static void SetLocalScaleX(this Transform @this, float x) =>
            @this.localScale = new Vector3(x, @this.localScale.y, @this.localScale.z);

        /// <summary>
        /// Y�������̃��[�J�����W�n�̃X�P�[�����O�l��ݒ肵�܂�
        /// </summary>
        public static void SetLocalScaleY(this Transform @this, float y) =>
            @this.localScale = new Vector3(@this.localScale.x, y, @this.localScale.z);

        /// <summary>
        /// Z�������̃��[�J�����W�n�̃X�P�[�����O�l��ݒ肵�܂�
        /// </summary>
        public static void SetLocalScaleZ(this Transform @this, float z) =>
            @this.localScale = new Vector3(@this.localScale.x, @this.localScale.y, z);

        /// <summary>
        /// X�������̃��[�J�����W�n�̃X�P�[�����O�l�����Z���܂�
        /// </summary>
        public static void AddLocalScaleX(this Transform transform, float x) =>
            transform.SetLocalScaleX(transform.localScale.x + x);

        /// <summary>
        /// Y�������̃��[�J�����W�n�̃X�P�[�����O�l�����Z���܂�
        /// </summary>
        public static void AddLocalScaleY(this Transform transform, float y) =>
            transform.SetLocalScaleY(transform.localScale.y + y);

        /// <summary>
        /// Z�������̃��[�J�����W�n�̃X�P�[�����O�l�����Z���܂�
        /// </summary>
        public static void AddLocalScaleZ(this Transform transform, float z) =>
            transform.SetLocalScaleZ(transform.localScale.z + z);


        /// ----------------------------------------------------------------------------
        // ������

        /// <summary>
        /// ���[�J���̍��W�C��]�C�X�P�[��������������g�����\�b�h
        /// </summary>
        public static void ResetLocal(this Transform @this) {
            @this.localPosition = Vector3.zero;
            @this.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// ���[�J���̍��W�C��]�C�X�P�[��������������g�����\�b�h
        /// </summary>
        public static void ResetWorld(this Transform @this) {
            @this.position = Vector3.zero;
            @this.rotation = Quaternion.identity;
        }

    }



    /// <summary>
    /// GameObject�^�̊g�����\�b�h���Ǘ�����N���X
    /// </summary>
    public static partial class GameObjectExtensions {

        /// ----------------------------------------------------------------------------
        // �ʒu�̐ݒ�

        /// <summary>
        /// �ʒu��ݒ肵�܂�
        /// </summary>
        public static void SetPosition(this GameObject gameObject, Vector3 position) {
            gameObject.transform.position = position;
        }

        /// <summary>
        /// X���W��ݒ肵�܂�
        /// </summary>
        public static void SetPositionX(this GameObject gameObject, float x) {
            gameObject.transform.SetPositionX(x);
        }

        /// <summary>
        /// Y���W��ݒ肵�܂�
        /// </summary>
        public static void SetPositionY(this GameObject gameObject, float y) {
            gameObject.transform.SetPositionY(y);
        }

        /// <summary>
        /// Z���W��ݒ肵�܂�
        /// </summary>
        public static void SetPositionZ(this GameObject gameObject, float z) {
            gameObject.transform.SetPositionZ(z);
        }

        /// <summary>
        /// X���W�ɉ��Z���܂�
        /// </summary>
        public static void AddPositionX(this GameObject gameObject, float x) {
            gameObject.transform.AddPositionX(x);
        }

        /// <summary>
        /// Y���W�ɉ��Z���܂�
        /// </summary>
        public static void AddPositionY(this GameObject gameObject, float y) {
            gameObject.transform.AddPositionY(y);
        }

        /// <summary>
        /// Z���W�ɉ��Z���܂�
        /// </summary>
        public static void AddPositionZ(this GameObject gameObject, float z) {
            gameObject.transform.AddPositionZ(z);
        }

        /// <summary>
        /// ���[�J�����W�n�̈ʒu��ݒ肵�܂�
        /// </summary>
        public static void SetLocalPosition(this GameObject gameObject, Vector3 localPosition) {
            gameObject.transform.localPosition = localPosition;
        }

        /// <summary>
        /// ���[�J�����W�n��X���W��ݒ肵�܂�
        /// </summary>
        public static void SetLocalPositionX(this GameObject gameObject, float x) {
            gameObject.transform.SetLocalPositionX(x);
        }

        /// <summary>
        /// ���[�J�����W�n��Y���W��ݒ肵�܂�
        /// </summary>
        public static void SetLocalPositionY(this GameObject gameObject, float y) {
            gameObject.transform.SetLocalPositionY(y);
        }

        /// <summary>
        /// ���[�J����Z���W��ݒ肵�܂�
        /// </summary>
        public static void SetLocalPositionZ(this GameObject gameObject, float z) {
            gameObject.transform.SetLocalPositionZ(z);
        }

        /// <summary>
        /// ���[�J�����W�n��X���W�ɉ��Z���܂�
        /// </summary>
        public static void AddLocalPositionX(this GameObject gameObject, float x) {
            gameObject.transform.AddLocalPositionX(x);
        }

        /// <summary>
        /// ���[�J�����W�n��Y���W�ɉ��Z���܂�
        /// </summary>
        public static void AddLocalPositionY(this GameObject gameObject, float y) {
            gameObject.transform.AddLocalPositionY(y);
        }

        /// <summary>
        /// ���[�J�����W�n��Z���W�ɉ��Z���܂�
        /// </summary>
        public static void AddLocalPositionZ(this GameObject gameObject, float z) {
            gameObject.transform.AddLocalPositionZ(z);
        }


        /// ----------------------------------------------------------------------------
        // �p�x�̐ݒ�

        /// <summary>
        /// ��]�p��ݒ肵�܂�
        /// </summary>
        public static void SetEulerAngle(this GameObject gameObject, Vector3 eulerAngles) {
            gameObject.transform.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// X�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetEulerAngleX(this GameObject gameObject, float x) {
            gameObject.transform.SetEulerAngleX(x);
        }

        /// <summary>
        /// Y�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetEulerAngleY(this GameObject gameObject, float y) {
            gameObject.transform.SetEulerAngleY(y);
        }

        /// <summary>
        /// Z�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetEulerAngleZ(this GameObject gameObject, float z) {
            gameObject.transform.SetEulerAngleZ(z);
        }

        /// <summary>
        /// X�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddEulerAngleX(this GameObject gameObject, float x) {
            gameObject.transform.AddEulerAngleX(x);
        }

        /// <summary>
        /// Y�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddEulerAngleY(this GameObject gameObject, float y) {
            gameObject.transform.AddEulerAngleY(y);
        }

        /// <summary>
        /// Z�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddEulerAngleZ(this GameObject gameObject, float z) {
            gameObject.transform.AddEulerAngleZ(z);
        }

        /// <summary>
        /// ���[�J�����W�n�̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetLocalEulerAngle(this GameObject gameObject, Vector3 localEulerAngles) {
            gameObject.transform.localEulerAngles = localEulerAngles;
        }

        /// <summary>
        /// ���[�J�����W�n��X�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetLocalEulerAngleX(this GameObject gameObject, float x) {
            gameObject.transform.SetLocalEulerAngleX(x);
        }

        /// <summary>
        /// ���[�J�����W�n��Y�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetLocalEulerAngleY(this GameObject gameObject, float y) {
            gameObject.transform.SetLocalEulerAngleY(y);
        }

        /// <summary>
        /// ���[�J�����W�n��Z�������̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetLocalEulerAngleZ(this GameObject gameObject, float z) {
            gameObject.transform.SetLocalEulerAngleZ(z);
        }

        /// <summary>
        /// ���[�J�����W�n��X�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddLocalEulerAngleX(this GameObject gameObject, float x) {
            gameObject.transform.AddLocalEulerAngleX(x);
        }

        /// <summary>
        /// ���[�J�����W�n��Y�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddLocalEulerAngleY(this GameObject gameObject, float y) {
            gameObject.transform.AddLocalEulerAngleY(y);
        }

        /// <summary>
        /// ���[�J�����W�n��X�������̉�]�p�����Z���܂�
        /// </summary>
        public static void AddLocalEulerAngleZ(this GameObject gameObject, float z) {
            gameObject.transform.AddLocalEulerAngleZ(z);
        }

        /// <summary>
        /// ���[�J�����W�n�̉�]�p��ݒ肵�܂�
        /// </summary>
        public static void SetLocalScale(this GameObject gameObject, Vector3 localScale) {
            gameObject.transform.localScale = localScale;
        }


        /// ----------------------------------------------------------------------------
        // �X�P�[���̐ݒ�

        /// <summary>
        /// X�������̃��[�J�����W�n�̃X�P�[�����O�l��ݒ肵�܂�
        /// </summary>
        public static void SetLocalScaleX(this GameObject gameObject, float x) {
            gameObject.transform.SetLocalScaleX(x);
        }

        /// <summary>
        /// Y�������̃��[�J�����W�n�̃X�P�[�����O�l��ݒ肵�܂�
        /// </summary>
        public static void SetLocalScaleY(this GameObject gameObject, float y) {
            gameObject.transform.SetLocalScaleY(y);
        }

        /// <summary>
        /// Z�������̃��[�J�����W�n�̃X�P�[�����O�l��ݒ肵�܂�
        /// </summary>
        public static void SetLocalScaleZ(this GameObject gameObject, float z) {
            gameObject.transform.SetLocalScaleZ(z);
        }

        /// <summary>
        /// X�������̃��[�J�����W�n�̃X�P�[�����O�l�����Z���܂�
        /// </summary>
        public static void AddLocalScaleX(this GameObject gameObject, float x) {
            gameObject.transform.AddLocalScaleX(x);
        }

        /// <summary>
        /// Y�������̃��[�J�����W�n�̃X�P�[�����O�l�����Z���܂�
        /// </summary>
        public static void AddLocalScaleY(this GameObject gameObject, float y) {
            gameObject.transform.AddLocalScaleY(y);
        }

        /// <summary>
        /// Z�������̃��[�J�����W�n�̃X�P�[�����O�l�����Z���܂�
        /// </summary>
        public static void AddLocalScaleZ(this GameObject gameObject, float z) {
            gameObject.transform.AddLocalScaleZ(z);
        }

        /// <summary>
        /// �e�I�u�W�F�N�g��ݒ肵�܂�
        /// </summary>
        public static void SetParent(this GameObject gameObject, Transform parent) {
            gameObject.transform.parent = parent;
        }

        /// <summary>
        /// �e�I�u�W�F�N�g��ݒ肵�܂�
        /// </summary>
        public static void SetParent(this GameObject gameObject, GameObject parent) {
            gameObject.transform.parent = parent.transform;
        }
    }

}