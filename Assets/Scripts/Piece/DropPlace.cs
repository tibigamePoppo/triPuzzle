using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class DropPlace : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
        {
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // ドラッグしてきた情報からCardMovementを取得
            if (card != null) // もしカードがあれば、
            {
                card.pieceParent = this.transform; // カードの親要素を自分（アタッチされてるオブジェクト）にする
            }
        }
    }
}
