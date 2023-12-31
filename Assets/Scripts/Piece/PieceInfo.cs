using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Piece
{
    public class PieceInfo : MonoBehaviour
    {
        [SerializeField, Tooltip("隣接するピース")]
        private List<GameObject> nextPiece;
        [SerializeField]
        private int Rotation;
        [SerializeField] 
        private bool slotRotatable;
        private bool isSeparated = false;

        public int getRotation => Rotation;
        public bool getRotatable => slotRotatable;
        public bool getIsSeparated => isSeparated;
        private int loopMax = 10;

        private void Start()
        {
            if (nextPiece.Count == 0)
            {
                Debug.LogError("nextPieceの数が0個です");
            }
            //ReflectRotation();
        }

        public void RotatePiece()
        {
            Rotation += 270;
            ReflectRotation();
        }

        public void InvertPiece()
        {
            Rotation += 180;
            ReflectRotation();
        }

        private void ReflectRotation()
        {
            Rotation %= 360;
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, -90 * Rotation);
        }
        
        private void Separeted()
        {
            isSeparated = true;
        }

        /// <summary>
        /// 分割されていない隣接するピースを戻り値として返す
        /// </summary>
        public List<GameObject> getNextPiece(int count)
        {
            int loop = 0;
            while (true)
            {
                loop++;
                List<GameObject> returnPiece = new List<GameObject>();
                returnPiece.Add(this.gameObject);
                Separeted();

                var nextValue = Random.Range(0, nextPiece.Count);
                var tempNextPiece = nextPiece[nextValue];           //隣のピースをランダムで選択する
                if(tempNextPiece != null)
                {
                    if (tempNextPiece.TryGetComponent(out PieceInfo infoCs))
                    {
                        //選択しているオブジェクトが分割されていない戻り値として返す
                        if (!infoCs.getIsSeparated)
                        {
                            if (count != 0)
                            {
                                //Debug.Log($"Countが残っているため隣接ピースを追加:{count}");
                                count--;
                                var nextPiece = infoCs.getNextPiece(count);
                                foreach (var item in nextPiece)
                                {
                                    returnPiece.Add(item);
                                }
                            }
                            return returnPiece;
                        }
                    }
                }
                if (loop > loopMax)
                {
                    return returnPiece;
                }
            }
        }
    }
}