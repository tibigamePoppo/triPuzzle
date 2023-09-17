using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceSlot : Droppable
    {
        private GameObject _pieceSetArea;
        
        private void Start()
        {
            _pieceSetArea = GameObject.FindGameObjectWithTag("SetArea");
        }
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // ドラッグしてきた情報からCardMovementを取得
            if (card != null) // もしカードがあれば、
            {
                Debug.Log("位置の修正");
                card.parentObject.transform.SetParent(_pieceSetArea.transform, false);
                card.parentObject.transform.position = gameObject.transform.position - card.PiecePosition; // カードの座標を修正する
            }
        }
    }
}
