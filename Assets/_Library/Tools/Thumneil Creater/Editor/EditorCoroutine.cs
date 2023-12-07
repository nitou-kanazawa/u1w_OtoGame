#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

// [参考]
//  qiita: UnityEditor上でStartContinueっぽいのを動かす https://qiita.com/k_yanase/items/686fc3134c363ffc5239

namespace nitou.Tools {

    /// <summary>
    /// エディター上でStartCorutineのような処理を使用可能にするクラスです 
    /// </summary>
    [UnityEditor.InitializeOnLoad]
    public sealed class EditorCoroutine {

        static EditorCoroutine() {
            EditorApplication.update += Update;
            Debug.Log("EditorCoroutine SetUp");
        }

        static private Dictionary<IEnumerator, EditorCoroutine.Coroutine> _asyncList = new ();
        static private List<EditorCoroutine.WaitForSeconds> _waitForSecondsList = new ();

        static private void Update() {
            CheackIEnumerator();
            CheackWaitForSeconds();
        }

        static private void CheackIEnumerator() {

            var removeList = new List<IEnumerator>();
            
            foreach (KeyValuePair<IEnumerator, EditorCoroutine.Coroutine> pair in _asyncList) {
                if (pair.Key != null) {

                    //IEnumratorのCurrentがCoroutineを返しているかどうか 
                    var c = pair.Key.Current as EditorCoroutine.Coroutine;
                    if (c != null) {
                        if (c.isActive) continue;
                    }
                    
                    //wwwクラスのダウンロードが終わっていなければ進まない 
                    var www = pair.Key.Current as WWW;
                    if (www != null) {
                        if (!www.isDone) continue;
                    }
                    
                    //これ以上MoveNextできなければ終了 
                    if (!pair.Key.MoveNext()) {
                        if (pair.Value != null) {
                            pair.Value.isActive = false;
                        }
                        removeList.Add(pair.Key);
                    }
                } else {
                    removeList.Add(pair.Key);
                }
            }

            foreach (IEnumerator async in removeList) {
                _asyncList.Remove(async);
            }
        }

        static private void CheackWaitForSeconds() {
            for (int i = 0; i < _waitForSecondsList.Count; i++) {
                if (_waitForSecondsList[i] != null) {
                    if (EditorApplication.timeSinceStartup - _waitForSecondsList[i].InitTime > _waitForSecondsList[i].Time) {
                        _waitForSecondsList[i].isActive = false;
                        _waitForSecondsList.RemoveAt(i);
                    }
                } else {
                    Debug.LogError("rem");
                    _waitForSecondsList.RemoveAt(i);
                }
            }
        }

        //関数 

        /// <summary>
        /// コルーチンを起動します 
        /// </summary>
        static public EditorCoroutine.Coroutine Start(IEnumerator iEnumerator) {

            if (Application.isEditor && !Application.isPlaying) {
                var coroutine = new Coroutine();
                if (!_asyncList.Keys.Contains(iEnumerator)) _asyncList.Add(iEnumerator, coroutine);
                iEnumerator.MoveNext();
                return coroutine;

            } else {
                Debug.LogError("EditorCoroutine.Startはゲーム起動中に使うことはできません");
                return null;
            }
        }

        /// <summary>
        /// コルーチンを停止します 
        /// </summary>
        static public void Stop(IEnumerator iEnumerator) {
            if (Application.isEditor) {
                if (_asyncList.Keys.Contains(iEnumerator)) {
                    _asyncList.Remove(iEnumerator);
                }
            } else {
                Debug.LogError("EditorCoroutine.Startはゲーム中に使うことはできません");
            }
        }

        /// <summary>
        /// 使用不可
        /// WaitForSecondsのインスタンスを登録します 
        /// </summary>
        static public void AddWaitForSecondsList(EditorCoroutine.WaitForSeconds coroutine) {
            if (_waitForSecondsList.Contains(coroutine) == false) {
                _waitForSecondsList.Add(coroutine);
            }
        }


        #region Inner Definition

        //待機処理用クラス 
        public class Coroutine {            
            public bool isActive;   // ※trueなら待機させる 
            public Coroutine() {
                isActive = true;
            }
        }

        public sealed class WaitForSeconds : EditorCoroutine.Coroutine {
            private float _time;
            private double _initTime;
            public float Time {
                get { return _time; }
            }
            public double InitTime {
                get { return _initTime; }
            }

            public WaitForSeconds(float time) : base() {
                this._time = time;
                this._initTime = EditorApplication.timeSinceStartup;
                EditorCoroutine.AddWaitForSecondsList(this);
            }
        }

        #endregion
    }


}
#endif