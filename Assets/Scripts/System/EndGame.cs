using System.Collections;
using System.Effect;
using UniRx;
using UnityEngine;

namespace System
{
    public class EndGame : MonoBehaviour
    {
        ButtonController _controller;

        private void Start()
        {
            _controller = GetComponent<ButtonController>();
            _controller
                .Pushed
                .Where(pushed => pushed)
                .Subscribe(_ =>
                {
                    EffectManager.Instance.InstanceEffect(EffectType.FadeIn, Vector3.zero);
                    StartCoroutine(GameEnd());
                }).AddTo(this);
        }
        
        //�Q�[���I��:�{�^������Ăяo��
        IEnumerator GameEnd()
        {
            yield return new WaitForSeconds(0.8f);
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
    #else
            Application.Quit();//�Q�[���v���C�I��
    #endif
        }
    }
}
