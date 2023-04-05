using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite _whiteSprite, _blackSprite;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool isWalkable;

    public Tile pawnPosition;
    public Tile pawnLastPosition;


    public BaseUnit occupiedUnit;

    public bool walkable => isWalkable && occupiedUnit == null;

    private void Update()
    {
        pawnLastPosition = pawnPosition;
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
                SetWalkable();
                SetWalkableOff();
            }
            else
            {
                if (UnitManager.instance.selectedPawn != null)
                {
                    var enemy = (BaseEnemy)occupiedUnit;
                    //de momento
                    Destroy(enemy.gameObject);
                    UnitManager.instance.SetSelectedPawn(null);
                }

            }
        }
        else 
        {
            if(UnitManager.instance.selectedPawn != null && walkable) 
            {
               
                SetUnit(UnitManager.instance.selectedPawn);               
                UnitManager.instance.SetSelectedPawn(null);
                
            }
        
        
        }
        
        
    }

    public void SetWalkable() 
    {

        pawnPosition = GridManager.instance.GetTileAtPosition(new Vector2(UnitManager.instance.selectedPawn.occupiedTile.transform.position.x,
           UnitManager.instance.selectedPawn.occupiedTile.transform.position.y));

        //hacer ifs para cuando alguna parte sea null
        GridManager.instance.GetTileAtPosition(new Vector2(UnitManager.instance.selectedPawn.occupiedTile.transform.position.x+1,
           UnitManager.instance.selectedPawn.occupiedTile.transform.position.y)).isWalkable = true;

        GridManager.instance.GetTileAtPosition(new Vector2(UnitManager.instance.selectedPawn.occupiedTile.transform.position.x-1,
           UnitManager.instance.selectedPawn.occupiedTile.transform.position.y)).isWalkable = true;

        GridManager.instance.GetTileAtPosition(new Vector2(UnitManager.instance.selectedPawn.occupiedTile.transform.position.x,
                   UnitManager.instance.selectedPawn.occupiedTile.transform.position.y+1)).isWalkable = true;

        GridManager.instance.GetTileAtPosition(new Vector2(UnitManager.instance.selectedPawn.occupiedTile.transform.position.x,
           UnitManager.instance.selectedPawn.occupiedTile.transform.position.y-1)).isWalkable = true;
    }

    public void SetWalkableOff()
    {

        GridManager.instance.GetTileAtPosition(new Vector2(pawnLastPosition.transform.position.x + 1,
           pawnLastPosition.transform.position.y)).isWalkable = false;

        GridManager.instance.GetTileAtPosition(new Vector2(pawnLastPosition.transform.position.x - 1,
           pawnLastPosition.transform.position.y)).isWalkable = false;

        GridManager.instance.GetTileAtPosition(new Vector2(pawnLastPosition.transform.position.x,
                   pawnLastPosition.transform.position.y + 1)).isWalkable = false;

        GridManager.instance.GetTileAtPosition(new Vector2(pawnLastPosition.transform.position.x,
           pawnLastPosition.transform.position.y - 1)).isWalkable = false;


    }
}
