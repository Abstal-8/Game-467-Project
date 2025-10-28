using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : BattleState
{



    public static Action<BattleStateManager> startStateEndEvent;


    public StartBattle(PlayerManager player, UIManager UI, Enemy enemy) : base(player, UI, enemy)
    {

        
    }

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("You have started a battle.");
        // Saves previous scene to go back to after battle
        prevScene = SceneManager.GetActiveScene();
        // Saves player position in previous scene
        prevPlayerPos = battleState.player.transform.position;
        // Get enemy Reference
        enemyReference = playerManager.Enemyencounter;
        battleState.battleToScene = prevScene.name;
        // Load battle scene
        SceneSwitch.instance.LoadLevel(battleState.sceneToBattle);
        uIManager.InitializeBattleScreen();

        startStateEndEvent += ExitState;
        startStateEndEvent(battleState);
    }

    public override void UpdateState(BattleStateManager battleState)
    {
    }


    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("StartBattle state exited.");
        Debug.Log("Prev player pos: " + prevPlayerPos);

        // Check for turn advantage
        // Change to state based on turn advantage
        startStateEndEvent -= ExitState;
        battleState.ChangeState(battleState.playerTurn);
    }

    
}
