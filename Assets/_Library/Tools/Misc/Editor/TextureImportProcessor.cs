using UnityEngine;
using UnityEditor;

// [�Q�l]
//  _: �e�N�X�`���́hGenerate Mip Maps�h�������ŃI�t�ɂ��Ď�Ԃƃ��������Ȃ����� https://spirits.appirits.com/doruby/9277/?cn-reloaded=1

namespace nitou.Tools {

    public class TextureImportProcessor : AssetPostprocessor {


#if TEXTURE_AUTO
        // �e�N�X�`���[�ǂݍ��ݎ��̏���
        public void OnPreprocessTexture() {

            // assetImporter���C���|�[�g�ݒ�������Ă���
            var importer = assetImporter as TextureImporter;

            // ������g������ۂ́C�ۑ��t�H���_���ɂ���Đݒ�𕪊򂳂���̂��y����

            importer.textureType = TextureImporterType.Sprite;  // 
            importer.mipmapEnabled = false;                     // �������ɂ��œK���Ȃ̂ŁC��{OFF.
        }

#endif
    }

}