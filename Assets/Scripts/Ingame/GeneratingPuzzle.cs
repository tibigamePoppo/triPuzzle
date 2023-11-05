using System.Collections.Generic;
using UnityEngine;
using Ingame;
using TMPro;
namespace Piece
{
    public class GeneratingPuzzle : MonoBehaviour
    {
        [Header("�������邨��̃v���t�@�u")]
        [SerializeField, Tooltip("�����PuzzleData���ׂĂ��i�[����")]
        private List<PuzzleData> _normalPuzzle;
        [SerializeField, Tooltip("�����PuzzleData���ׂĂ��i�[����")]
        private List<PuzzleData> _hardPuzzle;
        private List<PuzzleData> _puzzleQueue;//�i�[����Ă��鏇�ԂɃp�Y���𐶐�����
        private static int _currntPuzzleNumber = 0;
        [SerializeField, Tooltip("��������p�Y���̐e�̃I�u�W�F�N�g")]
        private GameObject _puzzleParentObject;
        [SerializeField, Tooltip("�p�Y���̃^�C�g���e�L�X�g")]
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
                Debug.LogError("GeneratedPuzzleObject���ݒ肳��Ă��܂���");
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