namespace nitou.Tools {

    public interface ICheckBoxWindowData {
        
        /// <summary>
        /// データ名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// チェックの有無
        /// </summary>
        bool IsChecked { get; set; }
    }
}