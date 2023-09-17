using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Effect
{
    public class EffectTest : MonoBehaviour
    {
        Camera Camera;
        private void Start()
        {
            Camera = Camera.main;

        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                EffectManager.Instance.InstanceEffect(EffectType.FadeIn,Vector3.zero);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    EffectManager.Instance.InstanceEffect(EffectType.FadeOut, Vector3.zero);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                var pos = Camera.ScreenToWorldPoint(Input.mousePosition + Camera.transform.forward * 10);
                EffectManager.Instance.InstanceEffect(EffectType.PieceSet, pos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                var pos = Camera.ScreenToWorldPoint(Input.mousePosition + Camera.transform.forward * 10);
                EffectManager.Instance.InstanceEffect(EffectType.CompletePuzzle, pos);
            }
        }
    }
}