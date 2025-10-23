using UnityEngine;

public class WinBattle : BattleState
{
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
