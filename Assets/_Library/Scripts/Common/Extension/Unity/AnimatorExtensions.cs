using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou {

    public static class AnimatorExtensionMethods {


        public static void Play(this Animator self) {
            self.Play(0);
        }

        public static void Play(this IReadOnlyList<Animator> self) {
            foreach (var animator in self) {
                animator.Play();
            }
        }
    }

}