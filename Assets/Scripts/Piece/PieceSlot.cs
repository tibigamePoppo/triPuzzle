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
        var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // ドラッグしてきた情報からCardMovementを取得
        if (card != null) // もしカードがあれば、
        {
            Debug.Log("位置の修正");
            card.parentObject.transform.position = gameObject.transform.position - card.PiecePosition; // カードの座標を修正する
        }
    }
}
