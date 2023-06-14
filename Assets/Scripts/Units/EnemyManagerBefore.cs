using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManagerBefore : MonoBehaviour
{
    public static EnemyManager instance;

    public BaseEnemy enemyToMove;
    public Tile enemyPosition;
    public Tile positionToMove;
    public AudioSource effect;

    // Update is called once per frame


    private void Start()
    {
        //enemyToMove = UnitManager.instance.GetRandomUnit<BaseEnemy>(Faction.Enemy);





    }


    void Update()
    {
        if (GameManager.instance.gameState == Gamestate.EnemiesTurn)
        {

            enemyToMove = RandomEnemy();

            if (enemyToMove != null)
            {


                enemyPosition = GridManager.instance.GetTileAtPosition(new Vector2(enemyToMove.occupiedTile.transform.position.x,
               enemyToMove.occupiedTile.transform.position.y));



                if (enemyToMove.CompareTag("Tower"))
                {
                    positionToMove = EnemyWalkableTower();
                    //funcion movimiento torre
                    if (positionToMove.isWalkable == true)
                    {

                        SetEnemy(enemyToMove, positionToMove);
                        GridManager.instance.SetWalkableOff();
                        enemyToMove = null;
                        effect.Play();
                        GameManager.instance.ChangeState(Gamestate.PawnTurn);
                    }

                }
                else if (enemyToMove.CompareTag("Alfil"))
                {
                    positionToMove = EnemyWalkableAlfil();
                    //funcion movimiento alfil
                    if (positionToMove.isWalkable == true)
                    {

                        SetEnemy(enemyToMove, positionToMove);
                        GridManager.instance.SetWalkableOff();
                        enemyToMove = null;
                        effect.Play();
                        GameManager.instance.ChangeState(Gamestate.PawnTurn);
                    }
                }
                else if (enemyToMove.CompareTag("Horse"))
                {
                    positionToMove = EnemyWalkableKnight();
                    //funcion movimiento caballo
                    if (positionToMove.isWalkable == true)
                    {

                        SetEnemy(enemyToMove, positionToMove);
                        GridManager.instance.SetWalkableOff();
                        enemyToMove = null;
                        effect.Play();
                        GameManager.instance.ChangeState(Gamestate.PawnTurn);
                    }
                }
                else
                    enemyToMove = null;
                GameManager.instance.ChangeState(Gamestate.PawnTurn);



            }
            else GameManager.instance.ChangeState(Gamestate.PawnTurn);
        }

    }

    public Tile EnemyWalkableTower()
    {
        List<Tile> positions = new List<Tile>();
        int index;
        Tile tileReturned;

        for (int i = 0; i < 6; i++)
        {
            Tile topTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x, enemyPosition.transform.position.y + i));
            if (topTileT != null)
            {
                topTileT.isWalkable = true;

                if (topTileT.occupiedUnit != null)
                {
                    if (topTileT.occupiedUnit.tag != "Player")
                    {
                        topTileT.isWalkable = false;
                    }
                }

                if (topTileT.isWalkable == true)
                {
                    positions.Add(topTileT);
                }

            }

            Tile botTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x, enemyPosition.transform.position.y - i));
            if (botTileT != null)
            {
                botTileT.isWalkable = true;

                if (botTileT.occupiedUnit != null)
                {
                    if (botTileT.occupiedUnit.tag != "Player")
                    {
                        botTileT.isWalkable = false;
                    }
                }

                if (botTileT.isWalkable == true)
                {
                    positions.Add(botTileT);
                }
            }

            Tile rightTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + i, enemyPosition.transform.position.y));
            if (rightTileT != null)
            {
                rightTileT.isWalkable = true;

                if (rightTileT.occupiedUnit != null)
                {
                    if (rightTileT.occupiedUnit.tag != "Player")
                    {
                        rightTileT.isWalkable = false;
                    }
                }

                if (rightTileT.isWalkable == true)
                {
                    positions.Add(rightTileT);
                }

            }

            Tile leftTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x - i, enemyPosition.transform.position.y));
            if (leftTileT != null)
            {
                leftTileT.isWalkable = true;


                if (leftTileT.occupiedUnit != null)
                {
                    if (leftTileT.occupiedUnit.tag != "Player")
                    {
                        leftTileT.isWalkable = false;
                    }
                }

                if (leftTileT.isWalkable == true)
                {
                    positions.Add(leftTileT);
                }
            }
  
        }

        index = UnityEngine.Random.Range(0, positions.Count);
        tileReturned = positions[index];
        return tileReturned;

    }


    public Tile EnemyWalkableAlfil()
    {
        List<Tile> positions = new List<Tile>();
        int index;
        Tile tileReturned;

        for (int i = 0; i < 6; i++)
        {
            Tile topRTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + i, enemyPosition.transform.position.y + i));
            if (topRTileT != null )
            {
                topRTileT.isWalkable = true;

                if (topRTileT.occupiedUnit != null)
                {
                    if (topRTileT.occupiedUnit.tag != "Player")
                    {
                        topRTileT.isWalkable = false;
                    }
                }

                if (topRTileT.isWalkable == true)
                {
                    positions.Add(topRTileT);
                }
            }

            Tile topLTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + i, enemyPosition.transform.position.y - i));
            if (topLTileT != null)
            {
                topLTileT.isWalkable = true;

                if (topLTileT.occupiedUnit != null)
                {
                    if (topLTileT.occupiedUnit.tag != "Player")
                    {
                        topLTileT.isWalkable = false;
                    }
                }

                if (topLTileT.isWalkable == true)
                {
                    positions.Add(topLTileT);
                }
            }

            Tile botRTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x - i, enemyPosition.transform.position.y + i));
            if (botRTileT != null)
            {
                botRTileT.isWalkable = true;

                if (botRTileT.occupiedUnit != null)
                {
                    if (botRTileT.occupiedUnit.tag != "Player")
                    {
                        botRTileT.isWalkable = false;
                    }
                }

                if (botRTileT.isWalkable == true)
                {
                    positions.Add(botRTileT);
                }
            }

            Tile botLTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x - i, enemyPosition.transform.position.y - i));
            if (botLTileT != null)
            {
                botLTileT.isWalkable = true;

                if (botLTileT.occupiedUnit != null)
                {
                    if (botLTileT.occupiedUnit.tag != "Player")
                    {
                        botLTileT.isWalkable = false;
                    }
                }

                if (botLTileT.isWalkable == true)
                {
                    positions.Add(botLTileT);
                }
            }
           
        }
        index = UnityEngine.Random.Range(0, positions.Count);
        tileReturned = positions[index];
        return tileReturned;
    }

    public Tile EnemyWalkableKnight()
    {
        List<Tile> positions = new List<Tile>();
        int index;
        Tile tileReturned;

        for (int i = -1; i <= 1; i++) // top row
        {
            //TOP ROW


            //MID ROW
            if (i != 0)
            {
                Tile topTile = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + i, enemyPosition.transform.position.y + 2));
                if (topTile != null)
                {
                    topTile.isWalkable = true;

                    if (topTile.occupiedUnit != null)
                    {
                        if (topTile.occupiedUnit.tag != "Player")
                        {
                            topTile.isWalkable = false;
                        }
                    }

                    if (topTile.isWalkable == true)
                    {
                        positions.Add(topTile);
                    }
                }


                Tile bottomTile = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + i, enemyPosition.transform.position.y - 2));
                if (bottomTile != null)
                {
                    bottomTile.isWalkable = true;

                    if (bottomTile.occupiedUnit != null)
                    {
                        if (bottomTile.occupiedUnit.tag != "Player")
                        {
                            bottomTile.isWalkable = false;
                        }
                    }

                    if (bottomTile.isWalkable == true)
                    {
                        positions.Add(bottomTile);
                    }
                }

                Tile rightTile = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + 2, enemyPosition.transform.position.y + i));
                if (rightTile != null)
                {
                    rightTile.isWalkable = true;

                    if (rightTile.occupiedUnit != null)
                    {
                        if (rightTile.occupiedUnit.tag != "Player")
                        {
                            rightTile.isWalkable = false;
                        }
                    }

                    if (rightTile.isWalkable == true)
                    {
                        positions.Add(rightTile);
                    }
                }

                Tile leftTile = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x - 2, enemyPosition.transform.position.y + i));
                if (leftTile != null)
                {
                    leftTile.isWalkable = true;

                    if (leftTile.occupiedUnit != null)
                    {
                        if (leftTile.occupiedUnit.tag != "Player")
                        {
                            leftTile.isWalkable = false;
                        }
                    }

                    if (leftTile.isWalkable == true)
                    {
                        positions.Add(leftTile);
                    }
                }

            }

            //BOTTOM ROW


        }
        index = UnityEngine.Random.Range(0, positions.Count);
        tileReturned = positions[index];
        return tileReturned;



    }

    public void SetEnemy(BaseUnit unit, Tile tile)
    {
        if (unit.occupiedTile != null)
        {
            if (tile.occupiedUnit != null) 
            {
                if (tile.occupiedUnit.tag == "Player") 
                {
                    GameManager.instance.ChangeState(Gamestate.GameOver);
                }
            }
            
            unit.occupiedTile.occupiedUnit = null;

        }
        unit.transform.position = tile.transform.position;
        tile.occupiedUnit = unit;
        unit.occupiedTile = tile;

    }


    public BaseEnemy RandomEnemy()
    {
        var enemies = FindObjectsOfType<BaseEnemy>();
        if (enemies.Length == 0)
        {
            return null;
        }
        else
        {
            int index;
            BaseEnemy enemyReturned;
            index = UnityEngine.Random.Range(0, enemies.Length);



            enemyReturned = enemies[index];


            return enemyReturned;
        }

    }
}

