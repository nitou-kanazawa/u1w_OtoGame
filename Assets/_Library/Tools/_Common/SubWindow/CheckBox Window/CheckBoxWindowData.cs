namespace nitou.Tools {

    public sealed class CheckBoxWindowData : ICheckBoxWindowData {

        public string Name { get; set; }
        public bool IsChecked { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CheckBoxWindowData(string name, bool isChecked) {
            Name = name;
            IsChecked = isChecked;
        }
    }
}