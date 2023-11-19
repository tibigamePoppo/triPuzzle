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
            if (cardTransform.gameObject.TryGetComponent<PieceParent>(out var cardParent)) // �����J�[�h������΁A
            {
                cardTransform.SetParent(gameObject.transform, false);
                cardTransform.position = new Vector3(0f, 0f, 0f); // �J�[�h�̍��W���C������
                cardParent.isArea = true;
            }
        }

        /// <summary>
        /// �g���Ă��Ȃ��s�[�X�����邩���m�F
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
