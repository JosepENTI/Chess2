using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Gamestate gameState;
    public int enemyNum;
    public GameObject panel;
    public GameObject panelGameOver;

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
                UnitManager.instance.SpawnHeroes();
                break;
            case Gamestate.SpawnKing:
                UnitManager.instance.SpawnKing();
                break;
            case Gamestate.SpawnEnemies:
                UnitManager.instance.SpawnEnemies(enemyNum);
                break;
            case Gamestate.PawnTurn:

                break;
            case Gamestate.EnemiesTurn:
                break;
            case Gamestate.GameOver:
                panelGameOver.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }
    
    
    }

    public void ActivePanel()
    {
        panel.SetActive(true);

    }

}


public enum Gamestate 
{
GenerateGrid = 0,
SpawnPawn = 1,
SpawnKing = 2,
SpawnEnemies = 3,
PawnTurn= 4,
EnemiesTurn = 5,
GameOver = 6
}
