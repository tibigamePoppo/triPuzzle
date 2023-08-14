using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Piece
{
    public class PieceMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Transform pieceParent;
        private Vector3 _prevPos;
        public void OnBeginDrag(PointerEventData eventData)
        {
            // ドラッグ前の位置を記憶しておく
            //_prevPos = transform.position;

            pieceParent = transform.parent;
            transform.SetParent(pieceParent.parent, false);
            GetComponent<Image>().raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // ドラッグ中は位置を更新する
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // ドラッグ前の位置に戻す
            //transform.position = _prevPos;
            
            transform.SetParent(pieceParent, false);
            GetComponent<Image>().raycastTarget = true;
        }
    }
}
