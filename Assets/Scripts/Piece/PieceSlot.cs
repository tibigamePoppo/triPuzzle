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
            card.gameObject.transform.position = new Vector3(0, 0, 0); // �J�[�h�̍��W���C������
        }
    }
}
