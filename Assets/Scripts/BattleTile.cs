using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTile : MonoBehaviour
{
    public Unit occupiedBy;
    public BattleManager battleManager;
    HashSet<Unit> targeted = new HashSet<Unit>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse is over " + this.name);
        // target selection
        // "targeted" allocation depends on type of ability?

        // single target
        this.Target();

        // Possibly target others too
    }


    void OnMouseExit()
    {
        Debug.Log("Mouse has left " + this.name);
        // untarget

        // single untarget
        this.Untarget();

        // Possibly untarget others too
    }

    HashSet<Unit> OnMouseDown()
    {
        Debug.Log("Clicked " + this.name);
        // confirm target selection
        // ONLY AVAILABLE DURING TARGET MODE
        if (battleManager.state == BattleState.TARGET)
        {
            return targeted;
        }
        return null;
    }

    void Target()
    {
        Debug.Log(this.name + " is targeted!");
        // Highlight tile
        // Add to targeted list
        if (this.occupiedBy != null)
        {
            targeted.Add(this.occupiedBy);
        }
    }

    void Untarget()
    {
        Debug.Log(this.name + " is no longer targeted!");
        // Remove highlight
        // Purge targeted list
        targeted.Clear();
    }
}
