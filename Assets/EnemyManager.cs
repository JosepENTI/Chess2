using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public BaseEnemy enemyToMove;
    public Tile enemyPosition;
    public Tile positionToMoveT;
    public Tile positionToMoveA;
    public Tile positionToMoveK;
    // Update is called once per frame


    private void Start()
    {
        //enemyToMove = UnitManager.instance.GetRandomUnit<BaseEnemy>(Faction.Enemy);
       

        
        
        
    }


    void Update()
    {
        enemyToMove = FindObjectOfType<BaseEnemy>();

        if (enemyToMove != null)
        {


            enemyPosition = GridManager.instance.GetTileAtPosition(new Vector2(enemyToMove.occupiedTile.transform.position.x,
           enemyToMove.occupiedTile.transform.position.y));
            if (GameManager.instance.gameState == Gamestate.EnemiesTurn)
            {


                if (enemyToMove.CompareTag("Tower"))
                {
                    positionToMoveT = EnemyWalkableTower();
                    //funcion movimiento torre
                    if (positionToMoveT.isWalkable == true)
                    {

                        SetEnemy(enemyToMove, positionToMoveT);
                        GridManager.instance.SetWalkableOff();
                        GameManager.instance.ChangeState(Gamestate.PawnTurn);
                    }
                }
                else if (enemyToMove.CompareTag("Alfil"))
                {
                    positionToMoveA = EnemyWalkableAlfil();
                    //funcion movimiento alfil
                    if (positionToMoveA.isWalkable == true)
                    {

                        SetEnemy(enemyToMove, positionToMoveA);
                        GridManager.instance.SetWalkableOff();
                        GameManager.instance.ChangeState(Gamestate.PawnTurn);
                    }
                }
                else if (enemyToMove.CompareTag("Horse"))
                {
                    positionToMoveK = EnemyWalkableKnight();
                    //funcion movimiento caballo
                    if (positionToMoveK.isWalkable == true)
                    {

                        SetEnemy(enemyToMove, positionToMoveK);
                        GridManager.instance.SetWalkableOff();
                        GameManager.instance.ChangeState(Gamestate.PawnTurn);
                    }
                }
                else
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
            }

            Tile botTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x, enemyPosition.transform.position.y - i));
            if (botTileT != null)
            {
                botTileT.isWalkable = true;
            }

            Tile rightTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + i, enemyPosition.transform.position.y));
            if (rightTileT != null)
            {
                rightTileT.isWalkable = true;
            }

            Tile leftTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x - i, enemyPosition.transform.position.y));
            if (leftTileT != null)
            {
                leftTileT.isWalkable = true;
            }
            positions.Add(topTileT);
            positions.Add(botTileT);
            positions.Add(rightTileT);
            positions.Add(leftTileT);


        }

        index = Random.Range(0, positions.Count);
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
            if (topRTileT != null)
            {
                topRTileT.isWalkable = true;
            }

            Tile topLTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + i, enemyPosition.transform.position.y - i));
            if (topLTileT != null)
            {
                topLTileT.isWalkable = true;
            }

            Tile botRTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x - i, enemyPosition.transform.position.y + i));
            if (botRTileT != null)
            {
                botRTileT.isWalkable = true;
            }

            Tile botLTileT = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x - i, enemyPosition.transform.position.y - i));
            if (botLTileT != null)
            {
                botLTileT.isWalkable = true;
            }
            positions.Add(topRTileT);
            positions.Add(topLTileT);
            positions.Add(botRTileT);
            positions.Add(botLTileT);
        }
        index = Random.Range(0, positions.Count);
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
                    topTile.isWalkable = true;


                Tile bottomTile = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + i, enemyPosition.transform.position.y - 2));
                if (bottomTile != null)
                    bottomTile.isWalkable = true;


                Tile rightTile = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x + 2, enemyPosition.transform.position.y + i));
                if (rightTile != null)
                    rightTile.isWalkable = true;


                Tile leftTile = GridManager.instance.GetTileAtPosition(new Vector2(enemyPosition.transform.position.x - 2, enemyPosition.transform.position.y + i));
                if (leftTile != null)
                    leftTile.isWalkable = true;


                positions.Add(topTile);
                positions.Add(bottomTile);
                positions.Add(rightTile);
                positions.Add(leftTile);

            }

            //BOTTOM ROW


        }
        index = Random.Range(0, positions.Count);
        tileReturned = positions[index];
        return tileReturned;



    }

    public void SetEnemy(BaseUnit unit, Tile tile)
    {
        if (unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        unit.transform.position = tile.transform.position;
        unit.occupiedTile = tile;

    }
}
