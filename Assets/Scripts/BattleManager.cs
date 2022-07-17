using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;        // wtf is this? Was included with iteration through children code...
using Priority_Queue;
using Combat;

public enum Action {START, ATK, SKILL, MOVE, END};
public enum TargetMode {NONE, SINGLE, ROW, COLUMN, ALL};   // For AoE spells later maybe? Not currently used

public class BattleManager : StateMachine
{
    public GameObject battleStations;
    public BattleUI battleUI;
    public Action action;

    // Important info for flow
    public List<Unit> allyUnits;
    public List<Unit> enemyUnits;
    public List<BattleTile> tiles;
    public List<BattleTile> targeted;   // Tiles currently targeted. Selection script under BattleTile.cs
    public bool busy;                          // To stop coroutines from overlapping
    public Unit takingTurn;            

    public TargetMode targetMode;

    public SimplePriorityQueue<Unit> turnOrder = new SimplePriorityQueue<Unit>(new ReverseComparer<float>());


    // Start is called before the first frame update
    void Start()
    {
        busy = false;
        Debug.Log("Start");
        targetMode = TargetMode.NONE;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        Debug.Log("Setup");
        // For every tile, spawn its occupant
        int x = 0;  // Systematically label each square as it's added to the List
        foreach (Transform tile in battleStations.transform)
        {
            BattleTile currTile = tile.gameObject.GetComponent<BattleTile>();
            currTile.battleManager = this;
            currTile.id = x;
            x++;
            // add to tiles
            tiles.Add(currTile);
            if (currTile.occupiedBy == null)
                continue;
            // maybe add animation here?
            Unit currUnit = Instantiate(currTile.occupiedBy, currTile.transform);
            currTile.occupiedBy = currUnit;
            if (currUnit.unitAffl == affl.ALLY)
                allyUnits.Add(currUnit);
            else
                enemyUnits.Add(currUnit);

            // Add to turn order
            turnOrder.Enqueue(currUnit, currUnit.unitSpd.value);
        }
        // Initialize UI by giving it the list of units
        battleUI.Init(allyUnits, enemyUnits);

        SetState(new TurnManager(this));
        yield break;
    }

    public void SetAction(int set)
    {
//        Debug.Log("Setting action");
        if (busy)
        {
            return; // No spamming!
        }
        action = (Action) set;
        switch(action)
        {
            case Action.ATK:
                Debug.Log("Attack");
                StartCoroutine(State.Attack());
                SetState(new PlayerTurn(this));               // PlayerTurn state waits till everything is done
                break;
            case Action.SKILL:
                Debug.Log("Skill");
                StartCoroutine(State.Skill());
                SetState(new PlayerTurn(this));
                break;
            case Action.MOVE:
                Debug.Log("Move");
                StartCoroutine(State.Move());                 // Not yet implemented
                SetState(new PlayerTurn(this));
                break;
            case Action.END:
                Debug.Log("End");
                StartCoroutine(State.End());
                // Does nothing rn
                SetState(new PlayerTurn(this));
                break;
        }
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