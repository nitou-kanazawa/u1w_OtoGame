#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

// [参考]
//  qiita: 属性(Attribute)に動的な引数を渡す その2 https://qiita.com/mu5dvlp/items/900f8e0009e420cdc101

namespace nitou {

    public class ColoredFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColoredFoldoutGroupAttribute> {
        
        /// <summary>
        /// フォルダが開いた状態かどうか
        /// </summary>
        private LocalPersistentContext<bool> _isExpanded;


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override void Initialize() {
            this._isExpanded = this.GetPersistentValue<bool>(
                "ColoredFoldoutGroupAttributeDrawer.isExpanded",
                GeneralDrawerConfig.Instance.ExpandFoldoutByDefault
            );
        }

        /// <summary>
        /// プロパティの描画
        /// </summary>
        protected override void DrawPropertyLayout(GUIContent label) {

            // カラー情報の取得

            //
            GUIHelper.PushColor(new Color(this.Attribute.R, this.Attribute.G, this.Attribute.B, this.Attribute.A));
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.BeginBoxHeader();
            GUIHelper.PopColor();

            this._isExpanded.Value = SirenixEditorGUI.Foldout(this._isExpanded.Value, label);
            SirenixEditorGUI.EndBoxHeader();

            // グループ変数の描画
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