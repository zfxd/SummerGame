using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleManager battleManager) : base(battleManager)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log("PlayerTurn");
            // Do stuff
            BattleManager.action = Action.START;
            Debug.Log("Select an action");
            // do it here
            yield return new WaitUntil(() => BattleManager.action != Action.START);
            switch(BattleManager.action)
            {
                case Action.ATK:
                    Debug.Log("Attack");
                    break;
                case Action.SKILL:
                    Debug.Log("Skill");
                    break;
                case Action.MOVE:
                    Debug.Log("Move");
                    break;
                case Action.END:
                    Debug.Log("End");
                    break;
            }


            // Once done, return to TurnManager
            BattleManager.SetState(new TurnManager(BattleManager));
        }

        HashSet<Unit> PlayerTarget()
        {
            return null;
        }

        void PlayerAttack()
        {
        }

        void PlayerSkill()
        {

        }

        void PlayerMove()
        {

        }

        void PlayerEnd()
        {

        }
    }
}