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
        private List<PuzzleData> Puzzle;
        [SerializeField, Tooltip("生成するパズルの親のオブジェクト")]
        private GameObject PuzzleParentObject;
        [SerializeField, Tooltip("パズルのタイトルテキスト")]
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
                Debug.LogError("GeneratedPuzzleObjectが設定されていません");
                return;
            }
            Destroy(GeneratedPuzzleObject);
            Generate(same);
        }
    }
}