using System;

// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedField.Global

namespace nitou.Tools {

    /// <summary>
    /// Assembly Definition Reference（.asmref）のJSONを表すクラス
    /// </summary>
    [Serializable]
    public sealed class JsonAssemblyDefinitionReference {
        public string reference = string.Empty;
    }
}