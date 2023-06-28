using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    public static Tile instance;

    [SerializeField] private Sprite _whiteSprite, _blackSprite;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject redHighlight;
    public bool isWalkable;
    //TORRE
    bool postenemyTopT = false;
    bool postenemyBotT = false;
    bool postenemyRT = false;
    bool postenemyLT = false;
    //ALFIL
    bool postenemyTopA = false;
    bool postenemyBotA = false;
    bool postenemyRA = false;
    bool postenemyLA = false;

    public Tile pawnPosition;
    public Tile pawnLastPosition;


    public BaseUnit occupiedUnit;

    public bool walkable => isWalkable && occupiedUnit == null;

    private void Update()
    {
        pawnLastPosition = pawnPosition;

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        }

        if (isWalkable == true)
        {
            redHighlight.SetActive(true);

        }
        else 
        {
        redHighlight.SetActive (false);
        }

        if (FindObjectOfType<BaseHero>().occupiedTile.occupiedUnit == null) 
        {
            FindObjectOfType<BaseHero>().occupiedTile.occupiedUnit = FindObjectOfType<BaseHero>();
        }

    }
    public void Init(int x, int y)
    {
        var isOffset = (x + y) % 2 == 1;
        _renderer.sprite = isOffset? _blackSprite : _whiteSprite;
        
        
    }

    public void SetUnit(BaseUnit unit) 
    {
        if (unit.occupiedTile != null) 
        {
            unit.occupiedTile.occupiedUnit = null;
        } 
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
        if (GameManager.instance.gameState != Gamestate.PawnTurn) 
        {
            return;
        }
        
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
                    else if (enemy.CompareTag("King"))
                    {
                        UnitManager.instance.SetKing();
                        GameManager.instance.ActivePanel();
                    }                  
                    Destroy(enemy.gameObject);                    
                    SetUnit(UnitManager.instance.selectedPawn);
                    UnitManager.instance.SetSelectedPawn(null);
                    GridManager.instance.SetWalkableOff();
                    GameManager.instance.ChangeState(Gamestate.EnemiesTurn);
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
                GameManager.instance.ChangeState(Gamestate.EnemiesTurn);
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
            {
                topTile.isWalkable = true;
                if(topTile.occupiedUnit != null) 
                {
                    if (topTile.occupiedUnit.CompareTag("Wall")) 
                    {
                        topTile.isWalkable = false;
                    }

                }
            }
            //MID ROW
            if (i != 0)
            {
                Tile middleTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y));
                if (middleTile != null)
                {
                    middleTile.isWalkable = true;

                    if (middleTile.occupiedUnit != null)
                    {
                        if (middleTile.occupiedUnit.CompareTag("Wall"))
                        {
                            middleTile.isWalkable = false;
                        }


                    }
                }
            }
            //BOTTOM ROW
            Tile bottomTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y - 1));
            if (bottomTile != null)
            {
                bottomTile.isWalkable = true;

                    if (bottomTile.occupiedUnit != null)
                    {
                        if (bottomTile.occupiedUnit.CompareTag("Wall"))
                        {
                            bottomTile.isWalkable = false;
                        }


                    }
                }
        }
    }

    public void SetWalkableTower() 
    {
        for (int i = 0; i < 6; i++) 
        {
            Tile topTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x, pawnPosition.transform.position.y + i));
            
            if (topTileT != null) 
            {                
             
                if (postenemyTopT == true)
                {
                    topTileT.isWalkable = false;
                }
                else
                {
                    topTileT.isWalkable = true;
                }

                if (topTileT.occupiedUnit != null)
                {
                    if (topTileT.occupiedUnit.CompareTag("Wall"))
                    {
                        topTileT.isWalkable = false;
                    }
                    
                    if (topTileT.occupiedUnit.faction != Faction.Hero)
                    {
                        postenemyTopT = true;
                    }
                }

            }

            

            Tile botTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x, pawnPosition.transform.position.y - i));
            
            if (botTileT != null)
            {                              

                if (postenemyBotT == true )
                {
                    botTileT.isWalkable = false;
                }
                else
                {
                    botTileT.isWalkable = true;
                }

                if (botTileT.occupiedUnit != null)
                {

                    if (botTileT.occupiedUnit.CompareTag("Wall"))
                    {
                        botTileT.isWalkable = false;
                    }
                     
                    if (botTileT.occupiedUnit.faction != Faction.Hero)
                    {
                        postenemyBotT = true;
                    }
                }

            }

            Tile rightTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x+i, pawnPosition.transform.position.y));
            
            if (rightTileT != null)
            {                                

                if (postenemyRT == true )
                {
                    rightTileT.isWalkable = false;
                }
                else
                {
                    rightTileT.isWalkable = true;
                }

                if (rightTileT.occupiedUnit != null)
                {
                    if (rightTileT.occupiedUnit.CompareTag("Wall"))
                    {
                        rightTileT.isWalkable = false;
                    }

                     if (rightTileT.occupiedUnit.faction != Faction.Hero)
                    {
                        postenemyRT = true;
                    }
                }

            }

            Tile leftTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y));
            
            if (leftTileT != null)
            {

                if (postenemyLT == true)
                {
                    leftTileT.isWalkable = false;
                }
                else 
                {
                    leftTileT.isWalkable = true;
                }

                if (leftTileT.occupiedUnit != null)
                {

                    if (leftTileT.occupiedUnit.CompareTag("Wall"))
                    {
                        leftTileT.isWalkable = false;
                    }

                     if (leftTileT.occupiedUnit.faction != Faction.Hero)
                    {
                        postenemyLT = true;
                    }
                }
            }
            
        }        
        postenemyBotT = false;
        postenemyTopT = false;
        postenemyLT = false;
        postenemyRT = false;
    }


    public void SetWalkableAlfil() 
    {
      
        for (int i = 0; i < 6; i++)
        {
            Tile topRTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x+i, pawnPosition.transform.position.y + i));
            if (topRTileT != null)
            {
                topRTileT.isWalkable = true;
                if (postenemyTopA == true)
                {
                    topRTileT.isWalkable = false;
                }
                else
                {
                    topRTileT.isWalkable = true;
                }

                if (topRTileT.occupiedUnit != null)
                {

                    if (topRTileT.occupiedUnit.CompareTag("Wall"))
                    {
                        topRTileT.isWalkable = false;
                    }

                     if (topRTileT.occupiedUnit.faction != Faction.Hero)
                    {
                        postenemyTopA = true;
                    }
                }
            }

            Tile topLTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x+i, pawnPosition.transform.position.y - i));
            if (topLTileT != null)
            {
                

                if (postenemyBotA == true)
                {
                    topLTileT.isWalkable = false;
                }
                else
                {
                    topLTileT.isWalkable = true;
                }

                if (topLTileT.occupiedUnit != null)
                {

                    if (topLTileT.occupiedUnit.CompareTag("Wall"))
                    {
                        topLTileT.isWalkable = false;
                    }

                     if (topLTileT.occupiedUnit.faction != Faction.Hero)
                    {
                        postenemyBotA = true;
                    }
                }
            }

            Tile botRTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y+i));
            if (botRTileT != null)
            {

                if (postenemyRA == true)
                {
                    botRTileT.isWalkable = false;
                }
                else
                {
                    botRTileT.isWalkable = true;
                }

                if (botRTileT.occupiedUnit != null)
                {

                    if (botRTileT.occupiedUnit.CompareTag("Wall"))
                    {
                        botRTileT.isWalkable = false;
                    }

                     if (botRTileT.occupiedUnit.faction != Faction.Hero)
                    {
                        postenemyRA = true;
                    }
                }
            }

            Tile botLTileT = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - i, pawnPosition.transform.position.y-i));
            if (botLTileT != null)
            {

                if (postenemyLA == true)
                {
                    botLTileT.isWalkable = false;
                }
                else
                {
                    botLTileT.isWalkable = true;
                }

                if (botLTileT.occupiedUnit != null)
                {

                    if (botLTileT.occupiedUnit.CompareTag("Wall"))
                    {
                        botLTileT.isWalkable = false;
                    }

                     if (botLTileT.occupiedUnit.faction != Faction.Hero)
                    {
                        postenemyLA = true;
                    }
                }
            }

        }
        postenemyBotA = false;
        postenemyTopA = false;
        postenemyLA = false;
        postenemyRA = false;
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
                {
                    topTile.isWalkable = true;

                    if (topTile.occupiedUnit != null)
                    {
                        if (topTile.occupiedUnit.CompareTag("Wall"))
                        {
                            topTile.isWalkable = false;
                        }

                    }
                }

                Tile bottomTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + i, pawnPosition.transform.position.y - 2));
                if (bottomTile != null)
                {
                    bottomTile.isWalkable = true;

                    if (bottomTile.occupiedUnit != null)
                    {
                        if (bottomTile.occupiedUnit.CompareTag("Wall"))
                        {
                            bottomTile.isWalkable = false;
                        }

                    }
                }

                Tile rightTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x + 2, pawnPosition.transform.position.y + i));
                if (rightTile != null)
                {
                    rightTile.isWalkable = true;

                    if (rightTile.occupiedUnit != null)
                    {
                        if (rightTile.occupiedUnit.CompareTag("Wall"))
                        {
                            rightTile.isWalkable = false;
                        }

                    }
                }

                Tile leftTile = GridManager.instance.GetTileAtPosition(new Vector2(pawnPosition.transform.position.x - 2, pawnPosition.transform.position.y + i));
                if (leftTile != null)
                {
                    leftTile.isWalkable = true;

                    if (leftTile.occupiedUnit != null)
                    {
                        if (leftTile.occupiedUnit.CompareTag("Wall"))
                        {
                            leftTile.isWalkable = false;
                        }

                    }
                }

            }

            //BOTTOM ROW
           
        }




    }

}


