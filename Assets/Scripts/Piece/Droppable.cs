using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class Droppable : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
        {
            if (eventData.pointerDrag.transform.parent.TryGetComponent<PieceParent>(out var card)) // もしカードがあれば、
            {
                card.canDrop = true;
            }
        }
    }
}
