using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Modal;
using nitou;
using nitou.UI;
using nitou.UI.PresentationFramework;

using OtoGame.Foundation;
using OtoGame.View;
using OtoGame.Presentation;
using OtoGame.LevelObjects;


public class Debug_Entry : MonoBehaviour {

    [SerializeField] ScreenContainer screenContainer;

    [SerializeField] TitleLevelReference _levelReference;


    // Start is called before the first frame update
    async void Start() {

        //await screenContainer.PushPage<Page>("DebugPage", true);

        //await screenContainer.PushPage<Page>("DebugPage 1", true);

        //await screenContainer.PushPage<Page>("DebugPage 2", true);

        //screenContainer.DebugLog();


        await screenContainer.PageContainer.Push<TitlePage>(ResourceKey.UI.TitlePage, true,
                onLoad: x => {
                    var page = x.page;

                    // 基本プレゼンター
                    var presenter = new TitlePagePresenter(page, null);
                    OnPagePresenterCreated(presenter, page);

                    // 追加プレゼンター
                    var additionalPresenter = new TitlePageLevelPresenter(page, _levelReference);
                    OnPagePresenterCreated(additionalPresenter, page);
                });

    }



    /// ----------------------------------------------------------------------------
    // Public Methord (画面遷移に関するイベント)

    /// <summary>
    /// Pageプレゼンターのセットアップ
    /// </summary>
    private IPagePresenter OnPagePresenterCreated(IPagePresenter presenter, Page page, bool shouldInitialize = true) {
        if (shouldInitialize) {
            ((IPresenter)presenter).Initialize();
            presenter.AddTo(page.gameObject);
        }
        return presenter;
    }

    /// <summary>
    /// Modalプレゼンターのセットアップ
    /// </summary>
    private IModalPresenter OnModalPresenterCreated(IModalPresenter presenter, Modal modal, bool shouldInitialize = true) {
        if (shouldInitialize) {
            ((IPresenter)presenter).Initialize();
            presenter.AddTo(modal.gameObject);
        }
        return presenter;
    }

}
