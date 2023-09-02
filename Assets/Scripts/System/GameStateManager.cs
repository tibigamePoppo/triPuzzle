using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piece;

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
        private GameState CurrentState = GameState.SetUp;
        void Start()
        {
            ChangeState(GameState.SetUp);
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
    }
}