using UnityEngine;

public class PlayerTurnState : BattleState
{
    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("Player turn start!");
    }

    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("Player turn end!");
        // Switch to enemy turn if enemy and/or player not dead
    }

    public override void UpdateState(BattleStateManager battleState)
    {
        throw new System.NotImplementedException();
    }
}
