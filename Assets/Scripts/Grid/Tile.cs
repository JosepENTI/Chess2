using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite _whiteSprite, _blackSprite;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool isWalkable;

    public BaseUnit occupiedUnit;

    public bool walkable => isWalkable && occupiedUnit == null;


    public void Init(bool isOffset)
    {
        _renderer.sprite = isOffset? _blackSprite : _whiteSprite;
    }

    public void SetUnit(BaseUnit unit) 
    {
        if(unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
        unit.transform.position = transform.position;
        occupiedUnit = unit;
        unit.occupiedTile = this;

    }

    private void OnMouseDown()
    {
        if (GameManager.instance.gameState != Gamestate.PawnTurn) return;

        if (occupiedUnit == null)
        {
            if (occupiedUnit.faction == Faction.Hero)
            {
                UnitManager.instance.SetSelectedPawn((BaseHero)occupiedUnit);
            }
            else
            {
                if (UnitManager.instance.selectedPawn != null)
                {
                    var enemy = (BaseEnemy)occupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.instance.SetSelectedPawn(null);
                }

            }
        }
        else 
        {
            if(UnitManager.instance.selectedPawn != null) 
            {
             SetUnit(UnitManager.instance.selectedPawn);
                UnitManager.instance.SetSelectedPawn(null);
            }
        
        
        }
        
        
    }
}
