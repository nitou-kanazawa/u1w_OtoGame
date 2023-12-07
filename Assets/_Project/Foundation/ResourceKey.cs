namespace OtoGame.Foundation{

    public static class ResourceKey {

        /// <summary>
        /// 
        /// </summary>
        public static class UI {

            // リソース情報
            private const string SHEET_PATH = "Sheet";
            private const string PAGE_PATH = "Page";
            private const string MODAL_PATH = "Modal";

            // コンテナ
            public const string BasicWindowContainer = "Main_Page_Container";
            public const string ControllContainer = "Controll Container";
            public const string ModalContainer = "Modal Container";


            /// ----------------------------------------------------------------------------
            // Page

            /// <summary>
            /// 起動画面
            /// </summary>
            public static string TitlePage => $"{PAGE_PATH}/Prefab_Title_Page";
            
            /// <summary>
            /// メニュー画面
            /// </summary>
            public static string MenuTopPage => $"{PAGE_PATH}/Prefab_MenuTop_Page";

            /// <summary>
            /// ロード画面
            /// </summary>
            public static string LoadingPage => $"{PAGE_PATH}/Prefab_Loading_Page";

            /// <summary>
            /// HUD画面
            /// </summary>
            public static string HUDPage => $"{PAGE_PATH}/Prefab_HUD_Page";


            /// ----------------------------------------------------------------------------
            // Modal

            /// <summary>
            /// 設定画面
            /// </summary>
            public static string SettingsModal => $"{MODAL_PATH}/Prefab_Settings_Modal";
            
            /// <summary>
            /// クレジット画面
            /// </summary>
            public static string CreditModal => $"{MODAL_PATH}/Prefab_Credit_Modal";

            /// <summary>
            /// ポーズ画面
            /// </summary>
            public static string PauseModal => $"{MODAL_PATH}/Prefab_Pause_Modal";

            /// <summary>
            /// リザルト画面
            /// </summary>
            public static string ResultModal => $"{MODAL_PATH}/Prefab_Result_Modal";
        }

    }
}
