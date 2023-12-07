using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.Input {

    public class DemoController : MonoBehaviour {

        [SerializeField] InputReaderSO _input;

        [Range(0, 10)]
        [SerializeField] float _speed = 4;

        private Camera _camera;


        // Start is called before the first frame update
        void Start() {
            //_input.MoveEvent += Move;
            //_input.AttackEvent += Attack;
            _camera = Camera.main;
        }

        private void Update() {
            Move(_input.ReadMoveValue());
        }


        private void Move(Vector3 inputDireciton) {
            var transedDirection = _camera.transform.TransformDirection(inputDireciton);
            var moveDirection = Vector3.ProjectOnPlane(transedDirection, Vector3.up);
            transform.position += moveDirection * Time.deltaTime * _speed;
        }

        private void Attack() {

        }
    }

}