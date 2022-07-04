using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: I'm 90% sure the targeting system isn't working correctly! Figure it out.
public class BattleTile : MonoBehaviour
{
    public Unit occupiedBy;
    public BattleManager battleManager;
    HashSet<BattleTile> targeted = new HashSet<BattleTile>();

    /* // Do you need this too?
    void Target()
    {
        Debug.Log(this.name + " is targeted!");
        // Highlight tile
        this.Highlight();
        // Add to targeted list
        targeted.Add(this);
    }

    void Untarget()
    {
        Debug.Log(this.name + " is no longer targeted!");
        // Remove highlight
        this.UnHighlight();
    } */

    void Highlight()
    {
        // For easy changing of Highlight/Selection effect later on
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void UnHighlight()
    {
        // Same as above
        this.GetComponent<SpriteRenderer>().color = Color.black;
    }

    
    /* //Possibly obsolete
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

    HashSet<BattleTile> OnMouseDown()
    {
        Debug.Log("Clicked " + this.name);
        // confirm target selection
        // ONLY AVAILABLE DURING TARGET MODE?
        return targeted; // Check for whether or not any units were actually hit outside of code?

    } */

}
