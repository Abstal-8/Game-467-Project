using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : BattleState
{

    Scene prevScene;
    Vector3 prevPlayerPos;

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("You have started a battle.");
        // Saves previous scene to go back to after battle
        prevScene = SceneManager.GetActiveScene();
        // Saves player position in previous scene
        prevPlayerPos = playerManager.gameObject.transform.position;
        // Get enemy Reference
        enemyReference = playerManager.enemyEncounter;
        // Load battle scene
        SceneManager.LoadScene(battleState.sceneToBattle); //Temporary
        // Setup UI elements -> player and enemy stats and enemy reference
        
        // Call UI Manager to set up UI elements - TODO
    




    }

    public override void UpdateState(BattleStateManager battleState)
    {
        // PLAY SCENE TRANSISTION ANIMATION ALSO COULD ADD OTHER ANIMATIONS FOR UI ELEMENTS DURING BATTLE INITIALIZATION
    }


    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("StartBattle state exited.");

        // Check for turn advantage
        // Change to state based on turn advantage

        battleState.ChangeState(battleState.playerTurn);
    }

    
}
