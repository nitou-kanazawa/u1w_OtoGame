using System.Threading.Tasks;
using UnityEngine.UI;
using nitou.UI;
using nitou.UI.PresentationFramework;

namespace OtoGame.View {

    public sealed class SettingsModal : Modal<SettingsView, SettingsViewState>, ISelectableContainer {

        /// <summary>
        /// �f�t�H���g�őI�������UI
        /// </summary>
        public Selectable DefaultSelection => SelectableGroup?.First;

        /// <summary>
        /// �Ǘ�����UI���X�g
        /// </summary>
        public SelectableGroup SelectableGroup { get; set; }

        /// <summary>
        /// �V�X�e���N���X�ɑI������鎞�̒x��
        /// </summary>
        public float Delay { get; } = 0f;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �I������
        /// </summary>
        public override Task Cleanup() {
            SelectableGroup?.Dispose();
            return base.Cleanup();
        }

    }
}
