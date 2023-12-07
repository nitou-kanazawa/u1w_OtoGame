using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


namespace OtoGame.Presentation {

    /// <summary>
    /// UI��ʂ̑J�ڃC�x���g���`����C���^�[�t�F�[�X
    /// ��Presenter���W���[�����Œ�`����
    /// </summary>
    public interface ITransitionService {

        // �N����

        // �^�C�g�����
        void TitlePage_StartButtonClicked();

        // ���j���[���
        void MenuPage_PlayButtonClicked();
        void MenuPage_CreditButtonClicked();
        void MenuPage_SettingsButtonClicked();


        // ���[�h���
        void LoadPage_AfterPush();
        UniTask LoadPage_BeforePop();
        void LoadPage_AfterPop();


        // �|�[�Y���
        void PausePage_ContinueButtonClicked();
        void PausePage_RestartButtonClicked();
        void PausePage_QuitButtonClicked();

        // ���U���g���
        void ResultPage_QuitButtonCliked();
        void ResultPage_TweenButtonCliked();

        // 


        // 
        void PopCommandExecuted();


    }
}
