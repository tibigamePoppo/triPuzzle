using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class DropPlace : MonoBehaviour, IDropHandler
    {
        private GameObject _pieceSetArea;
        
        private void Start()
        {
            _pieceSetArea = GameObject.FindGameObjectWithTag("SetArea");
        }

        public virtual void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
        {
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // ドラッグしてきた情報からCardMovementを取得
            if (card != null) // もしカードがあれば、
            {
                Debug.Log(this.gameObject.name);
                card.canDrop = true;
                card.parentObject.transform.SetParent(_pieceSetArea.transform, false); // カードの親要素を自分（アタッチされてるオブジェクト）にする
            }
        }
    }
}
