using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class BattleTile : MonoBehaviour
{
    List<int> toTarget;
    public Unit occupiedBy;
    public BattleManager battleManager;
    public int id;          // Aka indice in the tiles list. Easy access

    // Start is called before the first frame update
    void Start()
    {
        toTarget = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
//        Debug.Log("Mouse is over " + this.name);
        // target selection
        // calls .Target depending on targetMode?
        switch(battleManager.targetMode)
        {
            case TargetMode.NONE:
                Debug.Log("None");
                break;
            case TargetMode.SINGLE:
                Debug.Log("Single");
                toTarget = Selection.ToTargetSingle(this.id);
                break;
            case TargetMode.ROW:
                Debug.Log("Row");
                toTarget = Selection.ToTargetRow(this.id);
                break;
            case TargetMode.COLUMN:
                Debug.Log("Column");
                toTarget = Selection.ToTargetColumn(this.id);
                break;
            case TargetMode.ALL:
                Debug.Log("All (3x3)");
                toTarget = Selection.ToTargetAll(this.id);
                break;
        }
        foreach(int id in toTarget)
        {
            battleManager.tiles[id].Target();
        }
    }


    void OnMouseExit()
    {
//        Debug.Log("Mouse has left " + this.name);
        // Remove highlights
        foreach (BattleTile tile in battleManager.targeted)
        {
            tile.Untarget();
        }
        battleManager.targeted.Clear();
        toTarget.Clear();         // Make sure Selection methods return a COPY since the list is being cleared
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





}
