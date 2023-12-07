using System.Collections;
using System.Collections.Generic;
using System.Linq;

// [参考]
//  kanのメモ帳: Enumerable.Rangeを使って簡単かつスマートに連番のListを作る https://kan-kikuchi.hatenablog.com/entry/EnumerableRange


namespace nitou {

    /// <summary>
    /// Listに関する汎用機能を提供するライブラリ
    /// </summary>
    public static class ListUtility {

        /// <summary>
        /// start〜end(含む)の連番を順に含んだListを作成し取得
        /// </summary>
        public static List<int> RangeNumbers(int start, int end) {
            if (start == end) {
                return new List<int>() { start};
            }
            if (start > end) {
                return Enumerable.Range(end, start - end + 1).Reverse().ToList();
            }
            return Enumerable.Range(start, end - start + 1).ToList();
        }

    }
}