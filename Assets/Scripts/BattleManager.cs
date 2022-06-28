using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;        // wtf is this? Was included with iteration through children code...
using Priority_Queue;

public enum BattleState {START, TARGET, PLAYERTURN, ENEMYTURN, ENDTURN, WON, LOST};

public class BattleManager : MonoBehaviour
{
    public BattleState state;
    public GameObject battleStations;
    public BattleUI battleUI;

    public List<Unit> allyUnits;
    public List<Unit> enemyUnits;
    int temp = 10; // For now it runs for 10 turns because I don't want to stick Unity into an infinite loop

    public SimplePriorityQueue<Unit> turnOrder = new SimplePriorityQueue<Unit>(new ReverseComparer<float>());

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
            Unit currUnit = Instantiate(currTile.occupiedBy, currTile.transform);
            if (currUnit.unitAffl == affl.ALLY)
                allyUnits.Add(currUnit);
            else
                enemyUnits.Add(currUnit);
            // Add to turn order\
            turnOrder.Enqueue(currUnit, currUnit.unitSpd.value);
            Debug.Log("Count " + turnOrder.Count);
        }

        // Initialize UI by giving it the list of units
        battleUI.Init(allyUnits, enemyUnits);

        yield return new WaitForSeconds(2f);
        TurnManager();
    }

    void TurnManager()
    {
        Debug.Log("TurnManager begin");
        // Check for win/losecon
        
        // Check if anyone is above threshold
        while(turnOrder.GetPriority(turnOrder.First) < 1000)
        {
            // If no one, increment until someone is
            foreach(Unit unit in turnOrder)
            {
                Debug.Log("No one yet");
                turnOrder.UpdatePriority(unit, turnOrder.GetPriority(unit) + unit.unitSpd.value);
            }
        }

        Unit currUnit = turnOrder.First;
        // First move back to bottom of turnorder
        turnOrder.UpdatePriority(currUnit, 0);
        // Give them the turn
        if (currUnit.unitAffl == affl.ALLY)
            Debug.Log("Ally turn");
        else{
            Debug.Log("Enemy turn");
        }

        // hard 10 turn limit (move it to LoseCon area once ally and enemy turns are implemented)
        temp--;
        if (temp > 0)
            TurnManager();
    }
}

// Probably want this in another class? Reversing comparisons to turn minPriorityQueue into a maxPriorityQueue
public class ReverseComparer<T> : IComparer<T> where T : System.IComparable<T>
{
    public int Compare(T obj1, T obj2)
    {
        return -(obj1.CompareTo(obj2));
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
