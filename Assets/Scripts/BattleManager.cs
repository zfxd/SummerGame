using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;        // wtf is this? Was included with iteration through children code...
using Priority_Queue;
using Combat;

public enum BattleState {START, TARGET, PLAYERTURN, ENEMYTURN, ENDTURN, WON, LOST}; // Not sure if the last 3 will be needed
public enum Action {START, ATK, SKILL, MOVE, END};
public enum SelectionMode {SINGLE, ROW, COLUMN, ALL};

public class BattleManager : StateMachine
{
    public BattleState state;
    public GameObject battleStations;
    public BattleUI battleUI;
    public Action action;

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
            currTile.battleManager = this;
            if (currTile.occupiedBy == null)
                continue;
            // maybe add animation here?
            Unit currUnit = Instantiate(currTile.occupiedBy, currTile.transform);
            if (currUnit.unitAffl == affl.ALLY)
                allyUnits.Add(currUnit);
            else
                enemyUnits.Add(currUnit);
            // Add to turn order
            turnOrder.Enqueue(currUnit, currUnit.unitSpd.value);
        }

        // Initialize UI by giving it the list of units
        battleUI.Init(allyUnits, enemyUnits);

        yield return new WaitForSeconds(2f);
        SetState(new TurnManager(this));
    }

    void AllyTurn()
    {
        state = BattleState.PLAYERTURN;
        Debug.Log("Ally Turn");
        action = Action.START;
        // Do stuff
        Debug.Log("Select an action");
        // attack, skill, move, end
        switch(action)
        {
            case Action.ATK:
                Debug.Log("Attack");
                break;
            case Action.SKILL:
                Debug.Log("Skill");
                break;
            case Action.MOVE:
                Debug.Log("Move");
                break;
            case Action.END:
                Debug.Log("End");
                break;
        }

        // All done
        // Automatically exits into turnmanager
    }


    void EnemyTurn() // Maybe coroutine for this as well? Might not need it though cause it doesnt need to wait for input
    {
        state = BattleState.ENEMYTURN;
        // Enemy does jack shit till we get AI implemented
        // Maybe test some attacks
        Debug.Log("Enemy Turn");
        // Do stuff

        // All done
        // Automatically exits into turnmanager
    }

    public void SetAction(int set)
    {
        if (state == BattleState.PLAYERTURN)
            action = (Action) set;
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
