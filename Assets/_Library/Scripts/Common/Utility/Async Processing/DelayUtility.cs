using UnityEngine;
using System.Collections;
using System;

// [参考]
//  qiita: 一定時間後にスクリプトの処理を呼び出す方法まとめ https://qiita.com/toRisouP/items/e6d4f114d434ee588044 
//  Unityで遅延処理をする方法色々: https://12px.com/blog/2016/11/unity-delay/

namespace nitou {

	/// <summary>
	/// 遅延処理に関する汎用機能を提供するライブラリ
	/// </summary>
	public class DelayUtility : MonoBehaviour {

		private static readonly DelayUtility self;

		static DelayUtility() {
			GameObject obj = new GameObject("DelayUtility");
			self = obj.AddComponent<DelayUtility>();
			GameObject.DontDestroyOnLoad(obj);
		}

		public static Coroutine Delay(float waitTime, Action action) {
			return self.StartCoroutine(DelayMethod(waitTime, action));
		}
		public static Coroutine Delay<T>(float waitTime, Action<T> action, T t) {
			return self.StartCoroutine(DelayMethod(waitTime, action, t));
		}

		private static IEnumerator DelayMethod(float waitTime, Action action) {
			yield return new WaitForSeconds(waitTime);
			action();
		}
		private static IEnumerator DelayMethod<T>(float waitTime, Action<T> action, T t) {
			yield return new WaitForSeconds(waitTime);
			action(t);
		}

		public static void Stop(Coroutine coroutine) {
			self.StopCoroutine(coroutine);
		}

	}

}