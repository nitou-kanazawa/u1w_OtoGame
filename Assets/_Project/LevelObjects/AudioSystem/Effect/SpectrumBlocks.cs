using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [参考]
//  コガネブログ: オーディオの周波数スペクトルを取得できる「unity-audio-spectrum」紹介 https://baba-s.hatenablog.com/entry/2017/12/30/230200
//  ↓こっちに変えた方が良さそう
//  コガネブログ: オーディオのビートとスペクトラムを検知できる「Unity-Beat-Detection」紹介 https://baba-s.hatenablog.com/entry/2018/01/24/085500

/// <summary>
/// オーディオの周波数スペクトラムに合わせて振動するブロック
/// ※BGMだけではなくSEも含まれている
/// </summary>
[RequireComponent(typeof(AudioSpectrum))]
public class SpectrumBlocks : MonoBehaviour {

    // 周波数解析用
    private AudioSpectrum _spectrum;

    // 制御対象
    [SerializeField] Transform _blocksParent;
    private List<Transform> _blockList = new List<Transform>();

    // パラメータ
    [SerializeField] float _scale =3;

    /// --------------------------------------------------------------------
    // MonoBehaviour Method 

    /// 
    private void Awake() {
        _spectrum = GetComponent<AudioSpectrum>();

        // ブロックの取得
        foreach(Transform block in _blocksParent) {
            _blockList.Add(block);
        }

        //Debug.Log(_spectrum.Levels.Length);
    }

    ///
    private void Update() {

        var spectrumNum = _spectrum.Levels.Length;
        var num = Mathf.Min(spectrumNum, _blockList.Count);

        // スペクトラムに応じたスケールを設定
        for (int i = 0; i < num; i++) {
            var cube = _blockList[i];
            var localScale = cube.localScale;
            localScale.y = _spectrum.Levels[i] * _scale;
            cube.localScale = localScale;
        }
    }
}
