using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace nitou {

    public class GraphicColorSetter : MonoBehaviour {

        [OnInspectorGUI("AdjustColor")]
        public ColorReference _color = new();

        // �Ώ�
        private Graphic _graphic;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 

        private void OnEnable() {
            AdjustColor();
        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// �ΏۂɃJ���[�ݒ�𔽉f����
        /// </summary>
        private void AdjustColor() {
            if (_graphic == null)
                _graphic = GetComponent<Graphic>();

            if (_graphic != null)
                _graphic.color = _color;
        }
    }

}