#nullable enable
using System.Linq;
using UnityEditor;
using UnityEngine;

// [�Q�l]
//  _: Hierarchy �ŃI�u�W�F�N�g�̃R���|�[�l���g�ꗗ���A�C�R���\�� https://www.midnightunity.net/unity-extension-hierarchy-show-components/

namespace nitou {

    /// <summary>
    /// Hierarchy�E�B���h�E�ɃR���|�[�l���g�̃A�C�R����\������g���@�\
    /// </summary>
    public static class ComponentIconDrawerInHierarchy {

        /// ----------------------------------------------------------------------------
        // Field

        /// <summary>
        /// �A�C�R���̃T�C�Y
        /// </summary>
        private const int ICON_SIZE = 16;

        /// <summary>
        /// ���j���[�p�X
        /// </summary>
        private const string MENU_PATH = "GameObject/�R���|�[�l���g�A�C�R���\���ؑ�";

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
            // �X�N���v�g�A�C�R���̎擾
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
        /// �q�G�����L�[��ɃR���|�[�l���g�̃A�C�R����\������C�x���g�n���h��
        /// </summary>
        private static void DisplayHierarchyIcons(int instanceID, Rect selectionRect) {

            // instanceID���I�u�W�F�N�g�Q�Ƃɕϊ�
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) { return; }

            // �I�u�W�F�N�g���������Ă���R���|�[�l���g�ꗗ���擾
            var components = gameObject
                .GetComponents<Component>()
                .Where(x => x is not Transform)
                .ToArray();
            if (components.Length == 0) { return; }


            // �`��ʒu
            var pos = selectionRect;
            pos.x = pos.xMax - ICON_SIZE;
            pos.width = ICON_SIZE;
            pos.height = ICON_SIZE;

            // �A�C�R���̕`��
            var existsScriptIcon = false;
            foreach (var component in components) {

                // �R���|�[�l���g�̃A�C�R���摜���擾
                var image = AssetPreview.GetMiniThumbnail(component);
                if (image == null) continue;

                // Script�̃A�C�R����1�̂ݕ\��
                if (image == _scriptIcon) {
                    if (existsScriptIcon) continue;
                    existsScriptIcon = true;
                }

                // �`��
                DrawIcon(ref pos, image, component.IsEnabled() ? Color.white : _disableColor);
            }
        }

        /// <summary>
        /// �A�C�R����`�悷��
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
        /// �R���|�[�l���g���L�����ǂ������m�F����g�����\�b�h
        /// </summary>
        /// <param name="this">�g���Ώ�</param>
        /// <returns>�R���|�[�l���g���L���ƂȂ��Ă��邩�ǂ���</returns>
        private static bool IsEnabled(this Component @this) {
            var property = @this.GetType().GetProperty("enabled", typeof(bool));
            return (bool)(property?.GetValue(@this, null) ?? true);
        }
    }
}