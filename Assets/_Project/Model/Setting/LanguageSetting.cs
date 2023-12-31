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
        /// 音量
        /// </summary>
        public Language CurrentLanguage {
            get => _languageRP.Value;
            set => _languageRP.Value = value;
        }

        /// <summary>
        /// 現在の言語設定の通知
        /// </summary>
        public IReadOnlyReactiveProperty<Language> LanguageRP => _languageRP;
        private readonly ReactiveProperty<Language> _languageRP = new();

    }


}