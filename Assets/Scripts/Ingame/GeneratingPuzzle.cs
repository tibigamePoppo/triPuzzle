using System.Collections.Generic;
using UnityEngine;
using Ingame;
using TMPro;
using System;

namespace Piece
{
    public class GeneratingPuzzle : MonoBehaviour
    {
        [Header("�������邨��̃v���t�@�u")]
        [SerializeField, Tooltip("�����PuzzleData���ׂĂ��i�[����")]
        private List<PuzzleData> _normalPuzzle;
        [SerializeField, Tooltip("�����PuzzleData���ׂĂ��i�[����")]
        private List<PuzzleData> _hardPuzzle;
        private static List<PuzzleData> _puzzleQueue;//�i�[����Ă��鏇�ԂɃp�Y���𐶐�����
        private static int _currntPuzzleNumber = 0;
        [SerializeField, Tooltip("��������p�Y���̐e�̃I�u�W�F�N�g")]
        private GameObject _puzzleParentObject;
        [SerializeField, Tooltip("�p�Y���̃^�C�g���e�L�X�g")]
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
                Debug.LogError("GeneratedPuzzleObject���ݒ肳��Ă��܂���");
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