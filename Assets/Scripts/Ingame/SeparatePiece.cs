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

        private void Start()
        {
        }
        public void Separate(GameObject Puzzle)
        {
            var pieceParent = Puzzle.GetComponent<PieceParent>();
            Piece = pieceParent.getObject();
            foreach (var item in Piece)
            {
                var info = item.GetComponent<PieceInfo>();
                List<GameObject> separeted = new List<GameObject>();
                if (!info.getIsSeparated)
                {
                    separeted = info.getNextPiece(3);
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
                        }
                    }
                    newParent.transform.SetParent(PieceArea.transform);
                    var pieceParentCs = newParent.GetComponent<PieceParent>();
                    pieceParentCs.setObject();
                    pieceParentCs.childPositionReset();
                }
            }
        }

        private Color randomColor()
        {
            int randomColorInt = 0;
            randomColorInt = Random.Range(0, 6);
            Color returnColor = default;
            switch (randomColorInt)
            {
                case 0:
                    returnColor = Color.red;
                    break;
                case 1:
                    returnColor = Color.blue;
                    break;
                case 2:
                    returnColor = Color.yellow;
                    break;
                case 3:
                    returnColor = Color.green;
                    break;
                case 4:
                    returnColor = Color.white;
                    break;
                case 5:
                    returnColor = Color.magenta;
                    break;
                default:
                    returnColor = Color.black;
                    break;
            }
            return returnColor;
        }
    }
}