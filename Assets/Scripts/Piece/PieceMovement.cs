using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Piece
{
    public class PieceMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public GameObject parentObject;
        public bool canDrop;
        public Vector3 PiecePosition { get; private set; }
        private CanvasGroup _canvasGroup;
        private Transform _prevParent;
        private Vector3 _prevPos;

        private void Start()
        {
            parentObject = transform.parent.gameObject;
            _canvasGroup = parentObject.GetComponent<CanvasGroup>();
            PiecePosition = gameObject.GetComponent<RectTransform>().position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // ドラッグ前の位置を記憶しておく
            _prevPos = parentObject.transform.position;
            // ピース全体の親オブジェクトを保存
            _prevParent = parentObject.transform.parent;
            _canvasGroup.blocksRaycasts = false;
            parentObject.transform.SetParent(_prevParent.parent, false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // ドラッグ中は位置を更新する
            parentObject.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            if (!canDrop)
            {
                ResetPosition();
            }
            _canvasGroup.blocksRaycasts = true;
            canDrop = false;
        }

        private void ResetPosition()
        {
            parentObject.transform.position = _prevPos;
            parentObject.transform.SetParent(_prevParent.transform, false);
        }
    }
}
