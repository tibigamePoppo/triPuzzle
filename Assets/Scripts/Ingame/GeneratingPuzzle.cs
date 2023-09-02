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
        private List<PuzzleData> Puzzle;
        [SerializeField, Tooltip("��������p�Y���̐e�̃I�u�W�F�N�g")]
        private GameObject PuzzleParentObject;
        [SerializeField, Tooltip("�p�Y���̃^�C�g���e�L�X�g")]
        private TextMeshProUGUI PuzzleTitle;
        private GameObject GeneratedPuzzleObject = null;
        int randomInt = 0;
        [SerializeField]
        SeparatePiece pieceCs;

        public void Generate(bool same)
        {
            if (Puzzle.Count == 0)
            {
                return;
            }
            if (!same) randomInt = Random.Range(0, Puzzle.Count);
            GeneratedPuzzleObject = Instantiate(Puzzle[randomInt].PuzzlePrefab, PuzzleParentObject.transform);
            var GeneratedPieceObject = Instantiate(Puzzle[randomInt].PuzzlePrefab, PuzzleParentObject.transform);
            PuzzleTitle.text = Puzzle[randomInt].PuzzleTitle;
            pieceCs.Separate(GeneratedPieceObject);
        }

        public void ReGenerate(bool same)
        {
            if (GeneratedPuzzleObject == null)
            {
                Debug.LogError("GeneratedPuzzleObject���ݒ肳��Ă��܂���");
                return;
            }
            Destroy(GeneratedPuzzleObject);
            Generate(same);
        }
    }
}