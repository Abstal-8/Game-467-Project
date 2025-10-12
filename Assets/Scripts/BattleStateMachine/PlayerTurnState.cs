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
    }
}
