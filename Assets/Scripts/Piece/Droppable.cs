using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class Droppable : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData) // �h���b�v���ꂽ���ɍs������
        {
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // �h���b�O���Ă�����񂩂�CardMovement���擾
            if (card != null) // �����J�[�h������΁A
            {
                card.canDrop = true;
            }
        }
    }
}
