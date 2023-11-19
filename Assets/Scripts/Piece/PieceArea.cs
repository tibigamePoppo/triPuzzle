using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceArea : Droppable
    {
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            var cardTransform = eventData.pointerDrag.transform.parent;
            if (cardTransform.gameObject.TryGetComponent<PieceParent>(out var cardParent)) // もしカードがあれば、
            {
                cardTransform.SetParent(gameObject.transform, false);
                cardTransform.position = new Vector3(0f, 0f, 0f); // カードの座標を修正する
                cardParent.isArea = true;
            }
        }

        /// <summary>
        /// 使っていないピースがあるかを確認
        /// </summary>
        public bool isAllUsePiece()
        {
            int PieceCount = transform.childCount;
            if(PieceCount == 0)
            {
                return true;
            }    
            return false;
        }
    }
}
