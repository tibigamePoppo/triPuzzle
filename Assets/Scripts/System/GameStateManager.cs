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
        [SerializeField]
        GameObject ResultObject;
        private PieceArea pieceArea;
        [SerializeField]
        Transform PazzlePosition;
        [SerializeField]
        private List<GameObject> ResetChildObjects;
        public Action DropPiece;
        private GameState CurrentState = GameState.SetUp;
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
                    ResultObject.SetActive(false);
                    GeneratingPuzzleCs.Generate(false);
                    ChangeState(GameState.InGame);
                    break;
                case GameState.InGame:
                    break;
                case GameState.Result:
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
            yield return new WaitForSeconds(3f);
            ChangeState(GameState.Result);
        }
    }
}