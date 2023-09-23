using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class Droppable : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
        {
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // ドラッグしてきた情報からCardMovementを取得
            if (card != null) // もしカードがあれば、
            {
                card.canDrop = true;
            }
        }
    }
}
