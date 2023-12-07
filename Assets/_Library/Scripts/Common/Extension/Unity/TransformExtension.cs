using UnityEngine;


// [参考]
//  コガネブログ: Transform型の位置や回転角、サイズの設定を楽にする https://baba-s.hatenablog.com/entry/2014/02/28/000000
//  _:  Transformにリセット処理を追加してみる https://ookumaneko.wordpress.com/2015/10/01/unity%E3%83%A1%E3%83%A2-transform%E3%81%AB%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E5%87%A6%E7%90%86%E3%82%92%E8%BF%BD%E5%8A%A0%E3%81%97%E3%81%A6%E3%81%BF%E3%82%8B/#:~:text=%E3%83%AF%E3%83%BC%E3%83%AB%E3%83%89%E3%82%92%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E3%81%97%E3%81%9F%E3%81%84%E6%99%82,%E5%80%A4%E3%82%92%E3%83%AA%E3%82%BB%E3%83%83%E3%83%88%E5%87%BA%E6%9D%A5%E3%81%BE%E3%81%99%E3%80%82

namespace nitou {

    /// <summary>
    /// GameObjectの拡張メソッドクラス
    /// </summary>
    public static partial class TransformExtension {

        /// ----------------------------------------------------------------------------
        // 位置の設定

        /// <summary>
        /// X座標を設定する拡張メソッド
        /// </summary>
        public static void SetPositionX(this Transform @this, float x) =>
            @this.position = new Vector3(x, @this.position.y, @this.position.z);

        /// <summary>
        /// Y座標を設定する拡張メソッド
        /// </summary>
        public static void SetPositionY(this Transform @this, float y) =>
            @this.position = new Vector3(@this.position.x, y, @this.position.z);

        /// <summary>
        /// Z座標を設定する拡張メソッド
        /// </summary>
        public static void SetPositionZ(this Transform @this, float z) =>
            @this.position = new Vector3(@this.position.x, @this.position.y, z);

        /// <summary>
        /// X座標に加算する拡張メソッド
        /// </summary>
        public static void AddPositionX(this Transform @this, float x) =>
            @this.SetPositionX(x + @this.position.x);

        /// <summary>
        /// Y座標に加算する拡張メソッド
        /// </summary>
        public static void AddPositionY(this Transform @this, float y) =>
            @this.SetPositionY(y + @this.position.y);

        /// <summary>
        /// Z座標に加算する拡張メソッド
        /// </summary>
        public static void AddPositionZ(this Transform @this, float z) =>
            @this.SetPositionZ(z + @this.position.z);

        /// <summary>
        /// ローカルのX座標を設定する拡張メソッド
        /// </summary>
        public static void SetLocalPositionX(this Transform @this, float x) =>
            @this.localPosition = new Vector3(x, @this.localPosition.y, @this.localPosition.z);

        /// <summary>
        /// ローカルのY座標を設定する拡張メソッド
        /// </summary>
        public static void SetLocalPositionY(this Transform @this, float y) =>
            @this.localPosition = new Vector3(@this.localPosition.x, y, @this.localPosition.z);

        /// <summary>
        /// ローカルのZ座標を設定する拡張メソッド
        /// </summary>
        public static void SetLocalPositionZ(this Transform @this, float z) =>
            @this.localPosition = new Vector3(@this.localPosition.x, @this.localPosition.y, z);

        /// <summary>
        /// ローカルのX座標に加算する拡張メソッド
        /// </summary>
        public static void AddLocalPositionX(this Transform @this, float x) =>
            @this.SetLocalPositionX(x + @this.localPosition.x);

        /// <summary>
        /// ローカルのY座標に加算する拡張メソッド
        /// </summary>
        public static void AddLocalPositionY(this Transform @this, float y) =>
            @this.SetLocalPositionY(y + @this.localPosition.y);

        /// <summary>
        /// ローカルのZ座標に加算する拡張メソッド
        /// </summary>
        public static void AddLocalPositionZ(this Transform @this, float z) =>
            @this.SetLocalPositionZ(z + @this.localPosition.z);


        /// ----------------------------------------------------------------------------
        // 角度の設定

        /// <summary>
        /// X軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleX(this Transform @this, float x) =>
            @this.eulerAngles = new Vector3(x, @this.eulerAngles.y, @this.eulerAngles.z);

        /// <summary>
        /// Y軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleY(this Transform @this, float y) =>
            @this.eulerAngles = new Vector3(@this.eulerAngles.x, y, @this.eulerAngles.z);

        /// <summary>
        /// Z軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleZ(this Transform @this, float z) =>
            @this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, z);

        /// <summary>
        /// X軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleX(this Transform @this, float x) =>
            @this.SetEulerAngleX(@this.eulerAngles.x + x);

        /// <summary>
        /// Y軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleY(this Transform @this, float y) =>
            @this.SetEulerAngleY(@this.eulerAngles.y + y);

        /// <summary>
        /// Z軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleZ(this Transform @this, float z) =>
            @this.SetEulerAngleZ(@this.eulerAngles.z + z);

        /// <summary>
        /// ローカルのX軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleX(this Transform @this, float x) =>
            @this.localEulerAngles = new Vector3(x, @this.localEulerAngles.y, @this.localEulerAngles.z);

        /// <summary>
        /// ローカルのY軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleY(this Transform @this, float y) =>
            @this.localEulerAngles = new Vector3(@this.localEulerAngles.x, y, @this.localEulerAngles.z);

        /// <summary>
        /// ローカルのZ軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleZ(this Transform @this, float z) =>
            @this.localEulerAngles = new Vector3(@this.localEulerAngles.x, @this.localEulerAngles.y, z);

        /// <summary>
        /// ローカルのX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleX(this Transform @this, float x) =>
            @this.SetLocalEulerAngleX(@this.localEulerAngles.x + x);

        /// <summary>
        /// ローカルのY軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleY(this Transform @this, float y) =>
            @this.SetLocalEulerAngleY(@this.localEulerAngles.y + y);

        /// <summary>
        /// ローカルのX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleZ(this Transform @this, float z) =>
            @this.SetLocalEulerAngleZ(@this.localEulerAngles.z + z);


        /// ----------------------------------------------------------------------------
        // スケールの設定

        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleX(this Transform @this, float x) =>
            @this.localScale = new Vector3(x, @this.localScale.y, @this.localScale.z);

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleY(this Transform @this, float y) =>
            @this.localScale = new Vector3(@this.localScale.x, y, @this.localScale.z);

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleZ(this Transform @this, float z) =>
            @this.localScale = new Vector3(@this.localScale.x, @this.localScale.y, z);

        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleX(this Transform transform, float x) =>
            transform.SetLocalScaleX(transform.localScale.x + x);

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleY(this Transform transform, float y) =>
            transform.SetLocalScaleY(transform.localScale.y + y);

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleZ(this Transform transform, float z) =>
            transform.SetLocalScaleZ(transform.localScale.z + z);


        /// ----------------------------------------------------------------------------
        // 初期化

        /// <summary>
        /// ローカルの座標，回転，スケールを初期化する拡張メソッド
        /// </summary>
        public static void ResetLocal(this Transform @this) {
            @this.localPosition = Vector3.zero;
            @this.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// ローカルの座標，回転，スケールを初期化する拡張メソッド
        /// </summary>
        public static void ResetWorld(this Transform @this) {
            @this.position = Vector3.zero;
            @this.rotation = Quaternion.identity;
        }

    }



    /// <summary>
    /// GameObject型の拡張メソッドを管理するクラス
    /// </summary>
    public static partial class GameObjectExtensions {

        /// ----------------------------------------------------------------------------
        // 位置の設定

        /// <summary>
        /// 位置を設定します
        /// </summary>
        public static void SetPosition(this GameObject gameObject, Vector3 position) {
            gameObject.transform.position = position;
        }

        /// <summary>
        /// X座標を設定します
        /// </summary>
        public static void SetPositionX(this GameObject gameObject, float x) {
            gameObject.transform.SetPositionX(x);
        }

        /// <summary>
        /// Y座標を設定します
        /// </summary>
        public static void SetPositionY(this GameObject gameObject, float y) {
            gameObject.transform.SetPositionY(y);
        }

        /// <summary>
        /// Z座標を設定します
        /// </summary>
        public static void SetPositionZ(this GameObject gameObject, float z) {
            gameObject.transform.SetPositionZ(z);
        }

        /// <summary>
        /// X座標に加算します
        /// </summary>
        public static void AddPositionX(this GameObject gameObject, float x) {
            gameObject.transform.AddPositionX(x);
        }

        /// <summary>
        /// Y座標に加算します
        /// </summary>
        public static void AddPositionY(this GameObject gameObject, float y) {
            gameObject.transform.AddPositionY(y);
        }

        /// <summary>
        /// Z座標に加算します
        /// </summary>
        public static void AddPositionZ(this GameObject gameObject, float z) {
            gameObject.transform.AddPositionZ(z);
        }

        /// <summary>
        /// ローカル座標系の位置を設定します
        /// </summary>
        public static void SetLocalPosition(this GameObject gameObject, Vector3 localPosition) {
            gameObject.transform.localPosition = localPosition;
        }

        /// <summary>
        /// ローカル座標系のX座標を設定します
        /// </summary>
        public static void SetLocalPositionX(this GameObject gameObject, float x) {
            gameObject.transform.SetLocalPositionX(x);
        }

        /// <summary>
        /// ローカル座標系のY座標を設定します
        /// </summary>
        public static void SetLocalPositionY(this GameObject gameObject, float y) {
            gameObject.transform.SetLocalPositionY(y);
        }

        /// <summary>
        /// ローカルのZ座標を設定します
        /// </summary>
        public static void SetLocalPositionZ(this GameObject gameObject, float z) {
            gameObject.transform.SetLocalPositionZ(z);
        }

        /// <summary>
        /// ローカル座標系のX座標に加算します
        /// </summary>
        public static void AddLocalPositionX(this GameObject gameObject, float x) {
            gameObject.transform.AddLocalPositionX(x);
        }

        /// <summary>
        /// ローカル座標系のY座標に加算します
        /// </summary>
        public static void AddLocalPositionY(this GameObject gameObject, float y) {
            gameObject.transform.AddLocalPositionY(y);
        }

        /// <summary>
        /// ローカル座標系のZ座標に加算します
        /// </summary>
        public static void AddLocalPositionZ(this GameObject gameObject, float z) {
            gameObject.transform.AddLocalPositionZ(z);
        }


        /// ----------------------------------------------------------------------------
        // 角度の設定

        /// <summary>
        /// 回転角を設定します
        /// </summary>
        public static void SetEulerAngle(this GameObject gameObject, Vector3 eulerAngles) {
            gameObject.transform.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// X軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleX(this GameObject gameObject, float x) {
            gameObject.transform.SetEulerAngleX(x);
        }

        /// <summary>
        /// Y軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleY(this GameObject gameObject, float y) {
            gameObject.transform.SetEulerAngleY(y);
        }

        /// <summary>
        /// Z軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleZ(this GameObject gameObject, float z) {
            gameObject.transform.SetEulerAngleZ(z);
        }

        /// <summary>
        /// X軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleX(this GameObject gameObject, float x) {
            gameObject.transform.AddEulerAngleX(x);
        }

        /// <summary>
        /// Y軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleY(this GameObject gameObject, float y) {
            gameObject.transform.AddEulerAngleY(y);
        }

        /// <summary>
        /// Z軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleZ(this GameObject gameObject, float z) {
            gameObject.transform.AddEulerAngleZ(z);
        }

        /// <summary>
        /// ローカル座標系の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngle(this GameObject gameObject, Vector3 localEulerAngles) {
            gameObject.transform.localEulerAngles = localEulerAngles;
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleX(this GameObject gameObject, float x) {
            gameObject.transform.SetLocalEulerAngleX(x);
        }

        /// <summary>
        /// ローカル座標系のY軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleY(this GameObject gameObject, float y) {
            gameObject.transform.SetLocalEulerAngleY(y);
        }

        /// <summary>
        /// ローカル座標系のZ軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleZ(this GameObject gameObject, float z) {
            gameObject.transform.SetLocalEulerAngleZ(z);
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleX(this GameObject gameObject, float x) {
            gameObject.transform.AddLocalEulerAngleX(x);
        }

        /// <summary>
        /// ローカル座標系のY軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleY(this GameObject gameObject, float y) {
            gameObject.transform.AddLocalEulerAngleY(y);
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleZ(this GameObject gameObject, float z) {
            gameObject.transform.AddLocalEulerAngleZ(z);
        }

        /// <summary>
        /// ローカル座標系の回転角を設定します
        /// </summary>
        public static void SetLocalScale(this GameObject gameObject, Vector3 localScale) {
            gameObject.transform.localScale = localScale;
        }


        /// ----------------------------------------------------------------------------
        // スケールの設定

        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleX(this GameObject gameObject, float x) {
            gameObject.transform.SetLocalScaleX(x);
        }

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleY(this GameObject gameObject, float y) {
            gameObject.transform.SetLocalScaleY(y);
        }

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleZ(this GameObject gameObject, float z) {
            gameObject.transform.SetLocalScaleZ(z);
        }

        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleX(this GameObject gameObject, float x) {
            gameObject.transform.AddLocalScaleX(x);
        }

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleY(this GameObject gameObject, float y) {
            gameObject.transform.AddLocalScaleY(y);
        }

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleZ(this GameObject gameObject, float z) {
            gameObject.transform.AddLocalScaleZ(z);
        }

        /// <summary>
        /// 親オブジェクトを設定します
        /// </summary>
        public static void SetParent(this GameObject gameObject, Transform parent) {
            gameObject.transform.parent = parent;
        }

        /// <summary>
        /// 親オブジェクトを設定します
        /// </summary>
        public static void SetParent(this GameObject gameObject, GameObject parent) {
            gameObject.transform.parent = parent.transform;
        }
    }

}