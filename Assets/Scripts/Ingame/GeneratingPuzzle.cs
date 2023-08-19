using System.Collections.Generic;
using UnityEngine;
using Ingame;
using TMPro;

public class GeneratingPuzzle : MonoBehaviour
{
    [Header("�������邨��̃v���t�@�u")]
    [SerializeField, Tooltip("�����PuzzleData���ׂĂ��i�[����")]
    private List<PuzzleData> Puzzle;
    [SerializeField, Tooltip("��������p�Y���̐e�̃I�u�W�F�N�g")]
    private GameObject PuzzleParentObject;
    [SerializeField, Tooltip("�p�Y���̃^�C�g���e�L�X�g")]
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
            Debug.LogWarning("GeneratingPuzzle��Puzzle�Ƀf�[�^���Z�b�g����Ă��܂���");
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
            Debug.LogError("GeneratedPuzzleObject���ݒ肳��Ă��܂���");
            return;
        }
        Destroy(GeneratedPuzzleObject);
        Generate(same);
    }
}
