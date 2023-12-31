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
        
        //ゲーム終了:ボタンから呼び出す
        IEnumerator GameEnd()
        {
            yield return new WaitForSeconds(0.8f);
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
    #else
            Application.Quit();//ゲームプレイ終了
    #endif
        }
    }
}
