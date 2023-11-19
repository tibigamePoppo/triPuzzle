using System.Collections.Generic;
using UnityEngine;
using System.Effect;
using Audio;

namespace Piece
{
    public class PieceParent : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> ChildPiece;
        public bool canDrop;
        public bool changeArea;
        public bool isArea = true;
        private CanvasGroup _canvasGroup;
        private Vector3 _prevPos;
        private Transform _prevParent;
        public Vector3 PiecePosition { get; private set; }
        public int selectedPieceRotation;

        private void Start()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        public List<GameObject> getObject()
        {
            return ChildPiece;
        }

        public void setObject()
        {
            var Children = gameObject.GetComponentsInChildren<Transform>();
            foreach (var item in Children)
            {
                ChildPiece.Add(item.gameObject);
            }
            ChildPiece.Remove(gameObject);
        }

        public void childPositionReset()
        {
            Vector3 ResetValue = Vector3.zero;
            foreach (var item in ChildPiece)
            {
                ResetValue += item.transform.position;
            }
            ResetValue = ResetValue / ChildPiece.Count;
            foreach (var item in ChildPiece)
            {
                item.transform.position -= ResetValue;
            }
        }

        private bool CheckCollider()
        {
            foreach (var item in ChildPiece)
            {
                //var hit1 = Physics2D.OverlapPoint(item.transform.position);
                var collider2D = item.GetComponent<Collider2D>();
                collider2D.enabled = false;
                var res = new Collider2D[5];
                var hit = Physics2D.OverlapPoint(item.transform.position, new ContactFilter2D(), res);
                collider2D.enabled = true;
                if (hit != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public void RotatePieces()
        {
            gameObject.transform.Rotate(0, 0, 270f);
            foreach (var piece in ChildPiece)
            {
                piece.GetComponent<PieceInfo>().RotatePiece();
            }
        }

        private void SetRotatable(bool able)
        {
            foreach (var piece in ChildPiece)
            {
                piece.GetComponent<PieceMovement>().canRotate = able;
            }
        }

        public void SetPrevParent(Vector3 prev, Vector3 correct)
        {
            _prevPos = prev;
            PiecePosition = correct;
            _prevParent = gameObject.transform.parent;
            _canvasGroup.blocksRaycasts = false;
            gameObject.transform.SetParent(_prevParent.parent, false);
        }

        public void PlacePiece()
        {
            var rotatable = false;
            if (canDrop && !isArea) canDrop = CheckCollider();
            
            if (isArea) rotatable = true;
            
            if (!canDrop)
            {
                if (changeArea) rotatable = true;
                SeManager.Instance.ShotSe(SeType.pieceError);
                ResetPosition();
            }
            else
            {
                SeManager.Instance.ShotSe(SeType.pieceSet);
                if (!isArea)
                {
                    if (Camera.main != null)
                    {
                        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition +
                                                                 Camera.main.transform.forward * 10);
                        var position = gameObject.transform.position;
                        pos = new Vector3(position.x, position.y, pos.z);
                        EffectManager.Instance.InstanceEffect(EffectType.PieceSet, pos);
                    }
                }
            }

            _canvasGroup.blocksRaycasts = true;
            changeArea = false;
            canDrop = false;
            SetRotatable(rotatable);
        }

        private void ResetPosition()
        {
            gameObject.transform.SetParent(_prevParent.transform, false);
            gameObject.transform.position = _prevPos;
        }
    }
}