using System;
using System.Collections;
using Audio;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private GameObject _parentObject;
        private PieceParent _pieceParent;
        private GameObject _stateManager;
        private SlotRotationManager _slotRotationManager;
        public bool canRotate = true;
        private bool _isTap;
        private CanvasGroup _canvasGroup;
        private Transform _prevParent;
        private Vector3 _prevPos;
        private PieceInfo _pieceInfo;
        private Vector2 _piecePivot;
        private Vector3 _pivotCorrect;

        private void Start()
        {
            _parentObject = transform.parent.gameObject;
            _pieceParent = _parentObject.GetComponent<PieceParent>();
            _stateManager = GameObject.FindWithTag("Manager");
            _slotRotationManager = _stateManager.GetComponent<SlotRotationManager>();
            _pieceInfo = GetComponent<PieceInfo>();
            _piecePivot = (GetComponent<RectTransform>().pivot - new Vector2(0.5f, 0.5f)) * 2;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isTap = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isTap && canRotate)
            {
                SeManager.Instance.ShotSe(SeType.pieceRotate);
                _pieceParent.RotatePieces();
            }

            _isTap = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isTap = false;
            
            var rotation = _pieceInfo.getRotation;
            _pieceParent.selectedPieceRotation = rotation;
            _slotRotationManager.SendPieceRotation(rotation);
            
            var position = _parentObject.transform.position;
            var correctedPos = gameObject.transform.position - position;
            var rotate = Mathf.Deg2Rad * _parentObject.transform.localEulerAngles.z;
            _pivotCorrect = new Vector3(
                0.4f * (_piecePivot.x * Mathf.Cos(rotate) - _piecePivot.y * Mathf.Sin(rotate)),
                0.4f * (_piecePivot.y * Mathf.Cos(rotate) + _piecePivot.x * Mathf.Sin(rotate)),
                0);
            // ピース全体の親オブジェクトを保存
            _pieceParent.SetPrevParent(position, correctedPos, _piecePivot);
            
            SeManager.Instance.ShotSe(SeType.pieceRotate);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(Input.touchCount != 0)
            {
                var touchInfo = Input.GetTouch(0);
                eventData.position = touchInfo.position;
            }

            // ドラッグ中は位置を更新する
            if (Camera.main == null) return;
            var dragPos = Camera.main.ScreenToWorldPoint(eventData.position);
            dragPos.z = 0f;
            _parentObject.transform.position = dragPos - _pieceParent.PiecePosition + _pivotCorrect;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            StartCoroutine(CheckPlacePiece());
        }

        private IEnumerator CheckPlacePiece()
        {
            yield return StartCoroutine(_pieceParent.PlacePiece());
            _stateManager.GetComponent<IDropObservable>().CheckCompletePuzzle();
        }
    }
}
