using Audio;
using Piece;
using UnityEngine;

public class Title : MonoBehaviour
{
    void Start()
    {
        BGMManager.Instance.ShotSe(BGMType.title);
        GeneratingPuzzle._initializePuzzleQueue = true;
    }

}
