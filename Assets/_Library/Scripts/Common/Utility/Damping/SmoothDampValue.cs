using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [参考]
//  ねこじゃらしティ: SmoothDampで滑らかな追従を実装する https://nekojara.city/unity-smooth-damp
//  _ : SmoothDampを構造体化して使いやすくする https://tech.ftvoid.com/smooth-damp-struct

namespace nitou {

    /// ----------------------------------------------------------------------------
    #region Float

    /// <summary>
    /// 値を目標値に滑らかに追従させる構造体
    /// （※Mathf.SmoothDamp）
    /// </summary>
    public class SmoothDampFloat {

        private float _current;
        private float _currentVelocity;

        /// <summary>
        /// 平滑化された値を取得する
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
        /// 平滑化された値を取得する
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
        /// 平滑化された値を取得する
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
    /// 角度を目標値に滑らかに追従させる構造体
    /// （※Mathf.SmoothDampAngle）
    /// </summary>
    public class SmoothDampAngle {

        private float _current;
        private float _currentVelocity;

        /// <summary>
        /// 平滑化された値を取得する
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
        /// 平滑化された値を取得する
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
        /// 平滑化された値を取得する
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
    /// 値を目標値に滑らかに追従させる構造体
    /// （※Mathf.SmoothDamp）
    /// </summary>
    public class SmoothDampVector2 {

        private Vector2 _current;
        private Vector2 _currentVelocity;

        /// <summary>
        /// 平滑化された値を取得する
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
        /// 平滑化された値を取得する
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
        /// 平滑化された値を取得する
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