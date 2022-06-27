using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTile : MonoBehaviour
{
    bool targeted = false;
    public GameObject occupiedBy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(targeted)
        {
            // Highlight tile
            // Show Name + HP bar
        }
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse is over " + this.name);
        // target selection
    }

    void OnMouseOver()
    {
        // While mousing over...
        // TODO: Highlight tile?
        // TODO: Show HP bar?
        // What about AoE?
        // Idea: Make a "Targeted" state for these.
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse has left " + this.name);
        // untarget
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked " + this.name);
        // confirm target selection
    }
}
