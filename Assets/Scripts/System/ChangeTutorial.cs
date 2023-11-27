using Ingame.Tutorial;
using UnityEngine;

namespace System
{
    public class ChangeTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialManager manager;
        [SerializeField] private bool rotatable;
        public bool Rotatable => rotatable;
        
        public void ActiveChange(bool value)
        {
            manager.UIActiveChange(value);
        }

        public void CompleteStage()
        {
            rotatable = false;
            StartCoroutine(manager.NextStage());
        }
    }
}
