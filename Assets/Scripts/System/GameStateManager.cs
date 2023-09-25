using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piece;
using System.Effect;

namespace System
{
    public enum GameState
    {
        SetUp,
        InGame,
        Result,
    }
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField]
        GeneratingPuzzle GeneratingPuzzleCs;

        [SerializeField] private GameObject shadow;
        [SerializeField]
        GameObject ResultObject;
        private PieceArea pieceArea;
        [SerializeField]
        Transform PazzlePosition;
        [SerializeField]
        private List<GameObject> ResetChildObjects;
        public Action DropPiece;
        private GameState CurrentState = GameState.SetUp;
        [SerializeField] private GameObject[] hidePiece;
        void Start()
        {
            pieceArea = FindObjectOfType<PieceArea>();
            ChangeState(GameState.SetUp);
            DropPiece = CheckCompletePuzzle;
        }

        public void ChangeState(GameState state)
        {
            CurrentState = state;
            switch (state)
            {
                case GameState.SetUp:
                    shadow.SetActive(false);
                    ResultObject.SetActive(false);
                    GeneratingPuzzleCs.Generate(false);
                    ChangeState(GameState.InGame);
                    break;
                case GameState.InGame:
                    break;
                case GameState.Result:
                    shadow.SetActive(true);
                    ResultObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        private void CheckCompletePuzzle()
        {
            if(pieceArea.isAllUsePiece())
            {
                Debug.Log("すべてのピースを使用しました");
                EffectManager.Instance.InstanceEffect(EffectType.CompletePuzzle, PazzlePosition.position);
                StartCoroutine(goResult());
            }
        }

        IEnumerator goResult()
        {
            yield return new WaitForSeconds(1.5f);
            FindObjectOfType<CompleteImage>().ShowImage();
            foreach (var o in hidePiece)
            {
                o.SetActive(false);
            }
            yield return new WaitForSeconds(2.8f);
            ChangeState(GameState.Result);
        }
    }
}