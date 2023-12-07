using UnityEngine;
using UnityEditor;

// [参考]
//  _: テクスチャの”Generate Mip Maps”を自動でオフにして手間とメモリを省きたい https://spirits.appirits.com/doruby/9277/?cn-reloaded=1

namespace nitou.Tools {

    public class TextureImportProcessor : AssetPostprocessor {


#if TEXTURE_AUTO
        // テクスチャー読み込み時の処理
        public void OnPreprocessTexture() {

            // assetImporterがインポート設定を持っている
            var importer = assetImporter as TextureImporter;

            // ※今後拡張する際は，保存フォルダ名によって設定を分岐させるのが楽そう

            importer.textureType = TextureImporterType.Sprite;  // 
            importer.mipmapEnabled = false;                     // ※距離による最適化なので，基本OFF.
        }

#endif
    }

}