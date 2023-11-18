using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EmphasisColor : MonoBehaviour
{
    void Start()
    {
        Image _targetImage = GetComponent<Image>();
        _targetImage.DOColor(new Color(_targetImage.color.r, _targetImage.color.g, _targetImage.color.b, 0.3f), 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
