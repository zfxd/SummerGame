using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Selection : MonoBehaviour
    {
        public BattleManager BattleManager;
        public List<BattleTile> Tiles;
        List<BattleTile> targeted;

        public void Target(TargetMode mode)
        {
            switch(mode)
            {
                case TargetMode.SINGLE:
                    StartCoroutine(TargetSingle());
                    break;
            }
        }

        public IEnumerator TargetSingle()
        {

            yield break; 
            // or "yield return break;" to STOP a coroutine within a coroutine https://answers.unity.com/questions/561116/stopping-a-coroutine-within-same-function.html
        }
    }

}       