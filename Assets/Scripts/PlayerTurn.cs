using System.Collections;
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

            // Once done, return to TurnManager
            yield return new WaitForSeconds(2f);
            BattleManager.SetState(new TurnManager(BattleManager));
        }
    }
}