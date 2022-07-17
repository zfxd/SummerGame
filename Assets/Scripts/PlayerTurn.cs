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
            // Don't do anything if another coroutine is still busy
            yield return new WaitUntil(() => !BattleManager.busy);

            // Check if turn is over
            if (BattleManager.takingTurn.move == 0 && BattleManager.takingTurn.action == 0)
            {
                // reset values for next turn and return to TurnManager state
                BattleManager.takingTurn.move = BattleManager.takingTurn.baseMove;
                BattleManager.takingTurn.action = BattleManager.takingTurn.baseAction;
                BattleManager.SetState(new TurnManager(BattleManager));
                yield break;
            }
            BattleManager.action = Action.START;
            Debug.Log("Select an action");
            // do it here
            yield return new WaitUntil(() => BattleManager.action != Action.START);
            // SetAction method will call relevant coroutines and reset to PlayerTurn state
        }

        // Takes a targetMode
        public override IEnumerator Attack()
        {
            Debug.Log("Start attack coroutine");
            // Check if able to make an attack action rn
            if (BattleManager.takingTurn.action == 0)
            {
                Debug.Log("No more actions");
                yield break;
            }
            BattleManager.busy = true;
            BattleManager.targetMode = BattleManager.takingTurn.basicAtk;           // Set target mode

            bool valid = false;
            while (!valid)
            {
                Debug.Log("Loop");
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));      // "targeted" list updates in real time
                                                                                    // See BattleTile.cs
                                                                            // Basically it'll pull from BattleManager.targetMode
                                                                            // To determine which tiles to add to "targeted"
                // Wait for click (no escape yet)
                // Check if valid move
                if (BattleManager.targeted.Count == 0)                              // Once clicked, check that targets make sense
                                                                                    // First, check that any boxes are even selected
                {
                    Debug.Log("Invalid selection. Clicked outside a box?");
                    continue;
                }

                valid = Validate.CheckForEnemy(BattleManager.targeted);             // see Selection.cs Validate class

                if (!valid)
                    Debug.Log("No ENEMY targets within selection");
            }
            // cause all targeted targets to take damage
            bool killed = false;
            foreach (BattleTile tile in BattleManager.targeted)
            {
                Unit toHit = tile.occupiedBy;
                if (toHit != null)
                {
                    killed = toHit.TakeDamage((int)BattleManager.takingTurn.unitAtk.value);
                }
                if (killed)
                {
                    // Remove from BattleManager lists
                    BattleManager.enemyUnits.Remove(toHit);
                    BattleManager.turnOrder.Remove(toHit);
                    // if That was the last enemy, straight to win con
                    if (BattleManager.enemyUnits.Count == 0)
                    {
                        BattleManager.SetState(new Win(BattleManager));
                        yield break;
                    }
                    killed = false;
                }
            }

            // Action has been taken!
            BattleManager.takingTurn.action--;
            // Reset targetMode before we leave
            BattleManager.targetMode = TargetMode.NONE;
            BattleManager.busy = false;
        }

        public override IEnumerator Skill()
        {
            // Check if able to take action
            if (BattleManager.takingTurn.action == 0)
            {
                Debug.Log("No more actions");
                yield break;
            }
            BattleManager.busy = true;

            // TODO skill code

            // Probably set selection mode by reading from skill

            // Then handle selection code here

            // Then use a .Validate() function from skill to check that the targets selected was valid

            // The actual skill effect (damage, heal, etc) handled by calling a Skill.Function() too maybe?

            BattleManager.takingTurn.action--;
            BattleManager.targetMode = TargetMode.NONE;
            BattleManager.busy = false;
        }
        
        public override IEnumerator Move()
        {
            // TODO move code
            BattleManager.busy = true;
            // Check if able to move
            if (BattleManager.takingTurn.move == 0)
            {
                Debug.Log("No more moves");
                yield break;
            }

            // Reset values
            BattleManager.takingTurn.move--;
            BattleManager.busy = false;
            yield break;
        }

        public override IEnumerator End()
        {
            BattleManager.busy = true;
            // Depending on how Defending works, will require additional checks


            // For now this just skips your turn
            BattleManager.takingTurn.action = 0;
            BattleManager.takingTurn.move = 0;
            BattleManager.busy = false;
            Debug.Log("Turn skipped");
            yield break;
        }
    }
}