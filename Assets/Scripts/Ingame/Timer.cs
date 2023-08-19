using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private int count = 0;
    void Start()
    {
        StartCoroutine(countUp());
    }

    public void CountStop()
    {
        StopCoroutine(countUp());
    }

    IEnumerator countUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            count++;
            text.text = count.ToString();
        }
    }
}
