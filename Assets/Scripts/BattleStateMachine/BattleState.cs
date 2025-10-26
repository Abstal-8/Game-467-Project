using System;
using UnityEngine;

public abstract class BattleState
{
    // Variables needed: player reference, enemy reference, booleans: (_isFighting, _hasEnded),
    public static bool _hasBattleStarted = false;
    public static bool _hasBattleEnded = false;

    public static Action battleStartEvent;
    //public static Action battleEnd;
    // need player turn and enemy turn (maybe utilize funcs)

    // Test Variables
    
    protected PlayerManager playerManager;
    protected UIManager uIManager;
    protected Enemy enemyReference;

    public BattleState(PlayerManager player, UIManager UI, Enemy enemy)
    {
        playerManager = player;
        uIManager = UI;
        enemyReference = enemy;
    }


    // -------------------------


    // Methods needed: update, (maybe) onCollisionEnter, 


    public abstract void EnterState(BattleStateManager battleState);
    public abstract void UpdateState(BattleStateManager battleState);
    public abstract void ExitState(BattleStateManager battleState);

}
