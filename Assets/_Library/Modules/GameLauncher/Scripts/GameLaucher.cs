using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

// [参考]
//  kanのメモ帳: ゲーム開始(起動)時に一度だけ初期化処理するクラス(エディタ上でも使える) https://kan-kikuchi.hatenablog.com/entry/StartupInitializer
//  LIGHT11: シーンのロードと初期化タイミングをちゃんと理解する https://light11.hatenadiary.com/entry/2022/02/24/202754
//  Documentation: RuntimeInitializeOnLoadMethodAttribute https://docs.unity3d.com/ja/2022.2/ScriptReference/RuntimeInitializeOnLoadMethodAttribute.html

namespace nitou.Launcher {

    /// <summary>
    /// ゲーム起動時に一度だけ初期化処理を行うクラス
    /// （※シーン上のオブジェクトにアタッチしておく必要はない）
    /// </summary>
    public static class GameLaucher {

        // ※シーンはBuildSettingsに登録しておく
        private const string BOOT_SCENE = "BootScene";
        private const string START_SCENE = "Title";

        /// <summary>
        /// 初期化が完了したかどうか
        /// </summary>
        public static IObservable<Unit> OnInitializedAsync => _initializedAsyncSubject;
        private static AsyncSubject<Unit> _initializedAsyncSubject = new AsyncSubject<Unit>();


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// ゲーム開始時(シーン読み込み前、Awake前)に実行される初期化メソッド
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void RuntimeInitialize() {
            var currentScene = SceneManager.GetActiveScene();
            if (currentScene.name != START_SCENE) return;

            var cts = new CancellationTokenSource();

            Debug_.Log("----------------------------", Colors.GreenYellow);
            Debug_.Log("[GameLauncher] Start booting", Colors.GreenYellow);

            // BootSceneの読み込み
            var bootScene = await GetOrLoadBootScene();


            // 子Launcherメソッドの実行（※BootSceneのRootに配置しておく）
            var tasks = new List<UniTask>();
            var token = cts.Token;
            foreach (var obj in bootScene.GetRootGameObjects()) {

                // インターフェースを実装している場合，
                if (obj.TryGetComponent<ILauncherComponent>(out var childLauncer)) {
                    //Debug_.Log($"Execute {obj.name}'s launcher method.");
                    tasks.Add(childLauncer.RuntimeInitialize(token));
                }
            }
            await UniTask.WhenAll(tasks);

            await UnLoadBootScene();

            Debug_.Log("[GameLauncher] Complete booting", Colors.GreenYellow);
            Debug_.Log("----------------------------", Colors.GreenYellow);

            // ----------------------------------
            //初期化完了通知
            _initializedAsyncSubject.OnNext(Unit.Default);
            _initializedAsyncSubject.OnCompleted();

        }

        /// <summary>
        /// BootSceneを取得する．または読み込む
        /// </summary>
        private static async UniTask<Scene> GetOrLoadBootScene() {
            var currentScene = SceneManager.GetActiveScene();

            // 開始シーンがBootSceneで無い場合，
            if (currentScene.name != BOOT_SCENE) {
                // シーン読み込み
                await SceneManager.LoadSceneAsync(BOOT_SCENE, LoadSceneMode.Additive).ToUniTask();
            }

            return SceneManager.GetSceneByName(BOOT_SCENE);
        }

        /// <summary>
        /// BootSceneを破棄する
        /// </summary>
        private static async UniTask UnLoadBootScene() {
            if (SceneManager.GetActiveScene().name == BOOT_SCENE) return;

            var bootScene = SceneManager.GetSceneByName(BOOT_SCENE);
            await SceneManager.UnloadSceneAsync(bootScene);

        }

    }

}