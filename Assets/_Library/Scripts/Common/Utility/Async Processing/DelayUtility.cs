using UnityEngine;
using System.Collections;
using System;

// [�Q�l]
//  qiita: ��莞�Ԍ�ɃX�N���v�g�̏������Ăяo�����@�܂Ƃ� https://qiita.com/toRisouP/items/e6d4f114d434ee588044 
//  Unity�Œx��������������@�F�X: https://12px.com/blog/2016/11/unity-delay/

namespace nitou {

	/// <summary>
	/// �x�������Ɋւ���ėp�@�\��񋟂��郉�C�u����
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