using Piece;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceSlot : DropPlace
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // �h���b�O���Ă�����񂩂�CardMovement���擾
        if (card != null) // �����J�[�h������΁A
        {
            Debug.Log("�ʒu�̏C��");
            card.parentObject.transform.position = gameObject.transform.position - card.PiecePosition; // �J�[�h�̍��W���C������
        }
    }
}
