using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace nitou.Tools {

    /// <summary>
    /// Assembly Definition (.asmdef)�̃C���X�y�N�^�[�ɑ���GUI��ǉ�����N���X
    /// </summary>
    [InitializeOnLoad]
    internal static class AssemblyDefinitionAssetHeaderGUI {

        /// <summary>
        /// �`�F�b�N�E�C���h�E�ւ̕\���p�f�[�^
        /// </summary>
        private sealed class Data : ICheckBoxWindowData {

            public string GUID { get; }
            public AssemblyDefinitionAsset AssemblyDefinitionAsset { get; }
            public string Name { get; }
            public bool IsValid { get; }
            public bool IsChecked { get; set; }

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            public Data(string guid, string[] referenceGUIDArray) {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);

                GUID = guid;
                AssemblyDefinitionAsset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assetPath);
                IsValid = AssemblyDefinitionAsset != null;
                Name = IsValid ? AssemblyDefinitionAsset.name : string.Empty;
                IsChecked = referenceGUIDArray.Contains(guid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static AssemblyDefinitionAssetHeaderGUI() {
            Editor.finishedDefaultHeaderGUI -= OnGUI;
            Editor.finishedDefaultHeaderGUI += OnGUI;
        }

        /// <summary>
        /// �`�F�b�N�{�b�N�X�ŎQ�ƃA�Z���u���t�@�C���𕡐��I���ł���E�C���h�E��\��
        /// </summary>
        /// <param name="editor"></param>
        private static void OnGUI(Editor editor) {
            var assemblyDefinitionImporter = editor.target as AssemblyDefinitionImporter;

            if (assemblyDefinitionImporter == null) return;

            var assetPath = assemblyDefinitionImporter.assetPath;
            var json = File.ReadAllText(assetPath);
            var jsonAssemblyDefinition = JsonUtility.FromJson<JsonAssemblyDefinition>(json);

            var referenceGUIDArray = jsonAssemblyDefinition.references
                    .Select(x => x.Replace("GUID:", ""))
                    .ToArray();

            // ����{�^��
            GUI.color = MyEditorConfig.BasicButtonColor;
            if (GUILayout.Button("Select References")) {

                // Project���̑Sasmdef�t�@�C��
                var dataArray = AssetDatabase
                        .FindAssets($"t:{nameof(AssemblyDefinitionAsset)}")
                        .Select(x => new Data(x, referenceGUIDArray))
                        .Where(x => x.IsValid)
                        .OrderBy(x => x.Name)
                        .ToArray();

                // 
                CheckBoxWindow.Open(
                    title: "Select References",
                    dataList: dataArray,
                    onOk: OnOk
                );

                // �R�[���o�b�N �i��asmdef�t�@�C���̎Q�Ə����X�V�j
                void OnOk(IReadOnlyList<ICheckBoxWindowData> _) {
                    var oldReferences = jsonAssemblyDefinition.references.ToArray();

                    var newReferences = dataArray
                            .Where(x => x.IsChecked)
                            .Select(x => $"GUID:{x.GUID}")
                            .ToArray()
                        ;

                    if (oldReferences.SequenceEqual(newReferences)) return;

                    jsonAssemblyDefinition.references = newReferences;

                    var newJson = JsonUtility.ToJson(jsonAssemblyDefinition, true);

                    File.WriteAllText(assetPath, newJson, Encoding.UTF8);
                    AssetDatabase.Refresh();
                }
            }

        }

    }
}