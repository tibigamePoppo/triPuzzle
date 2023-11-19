using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceSlot : Droppable
    {
        private GameObject _pieceSetArea;
        private PieceInfo _pieceInfo;

        private void Start()
        {
            _pieceSetArea = GameObject.FindGameObjectWithTag("SetArea");
            _pieceInfo = GetComponent<PieceInfo>();
        }
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            var cardTransform = eventData.pointerDrag.transform.parent; // ドラッグしてきた情報からCardMovementを取得
            if (cardTransform.gameObject.TryGetComponent<PieceParent>(out var cardParent)) // もしカードがあれば、
            {
                if (_pieceInfo.getRotation == cardParent.selectedPieceRotation)
                {
                    //card.toPieceRotation = _pieceInfo.getRotation;
                    cardTransform.SetParent(_pieceSetArea.transform, false);
                    cardTransform.position = gameObject.transform.position - cardParent.PiecePosition - new Vector3(0f, 0f, 1f); // カードの座標を修正する
                    if (cardParent.isArea) cardParent.changeArea = true;
                    cardParent.isArea = false;
                }
                else cardParent.canDrop = false;
            }
        }
    }
}
