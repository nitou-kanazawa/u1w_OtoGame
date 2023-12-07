using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

namespace nitou.Camera {

    /// <summary>
    /// TPS�p�̃v���C���[�J����
    /// </summary>
    public class PlayerCameraController : MonoBehaviour {

        // �ʏ�J����
        [Title("Free Look Camera")]
        [SerializeField] private CinemachineFreeLook _freeLookCM;
        [SerializeField] private Transform _target;

        // �G�C���p�J����
        [Title("Aim Camera")]
        [SerializeField] private CinemachineVirtualCamera _aimCM;
        [SerializeField] private Transform _aimReference;

        // �e��ݒ�
        [Title("Settings")]
        [SerializeField] CinemachineInputProvider _inputProvider;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �J���������ON
        /// </summary>
        public void ActivateControll() {
            _inputProvider.enabled = true;
        }

        /// <summary>
        /// �J���������OF
        /// </summary>
        public void DeactivateControll() {
            _inputProvider.enabled = false;
        }


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 




#if UNITY_EDITOR
        private void OnValidate() {
            // �ʏ�J����
            if (_freeLookCM != null && _target != null) {
                _freeLookCM.m_Follow = _target;
                _freeLookCM.m_LookAt = _target;
            }

            // �G�C���J����
            if (_aimCM != null && _aimReference) {
                _aimCM.m_Follow = _aimReference;
                _aimCM.m_LookAt = _aimReference;
            }
        }
#endif

    }

}