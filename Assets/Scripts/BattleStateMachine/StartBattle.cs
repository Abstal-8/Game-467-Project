using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : BattleState
{

    Scene prevScene;
    Vector3 prevPlayerPos;


    public StartBattle(PlayerManager player, UIManager UI, Enemy enemy) : base(player, UI, enemy) { }

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("You have started a battle.");
        // Saves previous scene to go back to after battle
        prevScene = SceneManager.GetActiveScene();
        // Saves player position in previous scene
        prevPlayerPos = battleState.player.transform.position;
        // Get enemy Reference
        enemyReference = playerManager.Enemyencounter;
        // Load battle scene
        SceneManager.LoadScene(battleState.sceneToBattle);
        Debug.Log("Prev Pos: " + prevPlayerPos);
        Debug.Log("Prev scene: " + prevScene.name);
        Debug.Log(enemyReference + " " + enemyReference.name);

    }

    public override void UpdateState(BattleStateManager battleState)
    {
    }


    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("StartBattle state exited.");

        battleStartEvent?.Invoke();

        // Check for turn advantage
        // Change to state based on turn advantage

        battleState.ChangeState(battleState.playerTurn);
    }

    
}
