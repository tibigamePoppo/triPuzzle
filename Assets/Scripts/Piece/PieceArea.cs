using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceArea : Droppable
    {
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // ドラッグしてきた情報からCardMovementを取得
            if (card != null) // もしカードがあれば、
            {
                Debug.Log("位置の修正");
                card.ParentObject.transform.SetParent(gameObject.transform, false);
                card.ParentObject.transform.position = new Vector3(0f, 0f, 0f); // カードの座標を修正する
            }
        }
    }
}
