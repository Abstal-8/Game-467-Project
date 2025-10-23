using UnityEngine;

public class LoseBattle : BattleState
{
    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("You have lost. Try again!");
    }

    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("LoseBattle state exit");
    }

    public override void UpdateState(BattleStateManager battleState)
    {
        throw new System.NotImplementedException();
    }
}
