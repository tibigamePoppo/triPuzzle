using UnityEngine;
using UniRx;

namespace System
{
    public class SetDifficulty : MonoBehaviour
    {
        [SerializeField]
        private Difficulty _difficulty;
        ButtonController controller;
        void Start()
        {
            controller = GetComponent<ButtonController>();
            controller
                .Pushed
                .Where(pushed => pushed)
                .Subscribe(_ =>
                {
                    PlayerConfig.difficulty = _difficulty;
                }).AddTo(this);

        }
    }
}