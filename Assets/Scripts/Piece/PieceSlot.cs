using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Piece
{
    public class PieceSlot : Droppable
    {
        private GameObject _pieceSetArea;
        private PieceInfo _pieceInfo;
        private SlotRotationManager _slotRotationManager;

        private void Start()
        {
            _pieceSetArea = GameObject.FindGameObjectWithTag("SetArea");
            _slotRotationManager = FindObjectOfType<SlotRotationManager>();
            _pieceInfo = GetComponent<PieceInfo>();

            if (_pieceInfo.getRotatable)
                _slotRotationManager.BeginDragPiece.Subscribe(piece =>
                {
                    if ((piece + _pieceInfo.getRotation) % 180 != 0) RotatePiece();
                }).AddTo(this);
            if (gameObject.TryGetComponent(out Collider2D component))
            {
                component.enabled = false;
            }
        }
        
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            var cardTransform = eventData.pointerDrag.transform.parent; // ドラッグしてきた情報からCardMovementを取得
            if (cardTransform.gameObject.TryGetComponent<PieceParent>(out var cardParent)) // もしカードがあれば、
            {
                if (_pieceInfo.getRotation == cardParent.selectedPieceRotation)
                {
                    //card.toPieceRotation = _pieceInfo.getRotation;
                    cardTransform.SetParent(_pieceSetArea.transform, false);
                    cardTransform.position = gameObject.transform.position - cardParent.PiecePosition - new Vector3(0f, 0f, 1f); // カードの座標を修正する
                    if (cardParent.isArea) cardParent.changeArea = true;
                    cardParent.isArea = false;
                }
                else cardParent.canDrop = false;
            }
        }

        private void RotatePiece()
        {
            gameObject.transform.Rotate(0, 0, 270f);
            _pieceInfo.RotatePiece();
        }
    }
}
