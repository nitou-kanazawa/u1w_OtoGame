using System.Threading.Tasks;
using UnityEngine.UI;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class SettingsModal : Modal<SettingsView, SettingsViewState>, ISelectableContainer {

        /// <summary>
        /// デフォルトで選択されるUI
        /// </summary>
        public Selectable DefaultSelection => SelectableGroup?.First;

        /// <summary>
        /// 管理下のUIリスト
        /// </summary>
        public SelectableGroup SelectableGroup { get; set; }

        /// <summary>
        /// システムクラスに選択される時の遅延
        /// </summary>
        public float Delay { get; } = 0f;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 終了処理
        /// </summary>
        public override Task Cleanup() {
            SelectableGroup?.Dispose();
            return base.Cleanup();
        }

    }
}
