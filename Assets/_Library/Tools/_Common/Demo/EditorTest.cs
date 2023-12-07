using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

using nitou.Tools;

public static class EditorTest {


    private sealed class Data : ICheckBoxWindowData {
        public string Name { get; }
        public bool IsChecked { get; set; }

        public Data(string name) {
            Name = name;
        }
    }

    [MenuItem("Tools/MyCustom/CheckBoxes")]
    public static void Open() {

        var names = new[]
        {
            "フシギダネ",
            "フシギソウ",
            "フシギバナ",
        };

        var dataArray = names
                .Select(x => new Data(x))
                .ToArray()
            ;


        CheckBoxWindow.Open(title: "Select", dataList: dataArray, onOk: _ => {
            var values = dataArray
                .Where(x => x.IsChecked)
                .Select(x => x.Name);
            Debug.Log(string.Join(",", values));
        
        });
    }



}
