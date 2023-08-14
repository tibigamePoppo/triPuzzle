using System.Collections.Generic;
using UnityEngine;

public class GeneratingPuzzle : MonoBehaviour
{
    [Header("�������邨��̃v���t�@�u")]
    [SerializeField, Tooltip("����̃v���t�@�u���ׂĂ��i�[����")]
    private List<GameObject> Puzzle;
    [SerializeField, Tooltip("��������p�Y���̐e�̃I�u�W�F�N�g")]
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
            Debug.LogError("GeneratedPuzzleObject���ݒ肳��Ă��܂���");
            return;
        }
        Destroy(GeneratedPuzzleObject);
        Generate();
    }
}
