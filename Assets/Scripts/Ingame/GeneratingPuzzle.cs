using System.Collections.Generic;
using UnityEngine;
using Ingame;
using TMPro;
using System;

namespace Piece
{
    public class GeneratingPuzzle : MonoBehaviour
    {
        [Header("生成するお題のプレファブ")]
        [SerializeField, Tooltip("お題のPuzzleDataすべてを格納する")]
        private List<PuzzleData> _normalPuzzle;
        [SerializeField, Tooltip("お題のPuzzleDataすべてを格納する")]
        private List<PuzzleData> _hardPuzzle;
        private static List<PuzzleData> _puzzleQueue;//格納されている順番にパズルを生成する
        private static int _currntPuzzleNumber = 0;
        [SerializeField, Tooltip("生成するパズルの親のオブジェクト")]
        private GameObject _puzzleParentObject;
        [SerializeField, Tooltip("パズルのタイトルテキスト")]
        private TextMeshProUGUI _puzzleTitle;
        private GameObject _generatedPuzzleObject = null;
        public static bool _initializePuzzleQueue = true;
        [SerializeField]
        private GameStateManager gameSateManager;
        [SerializeField]
        SeparatePiece pieceCs;
        [SerializeField]
        BackGroundSet backGroundSet;
        static int _reGenerateCount = 0;
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
            gameSateManager._puzzleTile = _puzzleQueue[_currntPuzzleNumber].PuzzleTitle;
            gameSateManager.puzzleImage = _puzzleQueue[_currntPuzzleNumber].PuzzleImage;
            _puzzleTitle.text = "???";
            backGroundSet.setBackGround(_puzzleQueue[_currntPuzzleNumber]);
            _currntPuzzleNumber++;
            pieceCs.Separate(GeneratedPieceObject);
            if(_currntPuzzleNumber >= _puzzleQueue.Count)
            {
                _currntPuzzleNumber = 0;
                PuzzleQueueBuild();
            }
        }

        public void ReGenerate()
        {
            _reGenerateCount++;
            if (_reGenerateCount > 10)
            {
                _reGenerateCount = 0;
                return;
            }
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
            _currntPuzzleNumber = 0;
            if (PlayerConfig.difficulty.Equals(Difficulty.Normal))
            {
                pieceCs.MaxSinglePieceCount = 3;
                pieceCs.separatePieceSize = 3;
                _puzzleQueue = _normalPuzzle;
            }
            else if (PlayerConfig.difficulty.Equals(Difficulty.Hard))
            {
                pieceCs.MaxSinglePieceCount = 10;
                pieceCs.separatePieceSize = 5;
                _puzzleQueue = _hardPuzzle;
            }
            for (int i = 0; i < _puzzleQueue.Count; i++)
            {
                var tempData = new PuzzleData();
                int randomInt = UnityEngine.Random.Range(0, _puzzleQueue.Count);
                tempData = _puzzleQueue[i];
                _puzzleQueue[i] = _puzzleQueue[randomInt];
                _puzzleQueue[randomInt] = tempData;
            }
        }
    }
}