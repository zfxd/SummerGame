using System.Collections;
using UnityEngine;

namespace Combat
{
    public class Lose : State
    {
        Unit Enemy;

        public Lose(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log("Lose");
            // What happens when you lose?
            yield break;
        }
    }
}