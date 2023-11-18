using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveExample : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> movePoint = new List<GameObject>();
    [SerializeField]
    private float time;
    void Start()
    {
        Vector3[] pathPoint = new Vector3[movePoint.Count];
        for (int i = 0; i < movePoint.Count; i++)
        {
            pathPoint[i] = movePoint[i].transform.position;
        }
        transform.DOPath(pathPoint, time,PathType.CatmullRom).SetLoops(-1,LoopType.Restart);
    }
}
