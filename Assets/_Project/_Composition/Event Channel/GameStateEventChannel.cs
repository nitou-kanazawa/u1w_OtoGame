using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OtoGame.Composition {

    /// <summary>
    /// �X�e�[�g�̑J�ڃC�x���g
    /// </summary>
    public enum GameStateEvent {
        ToMenuPage,
        ToTitlePage,
        ToCregitPage,
        ToSettingsPage,

        PopCommand,

        CloseUI,
        OpenOptions,        // �I�v�V������          
        OpenInventory,      // �C���x���g�����J��
        StartConversation,  // ��b���n�߂�
    }


    [CreateAssetMenu(fileName = "GameState_Event", menuName = "EventChannel/GameState Event")]
    public class GameStateEventChannel : ScriptableObject {

        // �������i�����������j
        [Multiline]
        [SerializeField] private string _description = default;
        public string Description => _description;

        // �C�x���g
        public event System.Action<GameStateEvent> OnEventRaised = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �C�x���g�̔���
        /// </summary>
        [Button("Raise")]
        public void RaiseEvent(GameStateEvent eventId) {
            OnEventRaised.Invoke(eventId);
        }

    }

}