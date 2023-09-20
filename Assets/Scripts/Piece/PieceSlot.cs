using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceSlot : Droppable
    {
        [SerializeField] private int pieceNum;
        private GameObject _pieceSetArea;

        public int GetPieceNum() { return pieceNum;} 
        
        private void Start()
        {
            _pieceSetArea = GameObject.FindGameObjectWithTag("SetArea");
        }
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // ドラッグしてきた情報からCardMovementを取得
            if (card != null) // もしカードがあれば、
            {
                Debug.Log("位置の修正");
                card.toPieceNum = pieceNum;
                card.ParentObject.transform.SetParent(_pieceSetArea.transform, false);
                card.ParentObject.transform.position = gameObject.transform.position - card.PiecePosition - new Vector3(0f, 0f, 1f); // カードの座標を修正する
                card.isArea = false;
            }
        }
    }
}
