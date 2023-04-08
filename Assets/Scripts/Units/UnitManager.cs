using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    public BaseHero selectedPawn;

    private List<ScriptableUnits> units;

    private void Awake()
    {
        instance = this;

        units = Resources.LoadAll<ScriptableUnits>("Units").ToList();
    }

    public void SpawnHeroes() 
    {

        
            var pawnPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(pawnPrefab);
            var spawnTile = GridManager.instance.GetPawnSpawnTile();

            spawnTile.SetUnit(spawnedHero);


        GameManager.instance.ChangeState(Gamestate.SpawnEnemies);
    }   
    public void SpawnKing() 
    {
        // de moment es un que es el rei, quan hi hagi més s'ha de cambiar la
        // 
            var kingPrefab = GetRandomUnit<BaseKing>(Faction.King);
            var spawnedKing = Instantiate(kingPrefab);
            var spawnTile = GridManager.instance.GetKingSpawnTile();

            spawnTile.SetUnit(spawnedKing);

        GameManager.instance.ChangeState(Gamestate.PawnTurn);
    }

    public void SpawnEnemies(int enemies)
    {
        for (int i = 0; i < enemies; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
        }
       
        GameManager.instance.ChangeState(Gamestate.SpawnKing);
    }

    private T GetRandomUnit<T>(Faction faction) where T :  BaseUnit 
    {
        //posible canvi per fer diferents spawns
        return (T)units.Where(u=>u.faction == faction).OrderBy(o=>Random.value).First().unitPrefab; 
    }

    public void SetSelectedPawn(BaseHero pawn) 
    {
     selectedPawn= pawn;
    }

}
