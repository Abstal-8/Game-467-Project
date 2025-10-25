using UnityEngine;

public class PlayerTurnState : BattleState
{
    public PlayerTurnState(PlayerManager player, UIManager UI, Enemy enemy) : base(player, UI, enemy)
    {
    }

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
