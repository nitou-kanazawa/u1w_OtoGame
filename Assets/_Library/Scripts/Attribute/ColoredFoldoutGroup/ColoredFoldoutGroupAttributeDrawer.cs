#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

// [�Q�l]
//  qiita: ����(Attribute)�ɓ��I�Ȉ�����n�� ����2 https://qiita.com/mu5dvlp/items/900f8e0009e420cdc101

namespace nitou {

    public class ColoredFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColoredFoldoutGroupAttribute> {
        
        /// <summary>
        /// �t�H���_���J������Ԃ��ǂ���
        /// </summary>
        private LocalPersistentContext<bool> _isExpanded;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// ����������
        /// </summary>
        protected override void Initialize() {
            this._isExpanded = this.GetPersistentValue<bool>(
                "ColoredFoldoutGroupAttributeDrawer.isExpanded",
                GeneralDrawerConfig.Instance.ExpandFoldoutByDefault
            );
        }

        /// <summary>
        /// �v���p�e�B�̕`��
        /// </summary>
        protected override void DrawPropertyLayout(GUIContent label) {

            // �J���[���̎擾

            //
            GUIHelper.PushColor(new Color(this.Attribute.R, this.Attribute.G, this.Attribute.B, this.Attribute.A));
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.BeginBoxHeader();
            GUIHelper.PopColor();

            this._isExpanded.Value = SirenixEditorGUI.Foldout(this._isExpanded.Value, label);
            SirenixEditorGUI.EndBoxHeader();

            // �O���[�v�ϐ��̕`��
            if (SirenixEditorGUI.BeginFadeGroup(this, this._isExpanded.Value)) {
                for (int i = 0; i < this.Property.Children.Count; i++) {
                    this.Property.Children[i].Draw();
                }
            }

            SirenixEditorGUI.EndFadeGroup();
            SirenixEditorGUI.EndBox();
        }
    }

}
#endif