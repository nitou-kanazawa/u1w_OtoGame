using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

namespace nitou.Camera {

    public class CameraManager : MonoBehaviour {

        /// ----------------------------------------------------------------------------
        #region Inner Enum

        /// <summary>
        /// カメラのタイプ
        /// </summary>
        public enum CameraType {
            Player,
            World,
        }

        #endregion

        /// ----------------------------------------------------------------------------

        [SerializeField] PlayerCameraController _playerCamera;
        [SerializeField] WorldCameraController _worldCamera;

        /// <summary>
        /// 現在のカメラ
        /// </summary>
        public IReadOnlyReactiveProperty<CameraType> CurrentCamera => _currentCamera;
        private ReactiveProperty<CameraType> _currentCamera = new();


        /// ----------------------------------------------------------------------------
        // Public Method

        private void Start() {
            _currentCamera
                .Subscribe(type => {

                    switch (type) {
                        case CameraType.Player:
                            _playerCamera.gameObject.SetActive(true);
                            _worldCamera.gameObject.SetActive(false);
                            break;

                        case CameraType.World:
                            _playerCamera.gameObject.SetActive(false);
                            _worldCamera.gameObject.SetActive(true);
                            break;

                        default:
                            throw new System.NotImplementedException();
                    }


                }).AddTo(this);
        }


        [Button("Player")]
        public void ActivatePlayerCamera() {
            _currentCamera.Value = CameraType.Player;
        }

        [Button("World")]
        public void ActivateWorldCamera() {
            _currentCamera.Value = CameraType.World;
        }
    }

}