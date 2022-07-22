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

        public virtual IEnumerator Attack()
        {
            yield break;
        }

        public virtual IEnumerator Skill1()
        {
            yield break;
        }
        
        public virtual IEnumerator Skill2()
        {
            yield break;
        }

        public virtual IEnumerator Skill3()
        {
            yield break;
        }
    
        public virtual IEnumerator Skill4()
        {
            yield break;
        }

        public virtual IEnumerator Move()
        {
            yield break;
        }

        public virtual IEnumerator End()
        {
            yield break;
        }
    }
}