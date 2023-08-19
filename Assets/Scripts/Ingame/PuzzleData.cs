using UnityEngine;

namespace Ingame
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PuzzleData")]
    public class PuzzleData : ScriptableObject
    {
        public GameObject PuzzlePrefab;
        public string PuzzleTitle;
    }
}