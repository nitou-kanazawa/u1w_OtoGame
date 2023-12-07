using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Sirenix.OdinInspector;

namespace OtoGame.LevelObjects {

    [RequireComponent(typeof(Animator), typeof(AnimancerComponent))]
    public class PlayerAnimation : MonoBehaviour {

        private AnimancerComponent _animancer;
        private Animator _animator;


        [TitleGroup("Animation clips"), Indent]
        [SerializeField] ClipTransition _idleClip;

        [TitleGroup("Animation clips"), Indent]
        [SerializeField] ClipTransition _attackClip;


        // 現在のアニメーション
        private AnimancerState _animState;

        // 
        private (float min, float max) range = new(0.05f, 0.35f);


        /// ----------------------------------------------------------------------------
        // Public Method 

        public void Initialize() {
            // コンポーネント取得
            _animancer = GetComponent<AnimancerComponent>();
            _animator = GetComponent<Animator>();

            _animator.applyRootMotion = false;

            // 標準アニメーション再生
            _animState = _animancer.Play(_attackClip);
            _animState.Speed = 0f;
        }

        public void OnAttack() {
            var now = _animState.Time;
            _animState.Time = (range.min <= now && now <= range.max) ? range.min : 0f;
            _animState.Speed = 1f;
        }


        /// ----------------------------------------------------------------------------
        // Private Method 


        public void SetAnimationSpeed(float value = 1f) {
            _animState.Speed = value;
        }


        /// ----------------------------------------------------------------------------
        // Private Method 


        private void Start() {
            Initialize();
        }


        private void Update() {
            if (Input.GetKeyDown(KeyCode.Return))
                OnAttack();
        }
    }

}