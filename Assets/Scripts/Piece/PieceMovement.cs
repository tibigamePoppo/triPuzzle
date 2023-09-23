using System;
using System.Effect;
using Audio;
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
            // ドラッグ前の位置を記憶しておく
            _prevPos = ParentObject.transform.position;
            // ピース全体の親オブジェクトを保存
            _prevParent = ParentObject.transform.parent;
            _canvasGroup.blocksRaycasts = false;
            ParentObject.transform.SetParent(_prevParent.parent, false);
            SeManager.Instance.ShotSe(SeType.pieceRotate);
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

            // ドラッグ中は位置を更新する
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
                SeManager.Instance.ShotSe(SeType.pieceError);
                ResetPosition();
            }
            else
            {
                SeManager.Instance.ShotSe(SeType.pieceSet);
                if (!isArea)
                {
                    var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward * 10);
                    EffectManager.Instance.InstanceEffect(EffectType.PieceSet, pos);
                }
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
