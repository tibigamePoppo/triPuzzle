using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace System
{
    public class ChangeWindowActive : MonoBehaviour
    {
        [SerializeField, Tooltip("アクティブを変更するオブジェクトを設定")]
        private GameObject activeObject;
        [SerializeField, Tooltip("変更されるアクティブの値")]
        private bool value;
        ButtonController controller;
        void Start()
        {
            controller = GetComponent<ButtonController>();
            controller
                .Pushed
                .Subscribe(_ =>
                {
                    ActiveChange();
                }).AddTo(this);
        }

        private void ActiveChange()
        {
            activeObject.SetActive(value);
        }
    }
}