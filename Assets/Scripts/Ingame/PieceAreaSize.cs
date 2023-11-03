using UnityEngine;
using UnityEngine.UI;

public class PieceAreaSize : MonoBehaviour
{
    GridLayoutGroup _gridLayoutGroup;

    void Start()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        Difficulty _difficulty = PlayerConfig.difficulty;
        switch (_difficulty)
        {
            case Difficulty.Normal:
                _gridLayoutGroup.cellSize = new Vector2(100, 100);
                _gridLayoutGroup.spacing = new Vector2(70, 70);
                break;
            case Difficulty.Hard:
                _gridLayoutGroup.cellSize = new Vector2(50, 50);
                _gridLayoutGroup.spacing = new Vector2(45, 45);
                break;
            default:
                break;
        }
    }


    void Update()
    {
        
    }
}
