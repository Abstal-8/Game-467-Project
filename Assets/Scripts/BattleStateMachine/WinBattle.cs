using UnityEngine;

public class WinBattle : BattleState
{
    public WinBattle(PlayerManager player, UIManager UI, Enemy enemy) : base(player, UI, enemy)
    {
    }

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("You have won, congratulations!");
    }

    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("WinBattle state exit");
    }

    public override void UpdateState(BattleStateManager battleState)
    {
        throw new System.NotImplementedException();
    }
}
