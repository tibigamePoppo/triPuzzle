using System;
using UnityEngine;

public class NextPazzle : MonoBehaviour
{
    [SerializeField]
    private GameStateManager manager;
    
    public void OnClick()
    {
        manager.ChangeState(GameState.SetUp);
    }
}
