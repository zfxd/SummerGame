using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class PlayerTurn : State
    {
        Unit Player;

        public PlayerTurn(BattleManager battleManager, Unit player) : base(battleManager)
        {
//            BattleManager = battleManager;  // Not sure if this is redundant
                                            // Seemed usable even without it
                                            // Possibly due to the default constructor? idk
            Player = player;
        }

        public override IEnumerator Start()
        {
            Debug.Log("PlayerTurn");
            BattleManager.action = Action.START;
            Debug.Log("Select an action");
            // do it here
            yield return new WaitUntil(() => BattleManager.action != Action.START);
            switch(BattleManager.action)
            {
                case Action.ATK:
                    Debug.Log("Attack");
            // TODO: How do I call a coroutine from here?
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
            // TODO: How do I check that I'm done?
            BattleManager.SetState(new TurnManager(BattleManager));
        }

        // Takes a targetMode
        public IEnumerator PlayerAttack(TargetMode mode)
        {
            Debug.Log("Start attack coroutine");
            yield return new WaitForSeconds(2f);
            BattleManager.targetMode = mode;
            bool valid = false;
            while (!valid)
            {
                Debug.Log("Loop");
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                // Wait for click (no escape yet)
                // Check if valid move
                if (BattleManager.targeted.Count == 0)
                {
                    Debug.Log("Invalid selection. Clicked outside a box?");
                    continue;
                }
                // If no units inside selection?
                // Passed all the tests, it is valid
                valid = true;
            }
            // cause all targeted targets to take damage

            // Reset before we leave
            BattleManager.targetMode = TargetMode.NONE;
            yield break;
        }
    }
}