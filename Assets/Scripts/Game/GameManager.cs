using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Gamestate gameState;
    public int enemyNum;
    public int wallNum;
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
        AudioManager.Instance.PlayMusic("Theme");
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
            case Gamestate.SpawnWalls:
                UnitManager.instance.SpawnWalls(wallNum);
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
                AudioManager.Instance.PlaySFX("GameOver");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }
    
    
    }

    public void ActivePanel()
    {
        panel.SetActive(true);

    }
    public void SceneLoad(string scene)
    {        
        switch (scene)
        {
            case "Tutorial_1":
                if (!PlayerPrefs.HasKey("level_2"))
                {
                    PlayerPrefs.SetInt("level_2", 1);
                }
                SceneManager.LoadScene("level2");
                break;
            case "level2":
                if (!PlayerPrefs.HasKey("level_3"))
                {
                    PlayerPrefs.SetInt("level_3", 1);
                }
                SceneManager.LoadScene("level3");
                break;
            case "level3":
                if (!PlayerPrefs.HasKey("level_4"))
                {
                    PlayerPrefs.SetInt("level_4", 1);
                }
                SceneManager.LoadScene("level4");
                break;
            case "level4":
                if (!PlayerPrefs.HasKey("level_5"))
                {
                    PlayerPrefs.SetInt("level_5", 1);
                }
                SceneManager.LoadScene("level5");
                break;
            case "level5":
                if (!PlayerPrefs.HasKey("level_6"))
                {
                    PlayerPrefs.SetInt("level_6", 1);
                }
                SceneManager.LoadScene("level6");
                break;
            case "level6":
                break;
            
        }
    }
}




public enum Gamestate 
{
GenerateGrid = 0,
SpawnPawn = 1,
SpawnKing = 2,
SpawnEnemies = 3,
SpawnWalls =4,
PawnTurn= 5,
EnemiesTurn = 6,
GameOver = 7
}
