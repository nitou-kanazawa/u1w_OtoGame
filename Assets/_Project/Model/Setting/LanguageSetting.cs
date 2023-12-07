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
        /// ‰¹—Ê
        /// </summary>
        public Language CurrentLanguage {
            get => _languageRP.Value;
            set => _languageRP.Value = value;
        }

        /// <summary>
        /// Œ»İ‚ÌŒ¾Œêİ’è‚Ì’Ê’m
        /// </summary>
        public IReadOnlyReactiveProperty<Language> LanguageRP => _languageRP;
        private readonly ReactiveProperty<Language> _languageRP = new();

    }


}