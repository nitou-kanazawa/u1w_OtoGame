using System.IO;
using UnityEditor;
using UnityEngine;

// [�Q�l]
//  unity doqument: �N�����G�f�B�^�[�X�N���v�g���s https://docs.unity3d.com/ja/2019.4/Manual/RunningEditorCodeOnLaunch.html

namespace nitou.Tools {

    [InitializeOnLoad]
    public class CustomFolder {
        
        /// <summary>
        /// �R���X�g���N�^�i�ÓI�j
        /// </summary>
        static CustomFolder() {
            IconDictionaryCreator.BuildDictionary();
            EditorApplication.projectWindowItemOnGUI += DrawFolderIcon;
        }
        
        /// <summary>
        /// 
        /// </summary>
        static void DrawFolderIcon(string guid, Rect rect) {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var iconDictionary = IconDictionaryCreator.IconDictionary;
            var fileName = Path.GetFileName(path);

            // �]��
            if (path == "" ||
                Event.current.type != EventType.Repaint ||
                !File.GetAttributes(path).HasFlag(FileAttributes.Directory)) {
                return;
            }

            // Icon�摜�̎擾
            (bool isExist, Texture texture) = IconDictionaryCreator.GetIconTexture(fileName);
            if (!isExist ||texture == null) {
                return;
            }

            // Icon�摜�̔��f
            Rect imageRect;
            if (rect.height > 20) {
                imageRect = new Rect(rect.x - 1, rect.y - 1, rect.width + 2, rect.width + 2);
            } else if (rect.x > 20) {
                imageRect = new Rect(rect.x - 1, rect.y - 1, rect.height + 2, rect.height + 2);
            } else {
                imageRect = new Rect(rect.x + 2, rect.y - 1, rect.height + 2, rect.height + 2);
            }                      
            GUI.DrawTexture(imageRect, texture);
        }
    }
}
