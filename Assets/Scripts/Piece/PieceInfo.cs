using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Piece
{
    public class PieceInfo : MonoBehaviour
    {
        [SerializeField, Tooltip("�אڂ���s�[�X")]
        private List<GameObject> nextPiece;
        [SerializeField]
        private int Rotation;
        private bool isSeparated = false;

        public int getRotation { get => Rotation; }
        public bool getIsSeparated { get => isSeparated; }
        private int loopMax = 10;

        private void Start()
        {
            if (nextPiece.Count == 0)
            {
                Debug.LogError("nextPiece�̐���0�ł�");
            }
        }
        private void Separeted()
        {
            isSeparated = true;
        }

        /// <summary>
        /// ��������Ă��Ȃ��אڂ���s�[�X��߂�l�Ƃ��ĕԂ�
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
                var tempNextPiece = nextPiece[nextValue];           //�ׂ̃s�[�X�������_���őI������
                if(tempNextPiece != null)
                {
                    if (tempNextPiece.TryGetComponent(out PieceInfo infoCs))
                    {
                        //�I�����Ă���I�u�W�F�N�g����������Ă��Ȃ��߂�l�Ƃ��ĕԂ�
                        if (!infoCs.getIsSeparated)
                        {
                            if (count != 0)
                            {
                                //Debug.Log($"Count���c���Ă��邽�ߗאڃs�[�X��ǉ�:{count}");
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
                    Debug.LogWarning("loopMax�ɓ��B");
                    return returnPiece;
                }
            }
        }
    }
}