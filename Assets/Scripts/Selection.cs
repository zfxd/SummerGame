using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    // helper methods that take an ID (that the mouse is over) and return the IDs to target depending
    // on selection mode
    // Used in BattleTile.cs
    public static class Selection
    {
        static List<int>[] Rows = {new List<int>{0,3,6}, new List<int>{1,4,7}, new List<int>{2,5,8},
                                   new List<int>{9,12,15}, new List<int>{10,13,16}, new List<int>{11,14,17}};
        static List<int>[] Columns = {new List<int>{0,1,2}, new List<int>{3,4,5}, new List<int>{6,7,8},
                                   new List<int>{9,10,11}, new List<int>{12,13,14}, new List<int>{15,16,17}};

        static List<int>[] All = {new List<int>{0,1,2,3,4,5,6,7,8}, new List<int>{9,10,11,12,13,14,15,16,17}};

        public static List<int> ToTargetSingle(int id)
        {
            return new List<int>() {id};
        }

        public static List<int> ToTargetRow(int id)
        {
            Debug.Log("id is" + id);
            foreach (List<int> row in Rows)
            {
                if(row.Contains(id))
                return new List<int>(row);
            }
            Debug.Log("Uh oh this should never be reached");    
            return null;
        }

        public static List<int> ToTargetColumn(int id)
        {
            Debug.Log("id is" + id);
            foreach (List<int> col in Columns)
            {
                if(col.Contains(id))
                return new List<int>(col);
            }
            Debug.Log("Uh oh this should never be reached");    
            return null;
        }

        public static List<int> ToTargetAll(int id)
        {
            Debug.Log("id is" + id);
            foreach (List<int> square in All)
            {
                if(square.Contains(id))
                return new List<int>(square);
            }
            Debug.Log("Uh oh this should never be reached");    
            return null;
        }

        // Overlapping targeting areas gonna get wacky :D        
    }

    // Helper methods to validate that target selections make sense
    // Used in PlayerTurn.cs
    public static class Validate
    {
        /**
         *Given a list of targeted BattleTiles, check that at least one enemy is present
         * Uses ID 
        **/
        public static bool CheckForEnemy(List<BattleTile> tiles)
        {
            foreach(BattleTile tile in tiles)
            {
                // at least 1 ENEMY target hit
                if (tile.occupiedBy != null && tile.id > 8)
                {
                    return true;
                }
            }
            // If not, invalid
            return false;
        }

        public static bool CheckForAlly(List<BattleTile> tiles)
        {
            foreach(BattleTile tile in tiles)
            {
                // at least 1 ALLY target hit
                if (tile.occupiedBy != null && tile.id < 9)
                {
                    return true;
                }
            }
            // If not, invalid
            return false;
        }

    }

}       