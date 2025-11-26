using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBattle : BattleState
{
    public EndBattle(PlayerManager player, UIManager UI, Enemy enemy, SpiritBattleHandler sbh) : base(player, UI, enemy, sbh)
    {
    }

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("You have exited battle.");
        enemyReference = playerManager.Enemyencounter.GetComponent<Enemy>();
        battleEnd?.Invoke(); // Starts playing music
        ExitState(battleState);
        
        // Check if player won or lost
        // If won, switch to win state, give player rewards earned from battle and display win screen
        // If lost, switch to lose state, 
        // show player game over screen with options to retry, load a save file, and return to main menu
    }

    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("EndBattle state exit");
        SceneSwitch.instance.LoadLevel(battleState.battleToScene);
        uIManager.DeacivateBattleScreen();
        playerManager.gameObject.SetActive(true);
        enemyReference.gameObject.SetActive(false);
        enemyReference.currentHealth = enemyReference.maxHealth;
        playerManager.currentHealth = playerManager.maxHealth;
        enemyReference = null;
        spiritBattleHandler.FlushTokens();
        // (Testing only)
        /*
            Depends on option.
            Win - after recieving rewards through menu/dialouge boxes (and/or win screen)
            -> SceneManager.LoadScene(Previous scene player was in before battle);
            -> Switch state to exploration
            -> Debug.Log("EndBattle state exit");

            Lose - depending on the option you select from the menu.
            -> Retry: Switch from lose state back to battle state and restart the instance of the battle you lost
            -> Load: Load from wherever you last saved (Load scene along with player data)
            -> Main Menu: Go back to the main menu (and most likely lose progress from where you last saved)

        */
    }

    public override void UpdateState(BattleStateManager battleState)
    {

    }
}
