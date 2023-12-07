using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using UniRx;
using Sirenix.OdinInspector;
using nitou;

namespace OtoGame.LevelObjects {
    using OtoGame.Model;
    using OtoGame.Utility;

    /// <summary>
    /// ノートを管理するコンポーネント
    /// </summary>
    public class NotesManager : MonoBehaviour, IMusicReactor {

        /// ----------------------------------------------------------------------------
        // Field & Properity

        [Title("Prefab objects")]

        [AssetsOnly]
        [SerializeField] private NoteInstance _notePrefab;

        [AssetsOnly]
        [SerializeField] private GameObject _linePrefab;

        [AssetsOnly]
        [SerializeField] private GameObject _hitPosPrefab;


        /// ----------------------------------------------------------------------------
        // Field & Properity

        [Title("Notes path")]
        public SplineContainer _splineContainer;
        public float _preBeatCount = 4f;
        public float _postBeatCount = 1f;


        // パスの対応時間
        private float _preDuration;     // 前方（※打位置までの範囲）
        private float _postDuration;    // 後方（※打位置を過ぎた範囲）

        // パスの対応パラメータ
        private float _hitPosParam;     // ノーツを叩く位置のパラメータ（※値域[0~1]）
        private float _oneBeetParam;    // 1Beatに対応するパラメータ（※値域[0~1]）


        /// ----------------------------------------------------------------------------
        // Field & Properity

        /// <summary>
        /// 対象の音源
        /// </summary>
        public AudioClip TargetClip { get; private set; }

        // 譜面情報
        private int _notesCount;
        private float[] _timingArray;
        private int[] _typeArray;

        /// <summary>
        /// 現在の譜面位置（※画面に表示し始めるノーツ）
        /// </summary>
        [ShowInInspector, ReadOnly]
        public int NextIndex { get; private set; }

        // オブジェクトプール
        private NoteInstancePool _pool;
        private List<NoteInstance> _activeNoteList = new();

        // 補助線オブジェクト
        private List<GameObject> _lineList = new();


        /// <summary>
        /// 初期化が完了したかどうか
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// 実行できる状態かどうか
        /// </summary>
        public bool IsSetuped { get; private set; } = false;

        AudioClip IMusicReactor.TargetClip => throw new System.NotImplementedException();

        bool IMusicReactor.IsSetuped => throw new System.NotImplementedException();


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method 


        private void OnDestroy() {
            _pool?.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize() {
            if (IsInitialized) { return; }

            // オブジェクトプールの生成
            _pool = new NoteInstancePool(_notePrefab, this.transform, GetSplinePos(0f));
            _pool.PreloadAsync(10, 10).Subscribe().AddTo(this);

            // フラグ更新
            IsInitialized = true;
        }


        /// <summary>
        /// 音源データを与えてセットアップする
        /// </summary>
        /// <param name="musicData"></param>
        public void Setup(MusicData musicData) {
            if (!IsInitialized) throw new System.Exception("コンポーネントが初期化されていません");

            // 既にデータ登録済みの場合，先に破棄する
            if (IsSetuped) {
                Teardown();
            }

            // 音源・譜面情報
            TargetClip = musicData.Clip;
            _notesCount = musicData.TimingArray.Count();
            _timingArray = musicData.TimingArray;
            _typeArray = musicData.KeyArray;

            // パラメータ（時間）
            var oneBeat = Util.Bpm2Sec(musicData.BPM);
            _preDuration = oneBeat * _preBeatCount;
            _postDuration = oneBeat * _postBeatCount;

            // パラメータ（正規値）
            _oneBeetParam = 1.0f / (_preBeatCount + _postBeatCount);
            _hitPosParam = _preBeatCount / (_preBeatCount + _postBeatCount);

            // 補助線オブジェクトの生成
            CreateAxulayObjects();

            // 内部処理用
            NextIndex = 0;


            // フラグ更新
            IsSetuped = true;
        }

        /// <summary>
        /// 音源データをクリアする
        /// </summary>
        public void Teardown() {
            if (!IsInitialized) throw new System.Exception("コンポーネントが初期化されていません");

            // 音源・譜面情報
            TargetClip = null;
            _notesCount = 0;
            _timingArray = null;
            _typeArray = null;

            // 未返却インスタンスを処理
            foreach(var note in _activeNoteList) {
                _pool.Return(note);
            }
            _activeNoteList.Clear();

            // フラグ更新
            IsSetuped = false;
        }


        /// <summary>
        /// 指定のオーディオ再生時間に最も近いノーツを取得する
        /// </summary>
        public bool TryGetNearestNote(float audioTime, out NoteInstance nearest) {

            if (_activeNoteList.Count == 0) {
                nearest = null;
                return false;
            }

            // ※全探索なので，forで打ち切りにしてもよい
            nearest = _activeNoteList
                .OrderBy(n => Mathf.Abs(n.RaiseTime - audioTime))
                .First();
            return true;
        }

        /// <summary>
        /// 指定のオーディオ再生時間よりも前のノーツをノック済み状態にする
        /// </summary>
        public int CountUnKnockNotes(float audioTime) {
            if (_activeNoteList.Count == 0) {
                return 0;
            }

            return _activeNoteList.Count(n => n.RaiseTime < audioTime && !n.IsKnocked);
        }


        public Vector3 GetHitPos() {
            return GetSplinePos(_hitPosParam);
        }

        /// ----------------------------------------------------------------------------
        // Event Method 

        /// <summary>
        /// Audio再生に合わせた更新処理
        /// </summary>
        void IMusicReactor.UpdateWithAudio(float audioTime) {
            if (!IsSetuped) return;

            // ノーツの座標更新
            UpdateActiveNotes(audioTime);

            // ノーツの追加
            if ((NextIndex < _notesCount)
            && (audioTime > _timingArray[NextIndex] - _preDuration)) {
                AddNote(_timingArray[NextIndex], _typeArray[NextIndex]);
                NextIndex++;
            }
        }


        void IMusicReactor.OnPause() {
            
        }

        void IMusicReactor.OnUnPause() {
        
        }

        void IMusicReactor.OnStop() {
            Teardown();
        }

        /// ----------------------------------------------------------------------------
        // Private Method （初期化処理）

        /// <summary>
        /// 補助線の生成
        /// </summary>
        private void CreateAxulayObjects() {

            // 現在のオブジェクトを破棄
            DeleteAxulayObjects();


            float delta = _oneBeetParam != 0 ? _oneBeetParam : throw new System.Exception($"{nameof(_oneBeetParam)}値が不正です");
            var hitParam = _hitPosParam;

            // 零点（※ヒット位置）
            InstantiateLine(_hitPosPrefab, hitParam);

            // 前方
            for (float p = hitParam - delta; p >= 0; p -= delta) {
                InstantiateLine(_linePrefab, p);
            }
            // 後方
            for (float p = hitParam + delta; p <= 1; p += delta) {
                InstantiateLine(_linePrefab, p);
            }

            ///
            void InstantiateLine(GameObject prefab, float param) {
                var lineObj = GameObject.Instantiate(prefab, _splineContainer.transform);
                lineObj.transform.position = GetSplinePos(param);
            }
        }

        /// <summary>
        /// 補助線の削除
        /// </summary>
        private void DeleteAxulayObjects() {
            foreach(var obj in _lineList) {
                obj.Destroy();
            }
            _lineList.Clear();
        }


        /// ----------------------------------------------------------------------------
        // Private Method 


        /// <summary>
        /// 表示状態のノーツの更新
        /// </summary>
        private void UpdateActiveNotes(float currentTime) {

            bool darty = false;
            foreach (var note in _activeNoteList) {

                // Audioの再生時間に対応したパラメータ変数
                var min = note.RaiseTime - _preDuration;
                var max = note.RaiseTime + _postDuration;
                var progress = math.remap(min, max, 0, 1, currentTime);

                // パス上の座標値に変換
                note.Progress = progress;

                var position = GetSplinePos(progress);
                var tangent = _splineContainer.EvaluateTangent(progress);
                var upVector = _splineContainer.EvaluateUpVector(progress);

                // 
                note.transform.SetPositionAndRotation(
                    position,
                    Quaternion.LookRotation(tangent, upVector)
                );

                if (progress >= 1) {
                    _pool.Return(note);
                    darty = true;
                }
            }

            // リストを更新
            if (darty) {
                _activeNoteList = _activeNoteList.Where(n => n.Progress < 1).ToList();
            }
        }

        /// <summary>
        /// ノーツを表示状態に切り替える
        /// </summary>
        private void AddNote(float timing, int type) {
            var note = _pool.Rent();
            note.Setup(new NoteData(timing, type));

            _activeNoteList.Add(note);
        }

        /// <summary>
        /// 指定パラメータ（値域:0~1）に対応したスプライン上の座標を取得
        /// </summary>
        private Vector3 GetSplinePos(float progress) {
            return _splineContainer.EvaluatePosition(progress);
        }

    }

}