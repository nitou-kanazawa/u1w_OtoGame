using UnityEngine;

// [éQçl]
//  youtube: Unite Austin 2017 - Game Architecture with Scriptable Objects https://www.youtube.com/watch?v=raQ3iHhE_Kk
//  youtube: Value Reference Toggling - Scriptable Objects as Value Assets https://www.youtube.com/watch?v=RZJWwn40T8E&list=PL_HIoK0xBTK5h671a3uUNAtjdLLxrgasD


namespace nitou {

    public abstract class ValueAsset<T> : ScriptableObject {
        public T value;
    }

}