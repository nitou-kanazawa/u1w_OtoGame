using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace OtoGame.Composition {

    /// <summary>
    /// ステートの遷移イベント
    /// </summary>
    public enum GameStateEvent {
        ToMenuPage,
        ToTitlePage,
        ToCregitPage,
        ToSettingsPage,

        PopCommand,

        CloseUI,
        OpenOptions,        // オプションを          
        OpenInventory,      // インベントリを開く
        StartConversation,  // 会話を始める
    }


    [CreateAssetMenu(fileName = "GameState_Event", menuName = "EventChannel/GameState Event")]
    public class GameStateEventChannel : ScriptableObject {

        // 説明文（※メモ書き）
        [Multiline]
        [SerializeField] private string _description = default;
        public string Description => _description;

        // イベント
        public event System.Action<GameStateEvent> OnEventRaised = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// イベントの発火
        /// </summary>
        [Button("Raise")]
        public void RaiseEvent(GameStateEvent eventId) {
            OnEventRaised.Invoke(eventId);
        }

    }

}