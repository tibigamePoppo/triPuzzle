using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public GameObject ParentObject { get; private set; }
        private PieceParent _pieceParent;
        private GameStateManager stateManager;
        public bool canDrop;
        public bool isArea;
        public Vector3 PiecePosition { get; private set; }
        private CanvasGroup _canvasGroup;
        private Transform _prevParent;
        private Vector3 _prevPos;
        private int _pieceNum;
        public int toPieceNum;

        private void Start()
        {
            ParentObject = transform.parent.gameObject;
            _pieceParent = ParentObject.GetComponent<PieceParent>();
            _canvasGroup = ParentObject.GetComponent<CanvasGroup>();
            PiecePosition = gameObject.GetComponent<RectTransform>().position;
            stateManager = FindObjectOfType<GameStateManager>();
            _pieceNum = GetComponent<PieceSlot>().GetPieceNum();
            toPieceNum = _pieceNum;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            toPieceNum = _pieceNum;
            // �h���b�O�O�̈ʒu���L�����Ă���
            _prevPos = ParentObject.transform.position;
            // �s�[�X�S�̂̐e�I�u�W�F�N�g��ۑ�
            _prevParent = ParentObject.transform.parent;
            _canvasGroup.blocksRaycasts = false;
            ParentObject.transform.SetParent(_prevParent.parent, false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Touch touchInfo;
            if(Input.touchCount != 0)
            {
                touchInfo = Input.GetTouch(0);
                eventData.position = touchInfo.position;
                Debug.Log("Touch");
            }

            // �h���b�O���͈ʒu���X�V����
            if (Camera.main == null) return;
            var dragPos = Camera.main.ScreenToWorldPoint(eventData.position) - PiecePosition;
            dragPos.z = 0f;
            ParentObject.transform.position = dragPos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            CheckDroppable();
            if (!canDrop)
            {
                ResetPosition();
            }
            _canvasGroup.blocksRaycasts = true;
            canDrop = false;
            isArea = false;
            stateManager.DropPiece.Invoke();
        }

        private void ResetPosition()
        {
            ParentObject.transform.SetParent(_prevParent.transform, false);
            ParentObject.transform.position = _prevPos;
        }

        private void CheckDroppable()
        {
            if (canDrop)
            {
                canDrop = _pieceNum == toPieceNum;
            }
            if (canDrop && !isArea)
            {
                canDrop = _pieceParent.CheckCollider();
            }
        }
    }
}
