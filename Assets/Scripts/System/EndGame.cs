using System.Collections;
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
                    StartCoroutine(GameEnd());
                }).AddTo(this);
        }
        
        //�Q�[���I��:�{�^������Ăяo��
        IEnumerator GameEnd()
        {
            yield return new WaitForSeconds(0.5f);
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
    #else
            Application.Quit();//�Q�[���v���C�I��
    #endif
        }
    }
}
