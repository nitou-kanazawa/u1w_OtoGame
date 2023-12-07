using UnityEngine;

namespace nitou {

    /// <summary>
    /// Color���A�Z�b�g�Ƃ��ĕۑ����邽�߂̃X�N���v�^�u���I�u�W�F�N�g
    /// </summary>
    [CreateAssetMenu(fileName = "ColorAsset", menuName = "ScriptableObjects/Value Asset/Color Asset")]
    public class ColorAsset : ValueAsset<Color> { }

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class ColorReference : ValueRefrence<Color, ColorAsset> { }

}