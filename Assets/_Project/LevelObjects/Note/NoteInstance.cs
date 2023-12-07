using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Sirenix.OdinInspector;
using nitou;

namespace OtoGame.LevelObjects {
    using OtoGame.Model;

    public class NoteInstance : MonoBehaviour {

        /// ----------------------------------------------------------------------------
        // Field & Properiy

        [TitleGroup("Reference components"), Indent]
        [SerializeField] private NoteMeshController _meshController;

        /// <summary>
        /// �m�[�c�f�[�^
        /// </summary>
        public NoteData Data { get; private set; }

        /// <summary>
        /// �ō��^�C�~���O [sec]
        /// </summary>
        public float RaiseTime => Data.raiseTiming;

        /// <summary>
        /// 
        /// </summary>
        public int Type => Data.type;


        /// ----------------------------------------------------------------------------
        // Field & Properiy (���)

        /// <summary>
        /// �i���p�����[�^
        /// </summary>
        public float Progress { get; set; }

        /// <summary>
        /// �m�[�c�����ɑł��ꂽ���ǂ���
        /// </summary>
        public bool IsKnocked { get; private set; }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// ����������
        /// </summary>
        public void Initialize() {
            _meshController.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Setup(NoteData data) {
            Data = data;
            
            Progress = 0f;
            IsKnocked = false;
        }

        public void Show() {
            _meshController.SetSphereColor(Colors.WhiteSmoke);
            _meshController.ShowFishModel();
        }

        public void Hide() { 
        }


        /// <summary>
        /// �q�b�g�������̏���
        /// </summary>
        public void OnHit(Judgement judge) {
            // �����ς�or���茋�ʂ��s���ȏꍇ�C�������Ȃ�
            if (IsKnocked || !judge.IsHit()) {
                return;
            }

            // ���̂̃J���[�ݒ�
            switch (judge) {
                case Judgement.GREAT:
                    _meshController.SetSphereColor(Colors.Green);
                    break;

                case Judgement.GOOD:
                    _meshController.SetSphereColor(Colors.Yellow);
                    break;

                case Judgement.BAD:
                    _meshController.SetSphereColor(Colors.Red);
                    break;
            }

            // �����f�����\����
            _meshController.HideFishModel();

            IsKnocked = true;
        }

    }

}