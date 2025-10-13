using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : BattleState
{

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("You have started a battle.");
        SceneManager.LoadScene(battleState.sceneToBattle);
    }


    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("StartBattle state exited.");
        // Assuming player has advantage
        battleState.ChangeState(battleState.playerTurn);
    }

    
}
