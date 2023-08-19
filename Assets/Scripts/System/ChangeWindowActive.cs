using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace System
{
    public class ChangeWindowActive : MonoBehaviour
    {
        [SerializeField, Tooltip("�A�N�e�B�u��ύX����I�u�W�F�N�g��ݒ�")]
        private GameObject activeObject;
        [SerializeField, Tooltip("�ύX�����A�N�e�B�u�̒l")]
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