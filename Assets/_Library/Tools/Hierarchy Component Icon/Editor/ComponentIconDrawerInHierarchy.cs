#nullable enable
using System.Linq;
using UnityEditor;
using UnityEngine;

// [参考]
//  _: Hierarchy でオブジェクトのコンポーネント一覧をアイコン表示 https://www.midnightunity.net/unity-extension-hierarchy-show-components/

namespace nitou {

    /// <summary>
    /// Hierarchyウィンドウにコンポーネントのアイコンを表示する拡張機能
    /// </summary>
    public static class ComponentIconDrawerInHierarchy {

        /// ----------------------------------------------------------------------------
        // Field

        /// <summary>
        /// アイコンのサイズ
        /// </summary>
        private const int ICON_SIZE = 16;

        /// <summary>
        /// メニューパス
        /// </summary>
        private const string MENU_PATH = "GameObject/コンポーネントアイコン表示切替";

        private static readonly Color _disableColor = new(1.0f, 1.0f, 1.0f, 0.5f);

        private static Texture? _scriptIcon;
        private const string SCRIPT_ICON_NAME = "cs Script Icon";

        private static bool enabled = true;


        /// ----------------------------------------------------------------------------
        // Private Method

        [InitializeOnLoadMethod]
        private static void Initialize() {

            EditorApplication.hierarchyWindowItemOnGUI += DisplayHierarchyIcons;
            //        UpdateEnabled();

#pragma warning disable UNT0023 // Coalescing assignment on Unity objects
            // スクリプトアイコンの取得
            _scriptIcon ??= EditorGUIUtility.IconContent(SCRIPT_ICON_NAME).image;
#pragma warning restore UNT0023
        }

        /*

        [MenuItem(MenuPath, false, 20)]
        private static void ToggleEnabled() {
            enabled = !enabled;
            UpdateEnabled();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void UpdateEnabled() {
            EditorApplication.hierarchyWindowItemOnGUI -= DisplayIcons;
            if (enabled)
                EditorApplication.hierarchyWindowItemOnGUI += DisplayIcons;
        }


        */

        /// <summary>
        /// ヒエラルキー上にコンポーネントのアイコンを表示するイベントハンドラ
        /// </summary>
        private static void DisplayHierarchyIcons(int instanceID, Rect selectionRect) {

            // instanceIDをオブジェクト参照に変換
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) { return; }

            // オブジェクトが所持しているコンポーネント一覧を取得
            var components = gameObject
                .GetComponents<Component>()
                .Where(x => x is not Transform)
                .ToArray();
            if (components.Length == 0) { return; }


            // 描画位置
            var pos = selectionRect;
            pos.x = pos.xMax - ICON_SIZE;
            pos.width = ICON_SIZE;
            pos.height = ICON_SIZE;

            // アイコンの描画
            var existsScriptIcon = false;
            foreach (var component in components) {

                // コンポーネントのアイコン画像を取得
                var image = AssetPreview.GetMiniThumbnail(component);
                if (image == null) continue;

                // Scriptのアイコンは1つのみ表示
                if (image == _scriptIcon) {
                    if (existsScriptIcon) continue;
                    existsScriptIcon = true;
                }

                // 描画
                DrawIcon(ref pos, image, component.IsEnabled() ? Color.white : _disableColor);
            }
        }

        /// <summary>
        /// アイコンを描画する
        /// </summary>
        private static void DrawIcon(ref Rect pos, Texture image, Color color) {
            Color defaultColor = GUI.color;
            GUI.color = color;

            GUI.DrawTexture(pos, image, ScaleMode.ScaleToFit);
            pos.x -= pos.width;

            GUI.color = defaultColor;
        }


        /// ----------------------------------------------------------------------------

        /// <summary>
        /// コンポーネントが有効かどうかを確認する拡張メソッド
        /// </summary>
        /// <param name="this">拡張対象</param>
        /// <returns>コンポーネントが有効となっているかどうか</returns>
        private static bool IsEnabled(this Component @this) {
            var property = @this.GetType().GetProperty("enabled", typeof(bool));
            return (bool)(property?.GetValue(@this, null) ?? true);
        }
    }
}