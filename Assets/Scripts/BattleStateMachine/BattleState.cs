using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public abstract class BattleState
{
    // Variables needed: player reference, enemy reference, booleans: (_isFighting, _hasEnded),
    public static bool _hasBattleStarted = false;
    public static bool _hasBattleEnded = false;

    public static Action battleStartEvent;
    public static Action battleEnd;
    // need player turn and enemy turn (maybe utilize funcs)

    // Test Variables
    protected Scene prevScene;
    // protected Vector3 prevPlayerPos; for some reason the player keeps og position without this var ???
    
    protected PlayerManager playerManager;
    protected UIManager uIManager;
    protected Enemy enemyReference;
    protected SpiritBattleHandler spiritBattleHandler;

    public BattleState(PlayerManager player, UIManager UI, Enemy enemy, SpiritBattleHandler sbh)
    {
        playerManager = player;
        uIManager = UI;
        enemyReference = enemy;
        spiritBattleHandler = sbh;
    }


    // -------------------------


    // Methods needed: update, (maybe) onCollisionEnter, 


    public abstract void EnterState(BattleStateManager battleState);
    public abstract void UpdateState(BattleStateManager battleState);
    public abstract void ExitState(BattleStateManager battleState);

}
