using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceArea : Droppable
    {
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // �h���b�O���Ă�����񂩂�CardMovement���擾
            if (card != null) // �����J�[�h������΁A
            {
                Debug.Log("�ʒu�̏C��");
                card.ParentObject.transform.SetParent(gameObject.transform, false);
                card.ParentObject.transform.position = new Vector3(0f, 0f, 0f); // �J�[�h�̍��W���C������
            }
        }
    }
}
