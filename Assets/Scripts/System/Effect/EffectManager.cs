using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Effect
{
    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance { get; private set; }
        [SerializeField]
        private GameObject FadeIn;
        [SerializeField]
        private GameObject FadeOut;
        [SerializeField]
        private GameObject PieceSetEffect;
        [SerializeField]
        private GameObject CompletePuzzleEffect;
        [SerializeField]
        private GameObject CompletePartyEffect;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void InstanceEffect(EffectType type,Vector3 InstancePosition)
        {
            switch (type)
            {
                case EffectType.FadeIn:
                    Instantiate(FadeIn, transform);
                    break;
                case EffectType.FadeOut:
                    Instantiate(FadeOut, transform);
                    break;
                case EffectType.ScreenTap:
                    break;
                case EffectType.PieceSet:
                    var PieceSet = Instantiate(PieceSetEffect, InstancePosition,Quaternion.identity);
                    Destroy(PieceSet, 3f);
                    break;
                case EffectType.CompletePuzzle:
                    var CompletePuzzle = Instantiate(CompletePuzzleEffect, InstancePosition, Quaternion.identity);
                    Destroy(CompletePuzzle, 3f);
                    break;
                case EffectType.CompleteParty:
                    var CompleteParty = Instantiate(CompletePartyEffect, InstancePosition, Quaternion.identity);
                    //Destroy(CompleteParty, 4f);
                    break;
                default:
                    break;
            }
        }
        //SeManager.Instance.ShotSe(SeType.MoveCard);と入力すると音が出る?
    }
}
