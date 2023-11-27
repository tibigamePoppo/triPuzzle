using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Piece
{
    public class SeparatePiece : MonoBehaviour
    {
        private List<GameObject> Piece;
        [SerializeField]
        private GameObject PieceParent;
        [SerializeField]
        private GameObject PieceArea;
        public int MaxSinglePieceCount = 3;
        public int separatePieceSize = 3;
        GeneratingPuzzle generatingPuzzle;
        int pieceSize;

        private void Start()
        {
            generatingPuzzle = GetComponent<GeneratingPuzzle>();
        }
        public void Separate(GameObject Puzzle)
        {
            pieceSize = 0;
            var pieceParent = Puzzle.GetComponent<PieceParent>();
            Piece = pieceParent.getObject();
            foreach (var item in Piece)
            {
                var info = item.GetComponent<PieceInfo>();
                List<GameObject> separeted = new List<GameObject>();
                if (!info.getIsSeparated)
                {
                    separeted = info.getNextPiece(separatePieceSize);
                }
                if (separeted.Count != 0)
                {
                    var newParent = Instantiate(PieceParent, transform.transform.position, Quaternion.identity);
                    Color pieceColor = randomColor();
                    foreach (var separeteitem in separeted)
                    {
                        separeteitem.transform.SetParent(newParent.transform);
                        if (separeteitem.TryGetComponent(out PiecePresenter Presenter))
                        {
                            Presenter.ColorChange(pieceColor);
                            separeteitem.AddComponent<PieceMovement>();
                            Destroy(separeteitem.GetComponent<PieceSlot>());
                            separeteitem.GetComponent<Collider2D>().enabled = true;
                        }
                    }
                    newParent.transform.SetParent(PieceArea.transform);
                    var pieceParentCs = newParent.GetComponent<PieceParent>();
                    pieceParentCs.setObject();
                    pieceParentCs.childPositionReset();

                    var rnd = Random.Range(0, 4);
                    for (var i = 0; i < rnd; i++)
                    {
                        pieceParentCs.RotatePieces();
                    }

                    if (newParent.transform.childCount.Equals(1))
                        pieceSize++;
                    if (pieceSize >= MaxSinglePieceCount)
                    {
                        Debug.Log($"ピースの再生成を行いました");
                        //generatingPuzzle.ReGenerate();
                    }
                }
            }
        }

        private Color randomColor()
        {
            int randomColorInt = 0;
            randomColorInt = Random.Range(0, 10);
            Color returnColor = default;
            switch (randomColorInt)
            {
                case 0:
                    returnColor = new Color(233f / 255f, 166f / 255f, 172f / 255f);
                    break;
                case 1:
                    returnColor = new Color(247f / 255f, 188f / 255f, 163f / 255f);
                    break;
                case 2:
                    returnColor = new Color(245f / 255f, 220f / 255f, 154f / 255f);
                    break;
                case 3:
                    returnColor = new Color(180f / 255f, 221f / 255f, 144f / 255f);
                    break;
                case 4:
                    returnColor = new Color(156f / 255f, 193f / 255f, 225f / 255f);
                    break;
                case 5:
                    returnColor = new Color(228f / 255f, 168f / 255f, 192f / 255f);
                    break;
                case 6:
                    returnColor = new Color(170f / 255f, 255f / 255f, 255f / 255f);
                    break;
                case 7:
                    returnColor = new Color(221f / 255f, 255f / 255f, 170f / 255f);
                    break;
                case 8:
                    returnColor = new Color(204f / 255f, 221f / 255f, 255f / 255f);
                    break;
                case 9:
                    returnColor = new Color(221f / 255f, 187f / 255f, 255f / 255f);
                    break;
                default:
                    returnColor = new Color(233f / 255f, 166f / 255f, 172f / 255f);
                    break;
            }
            return returnColor;
        }
    }
}