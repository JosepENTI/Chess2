using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;


[CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableUnit")]

public class ScriptableUnits : ScriptableObject
{
    public Faction faction;
    public BaseUnit unitPrefab;

  
}

public enum Faction 
{
    Hero = 0,
    Enemy = 1,

}
