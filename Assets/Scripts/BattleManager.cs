using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;        // wtf is this? Was included with iteration through children code...
using Priority_Queue;
using Combat;

public enum Action {START, ATK, SKILL, MOVE, END};
public enum TargetMode {SINGLE, ROW, COLUMN, ALL};   // For AoE spells later maybe? Not currently used

public class BattleManager : StateMachine
{
    public GameObject battleStations;
    public BattleUI battleUI;
    public Action action;
    public Selection select;

    public List<Unit> allyUnits;
    public List<Unit> enemyUnits;
    public List<BattleTile> tiles;

    public SimplePriorityQueue<Unit> turnOrder = new SimplePriorityQueue<Unit>(new ReverseComparer<float>());

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        Debug.Log("Setup");
        // For every tile, spawn its occupant
        foreach (Transform tile in battleStations.transform)
        {
            BattleTile currTile = tile.gameObject.GetComponent<BattleTile>();
            currTile.battleManager = this; // Do I need this?
            if (currTile.occupiedBy == null)
                continue;
            // maybe add animation here?
            Unit currUnit = Instantiate(currTile.occupiedBy, currTile.transform);
            if (currUnit.unitAffl == affl.ALLY)
                allyUnits.Add(currUnit);
            else
                enemyUnits.Add(currUnit);

            // add to tiles
            tiles.Add(currTile);
            // Add to turn order
            turnOrder.Enqueue(currUnit, currUnit.unitSpd.value);
        }
        // Load information into Selection class (handles all the target selection)
        select.BattleManager = this;
        select.Tiles = tiles;
        // Initialize UI by giving it the list of units
        battleUI.Init(allyUnits, enemyUnits);

        yield return new WaitForSeconds(2f);
        SetState(new TurnManager(this));
    }

    public void SetAction(int set)
    {
//        Debug.Log("Setting action");
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
