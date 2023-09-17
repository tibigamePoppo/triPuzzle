using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
    public class BackGroundSet : MonoBehaviour
    {
        [SerializeField, Tooltip("”wŒiƒpƒlƒ‹")]
        private Image BackGroundPanel;
        [SerializeField,Tooltip("”wŒiŒ³‰æ‘œ")]
        private Sprite[] BackGroundImage;

        public void setBackGround(PuzzleData data)
        {
            switch (data.PuzzleType)
            {
                case PuzzleType.night:
                    BGMManager.Instance.ShotSe(BGMType.night);
                    BackGroundPanel.sprite = BackGroundImage[0];
                    break;
                case PuzzleType.room:
                    BGMManager.Instance.ShotSe(BGMType.room);
                    BackGroundPanel.sprite = BackGroundImage[1];
                    break;
                case PuzzleType.rain:
                    BGMManager.Instance.ShotSe(BGMType.rain);
                    BackGroundPanel.sprite = BackGroundImage[2];
                    break;
                default:
                    break;
            }
        }
    }
}