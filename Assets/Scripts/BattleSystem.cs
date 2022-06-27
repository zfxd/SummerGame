using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;        // wtf is this? Was included with iteration through children code...
using Priority_Queue;

public enum BattleState {START, TARGET, PLAYERTURN, ENEMYTURN, ENDTURN, WON, LOST};

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject battleStations;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        Debug.Log("Setup");
        // For every tile, spawn its occupant
        foreach (Transform tile in battleStations.transform)
        {
            BattleTile currTile = tile.gameObject.GetComponent<BattleTile>();
            if (currTile.occupiedBy == null)
                continue;
            // maybe add animation here?
            Unit currUnit = currTile.occupiedBy;
            Instantiate(currUnit, currTile.transform);
            // Add to turn order
        }
        yield return new WaitForSeconds(2f);
    }

    void TurnManager()
    {
        // 
    }
}


/*
    // Set up the objects to load. TODO how do we save and retrieve these?
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // TODO might need 1 for each of the 18 spots...
    // TODO attacks affiliated with spots then?
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;


    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        enemyUnit.TakeDamage(playerUnit.atk);

        yield return new WaitForSeconds(2f);

        // Turn Management
    }

    void PlayerTurn()
    {
        Debug.Log("Player Turn");
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerAttack());
    }

*/
