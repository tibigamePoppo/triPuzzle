using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using TMPro;
using Piece;

namespace Ingame.Tutorial
{
    public enum TutorialStage
    {
        Stage1 = 0,
        Stage2 = 1,
        Stage3 = 2,
        Stage4 = 3,
        Stage5 = 4,
        Stage6 = 5,
    }
    public class TutorialManager : MonoBehaviour
    {
        ReactiveProperty<TutorialStage> _tutorialStage = new ReactiveProperty<TutorialStage>();
        public IObservable<TutorialStage> tutorialStage//_currentHpの変化があったときに発行される
        {
            get { return _tutorialStage; }
        }
        [SerializeField, Tooltip("お題のPuzzleDataすべてを格納する")]
        private PuzzleData _Puzzle;
        [SerializeField, Tooltip("生成するパズルの親のオブジェクト")]
        private GameObject _puzzleParentObject;
        [SerializeField, Tooltip("パズルのタイトルテキスト")]
        private TextMeshProUGUI _puzzleTitle;
        [SerializeField]
        BackGroundSet backGroundSet;
        [SerializeField]
        List<GameObject> testPiece;
        [SerializeField]
        private GameObject _resultPanel;
        void Start()
        {
            foreach (var item in testPiece)
            {
                item.SetActive(false);
            }
            tutorialStage
                .Subscribe(stage =>
                {
                    switch (stage)
                    {
                        case TutorialStage.Stage1://左のピースの説明
                            StartCoroutine(NextClick());
                        break;
                        case TutorialStage.Stage2://右のパズルの説明
                            StartCoroutine(NextClick());
                            break;
                        case TutorialStage.Stage3://ピースをパズルに当てはめる説明
                            foreach (var item in testPiece)
                            {
                                item.SetActive(true);
                            }
                            Generate();
                            StartCoroutine(NextClick());
                            break;
                        case TutorialStage.Stage4://型と違うピースが置けない説明
                            StartCoroutine(NextClick());
                            break;
                        case TutorialStage.Stage5://すべてのピースがはまるとパズルがクリアできる説明
                            StartCoroutine(NextClick());
                            break;
                        case TutorialStage.Stage6://出来上がってイラストを見てチュートリアル終了
                            _resultPanel.SetActive(true);
                        break;
                        default:
                            break;
                    }
                }).AddTo(this);
        }
        private void NextStage()
        {
            _tutorialStage.Value++;
        }

        public void Generate()
        {
            var _generatedPuzzleObject = Instantiate(_Puzzle.PuzzlePrefab, _puzzleParentObject.transform);
            _puzzleTitle.text = _Puzzle.PuzzleTitle;
            backGroundSet.setBackGround(_Puzzle);
        }

        IEnumerator NextClick()
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0));
            NextStage();
        }
    }
}