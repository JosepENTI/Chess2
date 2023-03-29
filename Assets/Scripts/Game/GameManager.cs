using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Gamestate gameState;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(Gamestate.GenerateGrid);
    }

    public void ChangeState(Gamestate newState) 
    {
     gameState = newState;
        switch (newState) 
        {
            case Gamestate.GenerateGrid:
                GridManager.instance.GenerateGrid();
                break;
            case Gamestate.SpawnPawn:
                break;
            case Gamestate.SpawnEnemies:
                break;
            case Gamestate.PawnTurn:
                break;
            case Gamestate.EnemiesTurn:
                break;
                default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }
    
    
    }
   
}


public enum Gamestate 
{
GenerateGrid = 0,
SpawnPawn = 1,
SpawnEnemies = 2,
PawnTurn=3,
EnemiesTurn = 4
}
