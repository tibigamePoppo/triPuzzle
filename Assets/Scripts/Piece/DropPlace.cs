using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class DropPlace : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData) // �h���b�v���ꂽ���ɍs������
        {
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // �h���b�O���Ă�����񂩂�CardMovement���擾
            if (card != null) // �����J�[�h������΁A
            {
                card.pieceParent = this.transform; // �J�[�h�̐e�v�f�������i�A�^�b�`����Ă�I�u�W�F�N�g�j�ɂ���
            }
        }
    }
}
