using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == Gamestate.EnemiesTurn) 
        {
           var enemyToMove = UnitManager.instance.GetRandomUnit<BaseEnemy>(Faction.Enemy);

            if (enemyToMove.CompareTag("Tower"))
            {
                //funcion movimiento torre
            }
            else if (enemyToMove.CompareTag("Alfil"))
            {
                //funcion movimiento alfil
            }
            else if (enemyToMove.CompareTag("Horse"))
            {
                //funcion movimiento caballo
            }
        }        
    }
}
