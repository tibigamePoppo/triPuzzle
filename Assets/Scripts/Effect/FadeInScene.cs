using System.Effect;
using UnityEngine;

public class FadeInScene : MonoBehaviour
{
    void Start()
    {
        EffectManager.Instance.InstanceEffect(EffectType.FadeOut, Vector3.zero);
    }
}
