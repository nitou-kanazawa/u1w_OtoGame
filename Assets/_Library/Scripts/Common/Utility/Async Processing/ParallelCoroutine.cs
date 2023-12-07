using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [参考]
//  ほととぎす通信: 並列コルーチンを簡単に実装する方法 https://shibuya24.info/entry/unity-parallel-coroutine

namespace nitou {

    /// <summary>
    /// 並列での実行待機が可能なコルーチンクラス
    /// </summary>
    public static class ParallelCoroutine {
        
        /// <summary>
        /// 並列コルーチンの実行
        /// </summary>
        public static ParallelCoroutineResult Execute(MonoBehaviour owner, IList<IEnumerator> ienumeratorList, Action complete = null) {
            List<Coroutine> childCoroutineList = null;
            var mainCoroutine = owner.StartCoroutine(InternalExecute(owner, ienumeratorList, complete, x => childCoroutineList = x));
            return new ParallelCoroutineResult(owner, mainCoroutine, childCoroutineList);
        }

        private static IEnumerator InternalExecute(MonoBehaviour owner,
            IList<IEnumerator> ienumeratorList, Action complete, Action<List<Coroutine>> childCoroutineList) {
            List<Coroutine> coroutineList = new List<Coroutine>();
            for (int i = 0; i < ienumeratorList.Count; i++) {
                var ienumerator = ienumeratorList[i];
                coroutineList.Add(owner.StartCoroutine(ienumerator));
            }
            childCoroutineList.Invoke(coroutineList);

            foreach (var col in coroutineList) {
                yield return col;
            }

            complete?.Invoke();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public readonly struct ParallelCoroutineResult {
        
        private readonly Coroutine _main;
        private readonly List<Coroutine> _childCoroutineList;
        private readonly MonoBehaviour _owner;

        public ParallelCoroutineResult(MonoBehaviour owner, Coroutine main, List<Coroutine> childCoroutineList) {
            _owner = owner;
            _main = main;
            _childCoroutineList = childCoroutineList;
        }

        public void StopCoroutine() {
            if (_owner == null) return;
            _owner.StopCoroutine(_main);

            if (_childCoroutineList != null && _childCoroutineList.Count > 0) {
                foreach (var col in _childCoroutineList)
                    _owner.StopCoroutine(col);
            }
        }
    }

}
