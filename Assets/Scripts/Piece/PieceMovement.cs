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
            // �h���b�O�O�̈ʒu���L�����Ă���
            //_prevPos = transform.position;

            pieceParent = transform.parent;
            transform.SetParent(pieceParent.parent, false);
            GetComponent<Image>().raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // �h���b�O���͈ʒu���X�V����
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // �h���b�O�O�̈ʒu�ɖ߂�
            //transform.position = _prevPos;
            
            transform.SetParent(pieceParent, false);
            GetComponent<Image>().raycastTarget = true;
        }
    }
}
