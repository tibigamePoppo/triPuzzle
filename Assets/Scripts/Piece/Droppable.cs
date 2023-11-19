using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class Droppable : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData) // �h���b�v���ꂽ���ɍs������
        {
            if (eventData.pointerDrag.transform.parent.TryGetComponent<PieceParent>(out var card)) // �����J�[�h������΁A
            {
                card.canDrop = true;
            }
        }
    }
}
