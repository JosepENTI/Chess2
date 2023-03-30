using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    private List<ScriptableUnits> units;

    private void Awake()
    {
        instance = this;

        units = Resources.LoadAll<ScriptableUnits>("Units").ToList();
    }

    public void SpawnHeroes() 
    {
        var heroCount = 1;

        for (int i = 0; i < heroCount; i++) 
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);

        }
    
    }

    private T GetRandomUnit<T>(Faction faction) where T :  BaseUnit 
    {
        return (T)units.Where(u=>u.faction == faction).OrderBy(o=>Random.value).First().unitPrefab; 
    
    }
}
