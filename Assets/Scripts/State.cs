using System.Collections;
using UnityEngine;

namespace Combat
{
    public abstract class State
    {
        protected BattleManager BattleManager;

        public State(BattleManager battleManager)
        {
            BattleManager = battleManager;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }
    }
}