using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

namespace nitou.Camera {

    /// <summary>
    /// TPS用のプレイヤーカメラ
    /// </summary>
    public class PlayerCameraController : MonoBehaviour {

        // 通常カメラ
        [Title("Free Look Camera")]
        [SerializeField] private CinemachineFreeLook _freeLookCM;
        [SerializeField] private Transform _target;

        // エイム用カメラ
        [Title("Aim Camera")]
        [SerializeField] private CinemachineVirtualCamera _aimCM;
        [SerializeField] private Transform _aimReference;

        // 各種設定
        [Title("Settings")]
        [SerializeField] CinemachineInputProvider _inputProvider;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// カメラ操作をON
        /// </summary>
        public void ActivateControll() {
            _inputProvider.enabled = true;
        }

        /// <summary>
        /// カメラ操作をOF
        /// </summary>
        public void DeactivateControll() {
            _inputProvider.enabled = false;
        }


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 




#if UNITY_EDITOR
        private void OnValidate() {
            // 通常カメラ
            if (_freeLookCM != null && _target != null) {
                _freeLookCM.m_Follow = _target;
                _freeLookCM.m_LookAt = _target;
            }

            // エイムカメラ
            if (_aimCM != null && _aimReference) {
                _aimCM.m_Follow = _aimReference;
                _aimCM.m_LookAt = _aimReference;
            }
        }
#endif

    }

}