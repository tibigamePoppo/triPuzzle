using System.Collections.Generic;
using UnityEngine;
using Ingame;
using TMPro;

public class GeneratingPuzzle : MonoBehaviour
{
    [Header("生成するお題のプレファブ")]
    [SerializeField, Tooltip("お題のPuzzleDataすべてを格納する")]
    private List<PuzzleData> Puzzle;
    [SerializeField, Tooltip("生成するパズルの親のオブジェクト")]
    private GameObject PuzzleParentObject;
    [SerializeField, Tooltip("パズルのタイトルテキスト")]
    private TextMeshProUGUI PuzzleTitle;
    private GameObject GeneratedPuzzleObject = null;
    int randomInt = 0;
    void Start()
    {
        if (Puzzle.Count != 0)
        {
            Generate(false);
        }
        else
        {
            Debug.LogWarning("GeneratingPuzzleのPuzzleにデータがセットされていません");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(bool same)
    {
        if(!same) randomInt = Random.Range(0, Puzzle.Count);
        GeneratedPuzzleObject = Instantiate(Puzzle[randomInt].PuzzlePrefab, PuzzleParentObject.transform);
        PuzzleTitle.text = Puzzle[randomInt].PuzzleTitle;
    }

    public void ReGenerate(bool same)
    {
        if (GeneratedPuzzleObject == null)
        {
            Debug.LogError("GeneratedPuzzleObjectが設定されていません");
            return;
        }
        Destroy(GeneratedPuzzleObject);
        Generate(same);
    }
}
