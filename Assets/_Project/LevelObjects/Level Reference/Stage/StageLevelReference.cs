using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace OtoGame.LevelObjects {


    public class StageLevelReference : LevelReference {

        /// ----------------------------------------------------------------------------
        // Field 

        [Title("Reference")]

        [SerializeField] NotesManager _notesManager;
        [SerializeField] Player _player;

        [Title("Others")]

        [SerializeField] Transform _frontWater;
        private Vector3 _defaultPosition;
        private Vector3 _hidePosition;
        private readonly float hideDuration = 1f;


        [SerializeField] ThardParty _thardParty;


        /// ----------------------------------------------------------------------------
        // Properiy

        /// <summary>
        /// プレイヤー
        /// </summary>
        public NotesManager NotesManager => _notesManager;

        /// <summary>
        /// プレイヤー
        /// </summary>
        public Player Player => _player;


        /// <summary>
        /// 観客
        /// </summary>
        public ThardParty ThardParty => _thardParty;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void Start() {
            // 
            _defaultPosition = _frontWater.position;
            _hidePosition = _defaultPosition + Vector3.down * 5f;

        }


        /// ----------------------------------------------------------------------------
        // Public Method

        public UniTask HideFrontWater() {
            return _frontWater.DOMove(_hidePosition, hideDuration).SetEase(Ease.InCubic)
                .OnComplete(() => _frontWater.gameObject.SetActive(false))
                .SetLink(_frontWater.gameObject)
                .ToUniTask();
        }

    }

}