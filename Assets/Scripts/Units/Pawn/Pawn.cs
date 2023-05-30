using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseHero
{
    public static Pawn instance;
    public BaseHero selectedPawn;
    public SpriteRenderer sprite;
    public Sprite spritePawn;
    public Sprite spriteTower;
    public Sprite spriteAlfil;
    public Sprite spriteHorse;

    // Start is called before the first frame update
    void Start()
    {
        selectedPawn = FindObjectOfType<BaseHero>();
        sprite = selectedPawn.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (UnitManager.instance.isTower == true)
        {
            sprite.sprite = spriteTower;

        }
        else if (UnitManager.instance.isAlfil == true)
        {
            sprite.sprite = spriteAlfil;

        }
        else if (UnitManager.instance.isKnight == true)
        {
            sprite.sprite = spriteHorse;

        }
        else
        {
            sprite.sprite = spritePawn;

        }
    }

    public void changeSpritePawn()
    {
        sprite.sprite = spritePawn;
    }

    public void changeSpriteTower()
    {
        sprite.sprite = spriteTower;
    }

    public void changeSpriteAlfil()
    {
        sprite.sprite = spriteAlfil;
    }

    public void changeSpriteHorse()
    {
        sprite.sprite = spriteHorse;
    }


}
