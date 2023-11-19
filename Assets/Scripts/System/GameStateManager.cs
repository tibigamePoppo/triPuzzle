using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Piece;
using System.Effect;
using TMPro;
using Audio;

namespace System
{
    public enum GameState
    {
        SetUp,
        InGame,
        Complete,
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
        [SerializeField, Tooltip("パズルのタイトルテキスト")]
        private TextMeshProUGUI _puzzleTitleText;
        [Header("パズル完成UI")]
        [SerializeField, Tooltip("パズルのタイトルe")]
        private TextMeshProUGUI __puzzleTitle;
        [SerializeField, Tooltip("パズルの完成イメージ")]
        private Image __puzzleImage;
        [SerializeField, Tooltip("パズルの完成時に表示するオブジェクト一覧")]
        private GameObject _puzzleCompleteObject;
        [HideInInspector]
        public string _puzzleTile;
        [HideInInspector]
        public Sprite puzzleImage;
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
                    _puzzleCompleteObject.SetActive(false);
                    GeneratingPuzzleCs.Generate();
                    ChangeState(GameState.InGame);
                    break;
                case GameState.InGame:
                    break;
                case GameState.Complete:
                    __puzzleTitle.text = _puzzleTile;
                    __puzzleImage.sprite = puzzleImage;
                    _puzzleTitleText.text = _puzzleTile;
                    SeManager.Instance.ShotSe(SeType.complete);
                    EffectManager.Instance.InstanceEffect(EffectType.CompleteParty, Vector3.zero);
                    _puzzleCompleteObject.SetActive(true);
                    shadow.SetActive(true);
                    break;
                case GameState.Result:
                    _puzzleCompleteObject.SetActive(false);
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
            yield return new WaitForSeconds(2f);
            ChangeState(GameState.Complete);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Input.anyKey);
            ChangeState(GameState.Result);
        }
    }
}