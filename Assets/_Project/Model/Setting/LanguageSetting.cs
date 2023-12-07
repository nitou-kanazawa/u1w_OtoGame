using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace OtoGame.Model {

    public class LanguageSetting {

        public enum Language {
            Japanese,
            English,
        }

        /// <summary>
        /// ����
        /// </summary>
        public Language CurrentLanguage {
            get => _languageRP.Value;
            set => _languageRP.Value = value;
        }

        /// <summary>
        /// ���݂̌���ݒ�̒ʒm
        /// </summary>
        public IReadOnlyReactiveProperty<Language> LanguageRP => _languageRP;
        private readonly ReactiveProperty<Language> _languageRP = new();

    }


}