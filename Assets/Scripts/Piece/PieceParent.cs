using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Piece
{
    public class PieceParent : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> ChildPiece;

        private const int LayerSettingPiece = 6;
        private const int LayerMovingPiece = 7;

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
                var col2D = item.GetComponent<Collider2D>();
                var count = col2D.OverlapCollider(new ContactFilter2D(), new Collider2D[5]);
                Debug.Log("d‚È‚Á‚½”F"+count);
                if (count > 0) return false;
            }
            return true;
        }

        public void BeginMove()
        {
            foreach (var item in ChildPiece)
            {
                item.layer = LayerMovingPiece;
            }
        }

        public void EndMove()
        {
            foreach (var item in ChildPiece)
            {
                item.layer = LayerSettingPiece;
            }
        }
    }
}