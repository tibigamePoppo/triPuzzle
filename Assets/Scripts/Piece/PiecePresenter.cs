using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Piece
{
    public class PiecePresenter : MonoBehaviour
    {
        Image image;
        [SerializeField]
        Sprite _frameImage;
        void Awake()
        {
            image = GetComponent<Image>();
        }

        public void ColorChange(Color c)
        {
            image.sprite = _frameImage;
            image.color = c;
        }
    }
}