using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

// [�Q�l] ���b�V���𔽓]������@ https://edunity.hatenablog.com/entry/20201113/1605221523

namespace OtoGame.LevelObjects {

    public class NoteMeshController : MonoBehaviour, System.IDisposable{

        [TitleGroup("Graphics objects"), Indent]
        [SerializeField] private GameObject _sphereObj;

        [TitleGroup("Graphics objects"), Indent]
        [SerializeField] private GameObject _fishObj;


        // ����
        private MeshFilter _sphereFilter;
        private MeshRenderer _sphereRenderer;

        // �����f��
        private MeshRenderer _fishRenderer;

        // �}�e���A��
        private Material _sphereMat;


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// ����������
        /// </summary>
        public void Initialize() {

            // �R���|�[�l���g�擾
            _sphereFilter = _sphereObj.GetComponent<MeshFilter>();
            _sphereRenderer = _sphereObj.GetComponent<MeshRenderer>();
            _fishRenderer = _fishObj.GetComponent<MeshRenderer>();

            // �}�e���A��
            _sphereMat = _sphereRenderer.material;

            // �m�[�c���̂̃��b�V���𔽓]������
            Mesh Mesh = Instantiate(_sphereFilter.sharedMesh);
            Mesh.triangles = Mesh.triangles.Reverse().ToArray();
            _sphereFilter.sharedMesh = Mesh;
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
            Destroy(_sphereMat);
        }


        /// ----------------------------------------------------------------------------
        // Public Method (�O���t�B�b�N����)

        /// <summary>
        /// ���̂̃J���[�ݒ�
        /// </summary>
        public void SetSphereColor(Color color) {
            _sphereMat.color = color;
        }

        /// <summary>
        /// �����f����\������
        /// </summary>
        public void ShowFishModel() {
            _fishRenderer.enabled = true;
        }

        /// <summary>
        /// �����f�����\���ɂ���
        /// </summary>
        public void HideFishModel() {
            _fishRenderer.enabled = false;

        }


        /// ----------------------------------------------------------------------------
        // Private Method 
    }

}