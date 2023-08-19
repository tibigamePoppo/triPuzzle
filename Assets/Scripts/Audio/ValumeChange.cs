using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class ValumeChange : MonoBehaviour
    {
        ButtonController controller;
        private bool valumeOn = true;
        private float valumeValue = 1f;
        AudioSource source;
        [SerializeField,Tooltip("ボリュームがオンとオフの時のSprite")]
        private Sprite[] valumeImage;
        [SerializeField, Tooltip("ボリューム調整用のスライダー")]
        private Slider slider;
        Image image;

        void Start()
        {
            controller = GetComponent<ButtonController>();
            image = GetComponent<Image>();
            source = FindObjectOfType<AudioSource>();
            if (source == null) return;
            valumeValue = source.volume;
            slider.value = valumeValue;
            controller
                .Pushed
                .Subscribe(_ =>
                {
                    ValumeOn();
                }).AddTo(this);
            slider
                .OnValueChangedAsObservable()
                .Subscribe(value =>
                {
                    valumeValue = value;
                    source.volume = valumeValue;
                }).AddTo(this);
        }

        
        private void ValumeOn()
        {
            valumeOn = !valumeOn;
            if(valumeOn)
            {
                image.sprite = valumeImage[0];
                source.volume = valumeValue;
            }
            else
            {
                image.sprite = valumeImage[1];
                source.volume = 0;
            }
        }
    }
}
