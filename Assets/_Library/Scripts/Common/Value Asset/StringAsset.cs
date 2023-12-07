using UnityEngine;

namespace nitou {


    /// <summary>
    /// String���A�Z�b�g�Ƃ��ĕۑ����邽�߂̃X�N���v�^�u���I�u�W�F�N�g
    /// </summary>
    [CreateAssetMenu(fileName = "StringAsset", menuName = "ScriptableObjects/Value Asset/String Asset")]
    public class StringAsset : ValueAsset<string> { }

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class StringReference : ValueRefrence<string, StringAsset> { }

}