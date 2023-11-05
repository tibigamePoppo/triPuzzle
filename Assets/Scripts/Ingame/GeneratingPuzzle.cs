using System.Collections.Generic;
using UnityEngine;
using Ingame;
using TMPro;
namespace Piece
{
    public class GeneratingPuzzle : MonoBehaviour
    {
        [Header("生成するお題のプレファブ")]
        [SerializeField, Tooltip("お題のPuzzleDataすべてを格納する")]
        private List<PuzzleData> _normalPuzzle;
        [SerializeField, Tooltip("お題のPuzzleDataすべてを格納する")]
        private List<PuzzleData> _hardPuzzle;
        private List<PuzzleData> _puzzleQueue;//格納されている順番にパズルを生成する
        private static int _currntPuzzleNumber = 0;
        [SerializeField, Tooltip("生成するパズルの親のオブジェクト")]
        private GameObject _puzzleParentObject;
        [SerializeField, Tooltip("パズルのタイトルテキスト")]
        private TextMeshProUGUI _puzzleTitle;
        private GameObject _generatedPuzzleObject = null;
        private static bool _initializePuzzleQueue = true;
        [SerializeField]
        SeparatePiece pieceCs;
        [SerializeField]
        BackGroundSet backGroundSet;
        private void Awake()
        {
            if(_initializePuzzleQueue)
            {
                PuzzleQueueBuild();
                _initializePuzzleQueue = false;
            }
        }

        public void Generate()
        {
            if (_normalPuzzle.Count == 0)
            {
                return;
            }
            _generatedPuzzleObject = Instantiate(_puzzleQueue[_currntPuzzleNumber].PuzzlePrefab, _puzzleParentObject.transform);
            var GeneratedPieceObject = Instantiate(_puzzleQueue[_currntPuzzleNumber].PuzzlePrefab, _puzzleParentObject.transform);
            _puzzleTitle.text = _puzzleQueue[_currntPuzzleNumber].PuzzleTitle;
            backGroundSet.setBackGround(_puzzleQueue[_currntPuzzleNumber]);
            pieceCs.Separate(GeneratedPieceObject);
            _currntPuzzleNumber++;
            if(_currntPuzzleNumber >= _puzzleQueue.Count)
            {
                _currntPuzzleNumber = 0;
                PuzzleQueueBuild();
            }
        }

        public void ReGenerate()
        {
            if (_generatedPuzzleObject == null)
            {
                Debug.LogError("GeneratedPuzzleObjectが設定されていません");
                return;
            }
            Destroy(_generatedPuzzleObject);
            _currntPuzzleNumber--;
            Generate();
        }
        public void PuzzleQueueBuild()
        {
            if(PlayerConfig.difficulty.Equals(Difficulty.Normal))
                _puzzleQueue = _normalPuzzle;
            else if (PlayerConfig.difficulty.Equals(Difficulty.Hard))
                _puzzleQueue = _hardPuzzle;
            for (int i = 0; i < _puzzleQueue.Count; i++)
            {
                var tempData = new PuzzleData();
                int randomInt = Random.Range(0, _puzzleQueue.Count);
                tempData = _puzzleQueue[i];
                _puzzleQueue[i] = _puzzleQueue[randomInt];
                _puzzleQueue[randomInt] = tempData;
            }
        }
    }
}