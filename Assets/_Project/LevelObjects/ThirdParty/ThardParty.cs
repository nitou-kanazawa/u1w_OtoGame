using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace OtoGame.LevelObjects {

    public class ThardParty : MonoBehaviour {

        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _firstPos;

        private List<GameObject> _boatList;
        private readonly int maxCount = 6;



        // Tween
        private readonly float gap = 2.4f;
        private float duration = 0.5f;
        private Sequence tween;

        // Start is called before the first frame update
        void Start() {
            _boatList = new List<GameObject>();

            for (int i = 0; i < maxCount; i++) {
                var obj = GameObject.Instantiate(_prefab, _firstPos);
                obj.SetActive(false);
                obj.transform.position = this.transform.position;
                _boatList.Add(obj);
            }
        }

        public void ShowBoat(int num) {
            if(num <= 0) {
                HideAll();
            }else if (num >= maxCount) {
                return;
            }

            var endPos = _firstPos.position + Vector3.left * (num * gap);
            tween?.Kill();
            tween = DOTween.Sequence();
            for (int i = 0; i < maxCount; i++) {

                // •\Ž¦
                if (i < num) {
                    var pos = endPos - Vector3.left * (i * gap);
                    _boatList[i].SetActive(true);
                    tween.Join(_boatList[i].transform.DOJump(pos, 0.3f, 1, duration).SetEase(Ease.InOutCubic));
                    //_boatList[i].transform.DOMove(pos, duration).SetEase(Ease.InOutCubic).SetLink(this.gameObject);
                }
                // ”ñ•\Ž¦
                else {

                }
            }
            tween.SetLink(this.gameObject);
        }

        public void HideAll() {
            tween?.Kill();

            foreach (var obj in _boatList) {
                obj.SetActive(false);
                obj.transform.position = this.transform.position;
            }
        }

        // Update is called once per frame
        void Update() {
            //if (Input.GetKeyDown(KeyCode.Return)) {
            //    Debug.Log("Shoe Boat");
            //    ShowBoat();
            //}

            //if (Input.GetKeyDown(KeyCode.Space)) {
            //    Debug.Log("Hide Boat");
            //    HideAll();
            //}

        }
    }

}