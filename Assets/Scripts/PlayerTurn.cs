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

            // Check if GAME is over
            if (BattleManager.enemyUnits.Count == 0)
            {
                BattleManager.SetState(new Win(BattleManager));
                yield break;
            }
            
            // Check if turn is over
            if (BattleManager.takingTurn.move == 0 && BattleManager.takingTurn.action == 0)
            {
                // reset values for next turn and return to TurnManager state
                BattleManager.takingTurn.move = BattleManager.takingTurn.baseMove;
                BattleManager.takingTurn.action = BattleManager.takingTurn.baseAction;
                EndTurn();
                BattleManager.SetState(new TurnManager(BattleManager));
                yield break;
            }
            BattleManager.action = Action.START;
            Debug.Log("Select an action");
            // do it here
            yield return new WaitUntil(() => BattleManager.action != Action.START);
            // SetAction method will call relevant coroutines and reset to PlayerTurn state
        }
        public override IEnumerator Attack()
        {
            Debug.Log("Start attack coroutine");
            // Check if able to take action
            if (BattleManager.takingTurn.action == 0)
            {
                Debug.Log("No more actions");
                yield break;
            }
            BattleManager.busy = true;

            // TODO skill code
            Skill skill = BattleManager.takingTurn.skill0;

            // Check cooldown

            // Set selection mode by reading from skill
            BattleManager.targetMode = skill.targetMode;

            // Then handle selection code here
            bool valid = false;
            while (!valid)
            {
                Debug.Log("Loop");
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                // Possibly implement using escape to break out of loop TODO
                // Check if valid move
                if (BattleManager.targeted.Count == 0)
                {
                    Debug.Log("Invalid selection. Clicked outside a box?");
                    continue;
                }
            // Then use a .Validate() function from skill to check that the targets selected was valid
                valid = skill.IsValid(BattleManager.targeted);
                if (!valid)
                {
                    Debug.Log("Invalid selection for " + skill.skillName);
                }
            }
            // The actual skill effect (damage, heal, etc) handled by calling a Skill.Activate()
            skill.Activate(BattleManager);

            BattleManager.takingTurn.action--;
            BattleManager.targetMode = TargetMode.NONE;
            yield break;
        }

        public override IEnumerator Skill1()
        {
            // Check if able to take action
            if (BattleManager.takingTurn.action == 0)
            {
                Debug.Log("No more actions");
                yield break;
            }
            yield break; // Temporary before implementing skills
            BattleManager.busy = true;

            // TODO skill code
            Skill skill = BattleManager.takingTurn.skill1;

            // Check cooldown

            // Set selection mode by reading from skill
            BattleManager.targetMode = skill.targetMode;

            // Then handle selection code here
            bool valid = false;
            while (!valid)
            {
                Debug.Log("Loop");
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                // Possibly implement using escape to break out of loop
                // Check if valid move
                if (BattleManager.targeted.Count == 0)
                {
                    Debug.Log("Invalid selection. Clicked outside a box?");
                    continue;
                }
            // Then use a .Validate() function from skill to check that the targets selected was valid
                valid = skill.IsValid(BattleManager.targeted);
                if (!valid)
                {
                    Debug.Log("Invalid selection for " + skill.skillName);
                }
            }
            // The actual skill effect (damage, heal, etc) handled by calling a Skill.Activate()
            skill.Activate(BattleManager);

            BattleManager.takingTurn.action--;
            BattleManager.targetMode = TargetMode.NONE;
            yield break;
        }

        public override IEnumerator Skill2()
        {
            yield break;
        }

        public override IEnumerator Skill3()
        {
            yield break;
        }

        public override IEnumerator Skill4()
        {
            yield break;
        }
        
        public override IEnumerator Move()
        {
            // Check if able to move
            if (BattleManager.takingTurn.move == 0)
            {
                Debug.Log("No more moves");
                yield break;
            }
            BattleManager.busy = true;

            // Select a tile to move to
            BattleManager.targetMode = TargetMode.SINGLE;
            bool valid = false;
            while (!valid)
            {
                Debug.Log("Loop");
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));      // "targeted" list updates in real time
                // Wait for click
                // Check if valid move
                if (BattleManager.targeted.Count == 0)                              // Once clicked, check that targets make sense
                                                                                    // First, check that any boxes are even selected
                {
                    Debug.Log("Invalid selection. Clicked outside a box?");
                    continue;
                }

                valid = Validate.IsAlly(BattleManager.targeted) && !Validate.IsOccupied(BattleManager.targeted);

                if (!valid)
                    Debug.Log("Either targeted an enemy square or an occupied square");
            }

            // TODO: Move code
            // Let's not think about units that take up multiple squares for now
            // Single tile destination
            BattleTile dest = BattleManager.targeted[0]; // Assumes only one tile targeted
            BattleTile curr = BattleManager.takingTurn.transform.parent.gameObject.GetComponent<BattleTile>();
            // Only changes location in scene. Does not change the hierarchy in any way
            BattleManager.takingTurn.transform.SetParent(dest.transform, false);
            // This should change the hierarchy!
            dest.occupiedBy = BattleManager.takingTurn;
            curr.occupiedBy = null;


            // Reset values
            BattleManager.takingTurn.move--;
            BattleManager.targetMode = TargetMode.NONE;
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
        
        /**
         * Actions to take when ending a turn.
         * Decrement cooldown etc...
         */
        void EndTurn()
        {

        }
    }
}