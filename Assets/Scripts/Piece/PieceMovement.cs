using System;
using Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private GameObject _parentObject;
        private PieceParent _pieceParent;
        private GameStateManager _stateManager;
        public bool canRotate = true;
        private bool _isTap;
        private CanvasGroup _canvasGroup;
        private Transform _prevParent;
        private Vector3 _prevPos;
        private PieceInfo _pieceInfo;

        private void Start()
        {
            _parentObject = transform.parent.gameObject;
            _pieceParent = _parentObject.GetComponent<PieceParent>();
            _stateManager = FindObjectOfType<GameStateManager>();
            _pieceInfo = GetComponent<PieceInfo>();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isTap = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isTap && canRotate)
            {
                Debug.Log("回転する");
                _pieceParent.RotatePieces();
            }

            _isTap = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isTap = false;
            _pieceParent.selectedPieceRotation = _pieceInfo.getRotation;
            var position = _parentObject.transform.position;
            var correctedPos = gameObject.transform.position - position;
            // ピース全体の親オブジェクトを保存
            _pieceParent.SetPrevParent(position, correctedPos);
            SeManager.Instance.ShotSe(SeType.pieceRotate);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(Input.touchCount != 0)
            {
                var touchInfo = Input.GetTouch(0);
                eventData.position = touchInfo.position;
                Debug.Log("Touch");
            }

            // ドラッグ中は位置を更新する
            if (Camera.main == null) return;
            var dragPos = Camera.main.ScreenToWorldPoint(eventData.position);
            dragPos.z = 0f;
            _parentObject.transform.position = dragPos - _pieceParent.PiecePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _pieceParent.PlacePiece();
            _stateManager.DropPiece.Invoke();
        }
    }
}
