using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite _whiteSprite, _blackSprite;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject redHighlight;
    public bool isWalkable;

    public Tile pawnPosition;
    public Tile pawnLastPosition;


    public BaseUnit occupiedUnit;

    public bool walkable => isWalkable && occupiedUnit == null;

    private void Update()
    {
        pawnLastPosition = pawnPosition;

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene("Tutorial_1");
        
        }

        if (isWalkable == true)
        {
            redHighlight.SetActive(true);

        }
        else 
        {
        redHighlight.SetActive (false);
        }
    }
    public void Init(int x, int y)
    {
        var isOffset = (x + y) % 2 == 1;
        _renderer.sprite = isOffset? _blackSprite : _whiteSprite;
        
        
    }

    public void SetUnit(BaseUnit unit) 
    {
        if(unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        unit.transform.position = transform.position;
        occupiedUnit = unit;
        unit.occupiedTile = this;

    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    void OnMouseDown()
    {
        if (GameManager.instance.gameState != Gamestate.PawnTurn) return;

        if (occupiedUnit != null)
        {
            if (occupiedUnit.faction == Faction.Hero)
            {                             
                UnitManager.instance.SetSelectedPawn((BaseHero)occupiedUnit);
                pawnPosition = GridManager.instance.GetTileAtPosition(new Vector2(UnitManager.instance.selectedPawn.occupiedTile.transform.position.x,
           UnitManager.instance.selectedPawn.occupiedTile.transform.position.y));


                if (UnitManager.instance.isTower == true)
                {
                    SetWalkableTower();
                }
                else if (UnitManager.instance.isAlfil == true)
                {
                    SetWalkableAlfil();
                }
                else if (UnitManager.instance.isKnight == true) 
                {
                    SetWalkableKnight();
                }
                else
                {
                    SetWalkableKing();
                }


            }
            else
            {
                if (UnitManager.instance.selectedPawn != null && isWalkable == true )
                {
                    var enemy = (BaseUnit)occupiedUnit;
                    //de momento
                    if (enemy.CompareTag("Tower"))
                    {
                        UnitManager.instance.SetTower();
                    }
                    else if (enemy.CompareTag("Alfil"))
                    {
                        UnitManager.instance.SetAlfil();
                    }
                    else if (enemy.CompareTag("Horse")) 
                    {
                        UnitManager.instance.SetKnight();
                    }
                    else
                    {
                        UnitManager.instance.SetKing();
                        GameManager.instance.ActivePanel();
                    }
                    Destroy(enemy.gameObject);
                    SetUnit(UnitManager.instance.selectedPawn);
                    UnitManager.instance.SetSelectedPawn(null);
                    GridManager.instance.SetWalkableOff();
                }

            }
        }
        else 
        {
            if(UnitManager.instance.selectedPawn != null && walkable) 
            {
               
                SetUnit(UnitManager.instance.selectedPawn);               
                UnitManager.instance.SetSelectedPawn(null);
                GridManager.instance.SetWalkableOff();
            }
        
        
        }
        
        
    }

    public void SetWalkableKing() 
    {
        for(int i = -1; i <= 1; i++) // top row
        {
            //TOP ROW
            Tile topTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y + 1));
            if (topTile != null)
                topTile.isWalkable = true;

            //MID ROW
            if (i != 0) {
                Tile middleTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y));
                if (middleTile != null)
                    middleTile.isWalkable = true;
            }

            //BOTTOM ROW
            Tile bottomTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y - 1));
            if (bottomTile != null)
                bottomTile.isWalkable = true;
        }
    }

    public void SetWalkableTower() 
    {
        for (int i = 0; i < 6; i++) 
        {
            Tile topTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x, pawnPosition.transform.position.y + i));
            if (topTileT != null) 
            {
                topTileT.isWalkable = true;
            }

            Tile botTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x, pawnPosition.transform.position.y - i));
            if (botTileT != null)
            {
                botTileT.isWalkable = true;
            }

            Tile rightTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x+i, pawnPosition.transform.position.y));
            if (rightTileT != null)
            {
                rightTileT.isWalkable = true;
            }

            Tile leftTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y));
            if (leftTileT != null)
            {
                leftTileT.isWalkable = true;
            }
        }                
    }


    public void SetWalkableAlfil() 
    {
      
        for (int i = 0; i < 6; i++)
        {
            Tile topRTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x+i, pawnPosition.transform.position.y + i));
            if (topRTileT != null)
            {
                topRTileT.isWalkable = true;
            }

            Tile topLTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x+i, pawnPosition.transform.position.y - i));
            if (topLTileT != null)
            {
                topLTileT.isWalkable = true;
            }

            Tile botRTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y+i));
            if (botRTileT != null)
            {
                botRTileT.isWalkable = true;
            }

            Tile botLTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y-i));
            if (botLTileT != null)
            {
                botLTileT.isWalkable = true;
            }

        }
    }

    public void SetWalkableQueen()
    {
        for (int i = 0; i < 6; i++)
        {
            Tile topTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x, pawnPosition.transform.position.y + i));
            if (topTileT != null)
            {
                topTileT.isWalkable = true;
            }

            Tile botTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x, pawnPosition.transform.position.y - i));
            if (botTileT != null)
            {
                botTileT.isWalkable = true;
            }

            Tile rightTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y));
            if (rightTileT != null)
            {
                rightTileT.isWalkable = true;
            }

            Tile leftTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y));
            if (leftTileT != null)
            {
                leftTileT.isWalkable = true;
            }


            Tile topRTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y + i));
            if (topRTileT != null)
            {
                topRTileT.isWalkable = true;
            }

            Tile topLTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y - i));
            if (topLTileT != null)
            {
                topLTileT.isWalkable = true;
            }

            Tile botRTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y + i));
            if (botRTileT != null)
            {
                botRTileT.isWalkable = true;
            }

            Tile botLTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y - i));
            if (botLTileT != null)
            {
                botLTileT.isWalkable = true;
            }
        }

    }

    public void SetWalkableKnight()
    {

        for (int i = -1; i <= 1; i++) // top row
        {
            //TOP ROW
            

            //MID ROW
            if (i != 0)
            {
                Tile topTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y + 2));
                if (topTile != null)
                    topTile.isWalkable = true;


                Tile bottomTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y - 2));
                if (bottomTile != null)
                    bottomTile.isWalkable = true;


                Tile rightTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 2, pawnPosition.transform.position.y + i));
                if (rightTile != null)
                    rightTile.isWalkable = true;


                Tile leftTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 2, pawnPosition.transform.position.y + i));
                if (leftTile != null)
                    leftTile.isWalkable = true;

            }

            //BOTTOM ROW
           
        }




    }

}


