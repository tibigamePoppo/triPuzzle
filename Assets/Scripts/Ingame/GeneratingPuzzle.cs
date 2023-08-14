using System.Collections.Generic;
using UnityEngine;

public class GeneratingPuzzle : MonoBehaviour
{
    [Header("生成するお題のプレファブ")]
    [SerializeField, Tooltip("お題のプレファブすべてを格納する")]
    private List<GameObject> Puzzle;
    [SerializeField, Tooltip("生成するパズルの親のオブジェクト")]
    private GameObject PuzzleParentObject;
    private GameObject GeneratedPuzzleObject = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        int randomInt = Random.Range(0, Puzzle.Count);
        GeneratedPuzzleObject = Instantiate(Puzzle[randomInt], PuzzleParentObject.transform);
    }

    public void ReGenerate()
    {
        if (GeneratedPuzzleObject == null)
        {
            Debug.LogError("GeneratedPuzzleObjectが設定されていません");
            return;
        }
        Destroy(GeneratedPuzzleObject);
        Generate();
    }
}
