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

        public virtual void OnDrop(PointerEventData eventData) // �h���b�v���ꂽ���ɍs������
        {
            var card = eventData.pointerDrag.GetComponent<PieceMovement>(); // �h���b�O���Ă�����񂩂�CardMovement���擾
            if (card != null) // �����J�[�h������΁A
            {
                Debug.Log(this.gameObject.name);
                card.canDrop = true;
                card.parentObject.transform.SetParent(_pieceSetArea.transform, false); // �J�[�h�̐e�v�f�������i�A�^�b�`����Ă�I�u�W�F�N�g�j�ɂ���
            }
        }
    }
}
