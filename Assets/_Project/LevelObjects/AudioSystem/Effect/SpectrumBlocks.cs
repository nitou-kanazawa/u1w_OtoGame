using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [�Q�l]
//  �R�K�l�u���O: �I�[�f�B�I�̎��g���X�y�N�g�����擾�ł���uunity-audio-spectrum�v�Љ� https://baba-s.hatenablog.com/entry/2017/12/30/230200
//  ���������ɕς��������ǂ�����
//  �R�K�l�u���O: �I�[�f�B�I�̃r�[�g�ƃX�y�N�g���������m�ł���uUnity-Beat-Detection�v�Љ� https://baba-s.hatenablog.com/entry/2018/01/24/085500

/// <summary>
/// �I�[�f�B�I�̎��g���X�y�N�g�����ɍ��킹�ĐU������u���b�N
/// ��BGM�����ł͂Ȃ�SE���܂܂�Ă���
/// </summary>
[RequireComponent(typeof(AudioSpectrum))]
public class SpectrumBlocks : MonoBehaviour {

    // ���g����͗p
    private AudioSpectrum _spectrum;

    // ����Ώ�
    [SerializeField] Transform _blocksParent;
    private List<Transform> _blockList = new List<Transform>();

    // �p�����[�^
    [SerializeField] float _scale =3;

    /// --------------------------------------------------------------------
    // MonoBehaviour Method 

    /// 
    private void Awake() {
        _spectrum = GetComponent<AudioSpectrum>();

        // �u���b�N�̎擾
        foreach(Transform block in _blocksParent) {
            _blockList.Add(block);
        }

        //Debug.Log(_spectrum.Levels.Length);
    }

    ///
    private void Update() {

        var spectrumNum = _spectrum.Levels.Length;
        var num = Mathf.Min(spectrumNum, _blockList.Count);

        // �X�y�N�g�����ɉ������X�P�[����ݒ�
        for (int i = 0; i < num; i++) {
            var cube = _blockList[i];
            var localScale = cube.localScale;
            localScale.y = _spectrum.Levels[i] * _scale;
            cube.localScale = localScale;
        }
    }
}
