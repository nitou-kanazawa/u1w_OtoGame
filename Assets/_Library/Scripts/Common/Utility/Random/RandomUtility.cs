//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using static UnityEngine.Mathf;

namespace nitou {

    /// <summary>
    /// �����Ɋւ���ėp�@�\��񋟂��郉�C�u����
    /// </summary>
    public static class RandomUtility {

        /// ----------------------------------------------------------------------------
        // �^�U

        /// <summary>
        /// bool�^�̗������擾����
        /// </summary>
        public static bool RandomBool() {
            return Range(0, 2) == 0;
        }

        /// <summary>
        /// �C�ӌ^�̂Q�l���烉���_���Ɏ擾����
        /// </summary>
        public static T RandomValue<T>(T a, T b) {
            return RandomBool() ? a : b;
        }

        /// <summary>
        /// �C�ӌ^�̂R�l���烉���_���Ɏ擾����
        /// </summary>
        public static T RandomValue<T>(T a, T b, T c) {
            return Random.Range(0, 3) switch {
                0 => a,
                1 => b,
                2 => c,
                _ => throw new System.NotImplementedException(),
            };
        }


        public static T RadomValue<T>(IReadOnlyList<T> list) {
            return list[Random.Range(0, list.Count)];
        }


        /// ----------------------------------------------------------------------------
        // ���W

        /// <summary>
        /// �~��̃����_���ȓ_���擾����
        /// </summary>
        public static Vector2 PointInCircle(float radius, float startDeg = 0.0f, float endDeg = 359.99999f) {
            float deg = Range(startDeg, endDeg);
            float rad = deg * Deg2Rad;
            Vector2 position = new Vector2(Cos(rad), Sin(rad));

            position *= Range(0.0f, radius);
            return position;
        }

        /// <summary>
        /// �~����̃����_���ȓ_���擾����
        /// </summary>
        public static Vector2 PointInCircumference(float radius, float startDeg = 0.0f, float endDeg = 359.99999f) {
            float deg = Range(startDeg, endDeg);
            float rad = deg * Deg2Rad;
            Vector2 position = new Vector2(Cos(rad), Sin(rad)) * radius;
            return position;
        }

        /// <summary>
        /// �l�p�`��̃����_���ȓ_���擾����
        /// </summary>
        public static Vector2 PointInBox2D(float halfSizeX, float halfSizeY) {
            float posX = Range(-halfSizeX, halfSizeX);
            float posY = Range(-halfSizeY, halfSizeY);

            Vector2 pos = new Vector2(posX, posY);
            return pos;
        }

        /// <summary>
        /// �l�p�`��̃����_���ȓ_���擾����
        /// </summary>
        public static Vector2 PointInBox2D(Vector2 halfSize) {
            return PointInBox2D(halfSize.x, halfSize.y);
        }

        /// <summary>
        /// Box��̃����_���ȓ_���擾����
        /// </summary>
        public static Vector3 PointInBox(float halfSizeX, float halfSizeY, float halfSizeZ) {
            float posX = Range(-halfSizeX, halfSizeX);
            float posY = Range(-halfSizeY, halfSizeY);
            float posZ = Range(-halfSizeZ, halfSizeZ);

            Vector3 pos = new Vector3(posX, posY, posZ);
            return pos;
        }


        /// ----------------------------------------------------------------------------
        // �J���[

        /// <summary>
        /// �����_���ȐF�𐶐�
        /// </summary>
        public static Color Color(bool alpha = false) {
            return new Color(Range(0, 1.0f), Range(0, 1.0f), Range(0, 1.0f), alpha ? Range(0, 1.0f) : 1);

        }

        /// <summary>
        /// �����_���ȐF�𐶐�
        /// </summary>
        public static Color Color(Vector2 red, Vector2 green, Vector2 blue, Vector2 alpha) {
            return new Color(Range(red.x, red.y), Range(green.x, green.y), Range(blue.x, blue.y), Range(alpha.x, alpha.y));
        }
    }

}