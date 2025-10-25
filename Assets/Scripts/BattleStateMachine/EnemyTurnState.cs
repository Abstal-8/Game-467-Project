using UnityEngine;

public class EnemyTurnState : BattleState
{
    public EnemyTurnState(PlayerManager player, UIManager UI, Enemy enemy) : base(player, UI, enemy)
    {
    }

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("Enemy turn start!");
    }

    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("Enemy turn end!");
    }

    public override void UpdateState(BattleStateManager battleState)
    {
        throw new System.NotImplementedException();
    }
}
