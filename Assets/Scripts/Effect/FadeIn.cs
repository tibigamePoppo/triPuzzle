using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    private Image FadeBase;
    [SerializeField]
    private float FadePerSpeed = 0.1f;
    [SerializeField]
    private int FadeDivisionNumber = 20;
    void Start()
    {
        StartCoroutine(FadeInAnimation());
    }

    IEnumerator FadeInAnimation()
    {
        for (int i = 0; i < FadeDivisionNumber; i++)
        {
            yield return new WaitForSeconds(FadePerSpeed);
            FadeBase.color = new Color(1, 1, 1, (float)i / FadeDivisionNumber);
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
