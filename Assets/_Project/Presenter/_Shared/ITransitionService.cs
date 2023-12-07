using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


namespace OtoGame.Presentation {

    /// <summary>
    /// UI画面の遷移イベントを定義するインターフェース
    /// ※Presenterモジュール側で定義する
    /// </summary>
    public interface ITransitionService {

        // 起動時

        // タイトル画面
        void TitlePage_StartButtonClicked();

        // メニュー画面
        void MenuPage_PlayButtonClicked();
        void MenuPage_CreditButtonClicked();
        void MenuPage_SettingsButtonClicked();


        // ロード画面
        void LoadPage_AfterPush();
        UniTask LoadPage_BeforePop();
        void LoadPage_AfterPop();


        // ポーズ画面
        void PausePage_ContinueButtonClicked();
        void PausePage_RestartButtonClicked();
        void PausePage_QuitButtonClicked();

        // リザルト画面
        void ResultPage_QuitButtonCliked();
        void ResultPage_TweenButtonCliked();

        // 


        // 
        void PopCommandExecuted();


    }
}
