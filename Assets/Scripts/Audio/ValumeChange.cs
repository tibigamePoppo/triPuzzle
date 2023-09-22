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
        //private float preValume = 0f;
        private AudioSource _bgmSource;
        private AudioSource _seSource;
        [SerializeField,Tooltip("ボリュームがオンとオフの時のSprite")]
        private Sprite[] valumeImage;
        [SerializeField, Tooltip("ボリューム調整用のスライダー")]
        private Slider slider;
        Image image;

        void Start()
        {
            controller = GetComponent<ButtonController>();
            image = GetComponent<Image>();
            _bgmSource = FindObjectOfType<BGMManager>().GetComponent<AudioSource>();
            _seSource = FindObjectOfType<SeManager>().GetComponent<AudioSource>();
            if (_bgmSource == null || _seSource == null) return;
            valumeValue = _bgmSource.volume;
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
                    if (value > 0)
                    {
                        image.sprite = valumeImage[0]; 
                        valumeOn = false;
                    }
                    else
                    {
                        image.sprite = valumeImage[1];
                        valumeOn = true;
                        if (valumeValue <= 0) valumeValue = 0.8f;
                    }
                    _bgmSource.volume = value; _seSource.volume = value;
                }).AddTo(this);
        }

        
        private void ValumeOn()
        {
            if(valumeOn)
            {
                slider.value = valumeValue;
            }
            else
            {
                valumeValue = slider.value;
                slider.value = 0;
            }
        }
    }
}
