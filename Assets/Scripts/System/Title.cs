using Audio;
using UnityEngine;

public class Title : MonoBehaviour
{
    void Start()
    {
        BGMManager.Instance.ShotSe(BGMType.title);
    }

}
