using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JetBrains.Annotations;


// [参考]
//  コガネブログ: DictionaryをJsonUtilityで変換できるようにするクラス https://baba-s.hatenablog.com/entry/2020/11/20/080300

namespace nitou {

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class JsonDictionary<TKey, TValue> : ISerializationCallbackReceiver {
                
        [SerializeField] [UsedImplicitly] private KeyValuePair[] dictionary = default;

        private Dictionary<TKey, TValue> m_dictionary;

        public Dictionary<TKey, TValue> Dictionary => m_dictionary;


        /// ----------------------------------------------------------------------------
        // Public Method

        public JsonDictionary(Dictionary<TKey, TValue> dictionary) {
            m_dictionary = dictionary;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() {
            dictionary = m_dictionary
                    .Select(x => new KeyValuePair(x.Key, x.Value))
                    .ToArray();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() {
            m_dictionary = dictionary.ToDictionary(x => x.Key, x => x.Value);
            dictionary = null;
        }


        /// ----------------------------------------------------------------------------
        // Inner class 

        [Serializable]
        private struct KeyValuePair {
            [SerializeField] [UsedImplicitly] private TKey key;
            [SerializeField] [UsedImplicitly] private TValue value;

            public TKey Key => key;
            public TValue Value => value;

            public KeyValuePair(TKey key, TValue value) {
                this.key = key;
                this.value = value;
            }
        }
    }
}