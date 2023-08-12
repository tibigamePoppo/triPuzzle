using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;

namespace System
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField,Tooltip("ˆÚ“®æ‚ÌƒV[ƒ“‚ğİ’è")]
        private SceneObject targetScene;
        ButtonController controller;

        private void Start()
        {
            controller = GetComponent<ButtonController>();
            controller
                .Pushed
                .Where(pushed => pushed)
                .Subscribe(_ =>
                {
                    StartCoroutine(Change());
                }).AddTo(this);
        }

        IEnumerator Change()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(targetScene);
        }
    }
}