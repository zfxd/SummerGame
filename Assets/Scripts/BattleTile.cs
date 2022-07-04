using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: I'm 90% sure the targeting system isn't working correctly! Figure it out.
public class BattleTile : MonoBehaviour
{
    public Unit occupiedBy;
    public BattleManager battleManager;


    void OnMouseEnter()
    {
        Debug.Log("Mouse is over " + this.name);
        // target selection
        // calls .Target depending on targetMode?
        switch(battleManager.targetMode)
        {
            case TargetMode.NONE:
                Debug.Log("None");
                break;
            case TargetMode.SINGLE:
                Debug.Log("Single");
                this.Target();
                break;
        }
    }


    void OnMouseExit()
    {
        Debug.Log("Mouse has left " + this.name);
        // Remove highlights
        foreach (BattleTile tile in battleManager.targeted)
        {
            tile.Untarget();
        }
        battleManager.targeted.Clear();
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked " + this.name);
        // confirm target selection
        // ONLY AVAILABLE DURING TARGET MODE?
    }

    void Target()
    {
        Debug.Log(this.name + " is targeted!");
        // Highlight tile
        this.Highlight();
        // Add to targeted list
        battleManager.targeted.Add(this);
    }

    void Untarget()
    {
        Debug.Log(this.name + " is no longer targeted!");
        // Remove highlight
        this.UnHighlight();
    }

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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
