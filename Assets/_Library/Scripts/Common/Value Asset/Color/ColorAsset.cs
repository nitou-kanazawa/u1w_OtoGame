using UnityEngine;

namespace nitou {

    /// <summary>
    /// Colorをアセットとして保存するためのスクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(fileName = "ColorAsset", menuName = "ScriptableObjects/Value Asset/Color Asset")]
    public class ColorAsset : ValueAsset<Color> { }

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class ColorReference : ValueRefrence<Color, ColorAsset> { }

}