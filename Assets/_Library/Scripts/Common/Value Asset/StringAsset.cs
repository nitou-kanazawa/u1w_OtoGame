using UnityEngine;

namespace nitou {


    /// <summary>
    /// Stringをアセットとして保存するためのスクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(fileName = "StringAsset", menuName = "ScriptableObjects/Value Asset/String Asset")]
    public class StringAsset : ValueAsset<string> { }

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class StringReference : ValueRefrence<string, StringAsset> { }

}