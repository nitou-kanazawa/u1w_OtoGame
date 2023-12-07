using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

namespace OtoGame.LevelObjects {

    [RequireComponent(typeof(AnimancerComponent))]
    public class TitleLevelCharacter : MonoBehaviour {

        private AnimancerComponent _animancer;

        [SerializeField] ClipTransition _idleClip;

        private void Start() {
            _animancer = GetComponent<AnimancerComponent>();
            _animancer.Play(_idleClip);
        }
    }

}