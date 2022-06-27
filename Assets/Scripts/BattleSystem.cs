using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;        // wtf is this?

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject battleStations;

    // Set up the objects to load. TODO how do we save and retrieve these?
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // TODO might need 1 for each of the 18 spots...
    // TODO attacks affiliated with spots then?
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

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
            BattleTile curr = tile.gameObject.GetComponent<BattleTile>();
            if (curr.occupiedBy == null)
                continue;
            // maybe add animation here?
            Instantiate(curr.occupiedBy, tile);
        }
        yield return new WaitForSeconds(2f);

        /*
        // Put everyone in the right spot. How do we save ally positions?
        // Enemy positions?
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN; // TODO turn management goes here probably
        PlayerTurn();                   // TODO also will be different depending on turn management*/
    }

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

}
