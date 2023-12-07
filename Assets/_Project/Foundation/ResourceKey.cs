namespace OtoGame.Foundation{

    public static class ResourceKey {

        /// <summary>
        /// 
        /// </summary>
        public static class UI {

            // ���\�[�X���
            private const string SHEET_PATH = "Sheet";
            private const string PAGE_PATH = "Page";
            private const string MODAL_PATH = "Modal";

            // �R���e�i
            public const string BasicWindowContainer = "Main_Page_Container";
            public const string ControllContainer = "Controll Container";
            public const string ModalContainer = "Modal Container";


            /// ----------------------------------------------------------------------------
            // Page

            /// <summary>
            /// �N�����
            /// </summary>
            public static string TitlePage => $"{PAGE_PATH}/Prefab_Title_Page";
            
            /// <summary>
            /// ���j���[���
            /// </summary>
            public static string MenuTopPage => $"{PAGE_PATH}/Prefab_MenuTop_Page";

            /// <summary>
            /// ���[�h���
            /// </summary>
            public static string LoadingPage => $"{PAGE_PATH}/Prefab_Loading_Page";

            /// <summary>
            /// HUD���
            /// </summary>
            public static string HUDPage => $"{PAGE_PATH}/Prefab_HUD_Page";


            /// ----------------------------------------------------------------------------
            // Modal

            /// <summary>
            /// �ݒ���
            /// </summary>
            public static string SettingsModal => $"{MODAL_PATH}/Prefab_Settings_Modal";
            
            /// <summary>
            /// �N���W�b�g���
            /// </summary>
            public static string CreditModal => $"{MODAL_PATH}/Prefab_Credit_Modal";

            /// <summary>
            /// �|�[�Y���
            /// </summary>
            public static string PauseModal => $"{MODAL_PATH}/Prefab_Pause_Modal";

            /// <summary>
            /// ���U���g���
            /// </summary>
            public static string ResultModal => $"{MODAL_PATH}/Prefab_Result_Modal";
        }

    }
}
