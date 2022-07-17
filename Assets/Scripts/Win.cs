using System.Collections;
using UnityEngine;

namespace Combat
{
    public class Win : State
    {
        Unit Enemy;

        public Win(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log("Win");
            // What happens when you win?
            yield break;
        }
    }
}