using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [�Q�l]
//  �˂�����炵�e�B: SmoothDamp�Ŋ��炩�ȒǏ]���������� https://nekojara.city/unity-smooth-damp
//  _ : SmoothDamp���\���̉����Ďg���₷������ https://tech.ftvoid.com/smooth-damp-struct

namespace nitou {

    /// ----------------------------------------------------------------------------
    #region Float

    /// <summary>
    /// �l��ڕW�l�Ɋ��炩�ɒǏ]������\����
    /// �i��Mathf.SmoothDamp�j
    /// </summary>
    public class SmoothDampFloat {

        private float _current;
        private float _currentVelocity;

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public float GetNext(float target, float smoothTime, float maxSpeed, float deltaTime) {
            _current = Mathf.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime
            );

            return _current;
        }

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public float GetNext(float target, float smoothTime, float maxSpeed) {
            _current = Mathf.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed
            );

            return _current;
        }

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public float GetNext(float target, float smoothTime) {
            _current = Mathf.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime
                );

            return _current;
        }

    }

    /// <summary>
    /// �p�x��ڕW�l�Ɋ��炩�ɒǏ]������\����
    /// �i��Mathf.SmoothDampAngle�j
    /// </summary>
    public class SmoothDampAngle {

        private float _current;
        private float _currentVelocity;

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public float GetNext(float target, float smoothTime, float maxSpeed, float deltaTime) {
            _current = Mathf.SmoothDampAngle(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime
            );

            return _current;
        }

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public float GetNext(float target, float smoothTime, float maxSpeed) {
            _current = Mathf.SmoothDampAngle(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed
            );

            return _current;
        }

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public float GetNext(float target, float smoothTime) {
            _current = Mathf.SmoothDampAngle(
                _current,
                target,
                ref _currentVelocity,
                smoothTime
                );

            return _current;
        }

    }

    #endregion


    /// ----------------------------------------------------------------------------
    #region Vector2

    /// <summary>
    /// �l��ڕW�l�Ɋ��炩�ɒǏ]������\����
    /// �i��Mathf.SmoothDamp�j
    /// </summary>
    public class SmoothDampVector2 {

        private Vector2 _current;
        private Vector2 _currentVelocity;

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public Vector2 GetNext(Vector2 target, float smoothTime, float maxSpeed, float deltaTime) {
            _current = Vector2.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime
            );

            return _current;
        }

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public Vector2 GetNext(Vector2 target, float smoothTime, float maxSpeed) {
            _current = Vector2.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed
            );

            return _current;
        }

        /// <summary>
        /// ���������ꂽ�l���擾����
        /// </summary>
        public Vector2 GetNext(Vector2 target, float smoothTime) {
            _current = Vector2.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime
                );

            return _current;
        }

    }

    #endregion

}