using System;
using System.Text;                  // StringBuilder
using System.Linq;
using System.Collections.Generic;


namespace nitou {

    /// <summary>
    /// �z��ɑ΂���ėp�@�\��񋟂��郉�C�u����
    /// </summary>
    public static class MultiDimensionArrayUtility {

        /// ----------------------------------------------------------------------------
        // �z��̐���

        /// <summary>
        /// �����l���w�肵��2�����z��𐶐�
        /// </summary>
        public static T[,] Make2dArray<T>(T value, int xnum = 10, int ynum = 10) {
            var array = Enumerable.Repeat(value, ynum * xnum).ToArray();
            return array.To2dArray(ynum, xnum);
        }


        /// ----------------------------------------------------------------------------
        // ���͔z��src����̐V�K�z�񐶐�

        /// <summary>
        /// �w�肵��2�����z��𕡐�����
        /// </summary>
        public static T[,] Clone<T>(T[,] src) {
            // �z�񏀔�
            int ynum = src.GetLength(0);
            int xnum = src.GetLength(1);
            var dest = new T[ynum, xnum];

            // �l�}��
            for (int y = 0; y < ynum; ++y) {
                for (int x = 0; x < xnum; ++x) {
                    dest[y, x] = src[y, x];
                }
            }
            return dest;
        }

        /// <summary>
        /// �w�肵��2�����z��̈ʒu (x, y) ���� (w to h) �͈̔͂�؂���
        /// </summary>
        public static T[,] Cut<T>(T[,] src, int xID, int yID, int dx, int dy) {
            // �v�f���̌���
            int ynum = src.GetLength(0);
            int xnum = src.GetLength(1);
            if (xID < 0 || xID > xnum || yID < 0 || yID > ynum) {
                string msg = string.Format("Parameter is out of range. src[y={0},x={1}], x={2}, y={3}", ynum, xnum, xID, yID);
                throw new ArgumentOutOfRangeException(msg);
            }
            if (dx == 0 || dy == 0) {
                throw new ArgumentException(string.Format("Invalid parameter. w={0}, h={1}", dx, dy));
            }

            // �R�s�[��̔z��̑傫��
            int rh = yID + dy <= ynum ? dy : dy - (yID + dy - ynum);
            int rw = xID + dx <= xnum ? dx : dx - (xID + dx - xnum);
            // ���̑傫������R�s�[
            var map = new T[rh, rw];
            for (int my = 0, _y = yID; my < rh; my++, _y++) {
                for (int mx = 0, _x = xID; mx < rw; mx++, _x++) {
                    map[my, mx] = src[_y, _x];
                }
            }
            return map;
        }

        /// <summary>
        /// ���͔z��̎��͂Ƀ}�[�W����ǉ������z��𐶐�
        /// (���ǉ��̈��(T)�̏����l�ɂȂ�)
        /// </summary>
        public static T[,] AddMargin<T>(T[,] src, int margin) {
            // �v�f���̌���
            int ynum = src.GetLength(0);
            int xnum = src.GetLength(1);
            if (margin == 0) {
                throw new ArgumentException(string.Format("Invalid parameter. margin={0}", margin));
            }
            // 
            var map = new T[ynum + margin * 2, xnum + margin * 2];
            for (int y = 0; y < ynum; ++y) {
                for (int x = 0; x < xnum; ++x) {
                    map[y + margin, x + margin] = src[y, x];
                }
            }
            return map;
        }


        /// ----------------------------------------------------------------------------
        // �z��`���E�T�C�Y�̕ϊ�

        /// <summary>
        /// �w�肵��2�����z���1�����z��ɕϊ�����
        /// </summary>
        public static T[] To1dArray<T>(T[,] src) {
            // �z�񏀔� -----
            int rowNum = src.GetLength(0);
            int colNum = src.GetLength(1);
            var dest = new T[colNum * rowNum];

            // �l�}�� -----
            for (int r = 0, i = 0; r < rowNum; ++r) {
                for (int c = 0; c < colNum; ++c, ++i) {
                    dest[i] = src[r, c];
                }
            }
            return dest;
        }

        /// <summary>
        /// �w�肵��3�����z���1�����z��ɕϊ�����
        /// </summary>
        public static T[] To1dArray<T>(T[,,] src) {
            // �z�񏀔� -----
            int xnum = src.GetLength(0);
            int ynum = src.GetLength(1);
            int znum = src.GetLength(2);

            var dest = new T[xnum * ynum * znum];
            // �l�}�� -----
            for (int x = 0, i = 0; x < xnum; ++x) {
                for (int y = 0; y < ynum; ++y) {
                    for (int z = 0; z < znum; ++z, ++i) {
                        dest[i] = src[x, y, z];
                    }
                }
            }
            return dest;
        }

        /// <summary>
        /// �w�肵��1�����z���2�����z��ɕϊ�����
        /// (���͈͂𒴂��镪�͐؂�̂āA�s�����Ă��镪��(T)�̏����l�ɂȂ�)
        /// </summary>
        public static T[,] To2dArray<T>(T[] src, int xnum, int ynum) {
            var dest = new T[ynum, xnum];
            int len = xnum * ynum;
            len = src.Length < len ? src.Length : len;
            for (int y = 0, i = 0; y < ynum; y++) {
                for (int x = 0; x < xnum; x++, i++) {
                    if (i >= len) {
                        return dest;
                    }
                    dest[y, x] = src[i];
                }
            }
            return dest;
        }

        /// <summary>
        /// �w�肵��1�����z���3�����z��ɕϊ�����
        /// (���͈͂𒴂��镪�͐؂�̂āA�s�����Ă��镪��(T)�̏����l�ɂȂ�)
        /// </summary>
        public static T[,,] To3dArray<T>(T[] src, int xnum, int ynum, int znum) {
            var dest = new T[xnum, ynum, znum];
            int len = xnum * ynum * znum;
            len = src.Length < len ? src.Length : len;
            for (int x = 0, i = 0; x < xnum; x++) {
                for (int y = 0; y < ynum; y++) {
                    for (int z = 0; z < znum; z++, i++) {

                        if (i >= len) return dest;
                        dest[x, y, z] = src[i];
                    }
                }
            }
            return dest;
        }


        /// ----------------------------------------------------------------------------
        // �f�o�b�O����

        /// <summary>
        /// �ydebug�p�z �w�肵���z��̓��e�𕶎���ɕϊ�����
        /// </summary>
        public static string ToStringByDebug<T>(T[,] src) {
            var a = new StringBuilder();
            var b = new StringBuilder();
            int ymax = src.GetLength(0);
            int xmax = src.GetLength(1);
            for (int iy = 0; iy < ymax; iy++) {
                for (int ix = 0; ix < xmax; ix++) {
                    b.Append($" {src[iy, ix]},");
                }
                a.Append(b.ToString().Trim(' ', ','));
                a.Append(Environment.NewLine);
                b.Clear();
            }
            return a.ToString();
        }

        /// <summary>
        /// �ydebug�p�z �w�肵���z��̓��e�𕶎���ɕϊ�����
        /// </summary>
        public static string ToStringByDebug<T>(T[,,] src, Func<T, bool> evaluate = null) {
            var a = new StringBuilder();
            var b = new StringBuilder();    // �P�s�����i�[

            int ymax = src.GetLength(0);
            int xmax = src.GetLength(1);
            int zmax = src.GetLength(2);
            for (int iy = 0; iy < ymax; iy++) {

                a.Append($" [y = {iy}] ");
                a.Append(Environment.NewLine);
                a.Append(new string('-',zmax*2) + " Z");
                a.Append(Environment.NewLine);

                for (int ix = 0; ix < xmax; ix++) {

                    b.Append((ix==(xmax-1)) ? " X " : " | ");
                    
                    for (int iz = 0; iz < zmax; iz++) {

                        // �l�̒ǉ�
                        var value = src[ix, iy, iz];
                        if (evaluate != null && evaluate(value)) {
                            b.Append($" <color=cyan>{value}</color>,");
                        } else {
                            b.Append($" {value},");
                        }

                    }
                    a.Append(b.ToString().Trim(' ', ','));
                    a.Append(Environment.NewLine);
                    b.Clear();
                }
                a.Append(Environment.NewLine);
            }
            return a.ToString();
        }
    }



    /// <summary>
    /// �z��ɑ΂���g���@�\��񋟂���
    /// </summary>
    public static class MultiDimeArrayExtension {

        /// ----------------------------------------------------------------------------

        public static T[,] Clone<T>(this T[,] src) =>
            MultiDimensionArrayUtility.Clone(src);
        public static T[,] Cut<T>(this T[,] src, int x, int y, int w, int h) =>
            MultiDimensionArrayUtility.Cut(src, x, y, w, h);
        public static T[,] AddMargin<T>(this T[,] src, int m)
            => MultiDimensionArrayUtility.AddMargin(src, m);

        /// ----------------------------------------------------------------------------

        public static T[] To1dArray<T>(this T[,] src) =>
            MultiDimensionArrayUtility.To1dArray(src);
        public static T[] To1dArray<T>(this T[,,] src) =>
            MultiDimensionArrayUtility.To1dArray(src);
        public static T[,] To2dArray<T>(this T[] src, int xmax, int ymax) =>
            MultiDimensionArrayUtility.To2dArray(src, xmax, ymax);
        public static T[,,] To3dArray<T>(this T[] src, int xmax, int ymax, int zmax) =>
            MultiDimensionArrayUtility.To3dArray(src, xmax, ymax, zmax);

        public static string ToStringByDebug<T>(this T[,] src) =>
            MultiDimensionArrayUtility.ToStringByDebug(src);
        public static string ToStringByDebug<T>(this T[,,] src, Func<T, bool> evaluate = null) =>
            MultiDimensionArrayUtility.ToStringByDebug(src, evaluate);


        /// ----------------------------------------------------------------------------
        // �z��̕���

        public static void Deconstruct<T>(this IList<T> self, out T first, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default(T);
            rest = self.Skip(1).ToArray();
        }
        public static void Deconstruct<T>(this IList<T> self, out T first, out T second, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default(T);
            second = self.Count > 1 ? self[1] : default(T);
            rest = self.Skip(2).ToArray();
        }
        public static void Deconstruct<T>(this IList<T> self, out T first, out T second, out T third, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default(T);
            second = self.Count > 1 ? self[1] : default(T);
            third = self.Count > 2 ? self[2] : default(T);
            rest = self.Skip(3).ToArray();
        }
        public static void Deconstruct<T>(this IList<T> self, out T first, out T second, out T third, out T four, out IList<T> rest) {
            first = self.Count > 0 ? self[0] : default(T);
            second = self.Count > 1 ? self[1] : default(T);
            third = self.Count > 2 ? self[2] : default(T);
            four = self.Count > 3 ? self[3] : default(T);
            rest = self.Skip(4).ToArray();
        }

    }




}