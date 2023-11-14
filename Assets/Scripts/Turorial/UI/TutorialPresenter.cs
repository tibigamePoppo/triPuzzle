using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Ingame.Tutorial
{
    public class TutorialPresenter : MonoBehaviour
    {
        [SerializeField]
        TutorialManager _tutorialManager;
        [SerializeField]
        List<GameObject> _stageUi;

        void Start()
        {
            foreach (var item in _stageUi)
            {
                item.SetActive(false);
            }            
                _tutorialManager
               .tutorialStage
               .Subscribe(stage =>
               {
                   if(stage == 0)
                   {
                       _stageUi[(int)stage].SetActive(true);
                   }
                   else
                   {
                       _stageUi[(int)stage - 1].SetActive(false);
                       _stageUi[(int)stage].SetActive(true);
                   }
               }).AddTo(this);
        }
    }
}