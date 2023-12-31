using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffect : MonoBehaviour
{
    ParticleSystem tapEffect;
    Camera Camera;

    private void Start()
    {
        tapEffect = GetComponent<ParticleSystem>();
        StartCoroutine(InputMouse());
    }
    IEnumerator InputMouse()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0));
            if(Camera == null)
            {
                Camera = Camera.main;
            }
            var pos = Camera.ScreenToWorldPoint(Input.mousePosition + Camera.transform.forward * 10);
            tapEffect.transform.position = pos;
            tapEffect.Emit(1);
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse0));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
