using System.Collections.Generic;
using UnityEngine;
namespace Piece
{
    public class PieceParent : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> ChildPiece;

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

        public bool CheckCollider()
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
    }
}