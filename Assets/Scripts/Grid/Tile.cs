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
                else if(UnitManager.instance.isAlfil == true) 
                {
                    SetWalkableAlfil();
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

        if (GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
           pawnPosition.transform.position.y - 1)) == null)
        {
            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
              pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y - 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y - 1)).isWalkable = true;

        }
        else if (GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
           pawnPosition.transform.position.y)) == null)
        {
            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
              pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y - 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y - 1)).isWalkable = true;
        }
        else if (GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
           pawnPosition.transform.position.y + 1)) == null)
        {
            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y - 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y - 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y - 1)).isWalkable = true;

        }
        else if (GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
           pawnPosition.transform.position.y - 1)) == null)
        {
            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y + 1)).isWalkable = true;
        }
        else 
        {
            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y - 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y + 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 1,
               pawnPosition.transform.position.y - 1)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 1,
               pawnPosition.transform.position.y - 1)).isWalkable = true;


        }


    }

    public void SetWalkableTower() 
    {
        

        for (int i = 0; i < 6; i++) 
        {
            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y + i)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x,
               pawnPosition.transform.position.y - i)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i,
               pawnPosition.transform.position.y)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i,
               pawnPosition.transform.position.y)).isWalkable = true;

        }                
    }


    public void SetWalkableAlfil() 
    {
      
        for (int i = 0; i < 4; i++)
        {
            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x+ i,
               pawnPosition.transform.position.y + i)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x +i,
               pawnPosition.transform.position.y - i)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i,
               pawnPosition.transform.position.y+i)).isWalkable = true;

            GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i,
               pawnPosition.transform.position.y-i)).isWalkable = true;

        }
    }

    
}
