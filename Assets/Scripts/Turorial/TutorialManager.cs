using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Effect;
using Audio;
using TMPro;
using Piece;
using UnityEngine.SceneManagement;

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
        Stage7 = 6,
    }
    public class TutorialManager : MonoBehaviour, IDropObservable
    {
        ReactiveProperty<TutorialStage> _tutorialStage = new ReactiveProperty<TutorialStage>();
        public IObservable<TutorialStage> tutorialStage//_currentHp�̕ω����������Ƃ��ɔ��s�����
        {
            get { return _tutorialStage; }
        }
        [SerializeField, Tooltip("�����PuzzleData���ׂĂ��i�[����")]
        private PuzzleData _Puzzle;
        [SerializeField, Tooltip("��������p�Y���̐e�̃I�u�W�F�N�g")]
        private GameObject _puzzleParentObject;
        [SerializeField, Tooltip("�p�Y���̃^�C�g���e�L�X�g")]
        private TextMeshProUGUI _puzzleTitle;
        [SerializeField]
        BackGroundSet backGroundSet;
        [SerializeField]
        Transform PazzlePosition;
        [SerializeField]
        List<GameObject> testPiece;
        [SerializeField]
        private PieceArea pieceArea;
        [SerializeField]
        private GameObject[] hidePiece;
        [SerializeField]
        private GameObject clickUI;
        [SerializeField]
        private GameObject yachtPanel;
        [SerializeField]
        private SceneObject targetScene;
        [SerializeField]
        private GameObject[] navigateUI;
        [SerializeField] 
        private GameObject[] mobilePieces;

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
                        case TutorialStage.Stage1://���̃s�[�X�̐���
                            StartCoroutine(NextClick());
                        break;
                        case TutorialStage.Stage2://�E�̃p�Y���̐���
                            foreach (var item in testPiece)
                            {
                                item.SetActive(true);
                            }
                            Generate();
                            mobilePieces[0].GetComponent<CanvasGroup>().blocksRaycasts = true;
                            break;
                        case TutorialStage.Stage3://�s�[�X���p�Y���ɓ��Ă͂߂����
                            mobilePieces[0].GetComponent<CanvasGroup>().blocksRaycasts = false;
                            mobilePieces[1].GetComponent<CanvasGroup>().blocksRaycasts = true;
                            break;
                        case TutorialStage.Stage4://�^�ƈႤ�s�[�X���u���Ȃ�����
                            break;
                        case TutorialStage.Stage5://���ׂẴs�[�X���͂܂�ƃp�Y�����N���A�ł������
                            break;
                        case TutorialStage.Stage6:
                            SeManager.Instance.ShotSe(SeType.complete);
                            StartCoroutine(NextClick());
                            break;
                        case TutorialStage.Stage7://�`���[�g���A���I��
                            EffectManager.Instance.InstanceEffect(EffectType.FadeIn, Vector3.zero);
                            StartCoroutine(Change());
                            break;
                        default:
                            break;
                    }
                }).AddTo(this);
        }

        public IEnumerator NextStage()
        {
            yield return new WaitForSeconds(0.5f);
            _tutorialStage.Value++;
        }

        private void Generate()
        {
            _puzzleTitle.text = _Puzzle.PuzzleTitle;
            backGroundSet.setBackGround(_Puzzle);
        }

        IEnumerator NextClick()
        {
            yield return new WaitForSeconds(1f);
            clickUI.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse0));
            clickUI.SetActive(false);
            StartCoroutine(NextStage());
        }

        public void CheckCompletePuzzle()
        {
            if(pieceArea.isAllUsePiece())
            {
                EffectManager.Instance.InstanceEffect(EffectType.CompletePuzzle, PazzlePosition.position);
                StartCoroutine(goResult());
            }
        }

        IEnumerator goResult()
        {
            yield return new WaitForSeconds(1.5f);
            yachtPanel.GetComponent<CompleteImage>().ShowImage();
            foreach (var o in hidePiece)
            {
                o.SetActive(false);
            }
            yield return new WaitForSeconds(2.8f);
            StartCoroutine(NextStage());
        }

        IEnumerator Change()
        {
            yield return new WaitForSeconds(0.8f);
            SceneManager.LoadScene(targetScene);
        }

        public void UIActiveChange(bool active)
        {
            switch (_tutorialStage.Value)
            {
                case TutorialStage.Stage2:
                    navigateUI[0].SetActive(active);
                    break;
                case TutorialStage.Stage3:
                    navigateUI[1].SetActive(active);
                    break;
                case TutorialStage.Stage4:
                    navigateUI[2].SetActive(active);
                    break;
                default:
                    break;
            }
        }
    }
}