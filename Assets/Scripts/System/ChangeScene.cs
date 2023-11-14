using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Collections;
using System.Effect;

namespace System
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField,Tooltip("�ړ���̃V�[����ݒ�")]
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
                    EffectManager.Instance.InstanceEffect(EffectType.FadeIn, Vector3.zero);
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