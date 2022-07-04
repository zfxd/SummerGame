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
                    Player.PlayerAttack();
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
    }
}