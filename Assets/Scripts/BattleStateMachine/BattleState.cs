
public abstract class BattleState
{
    // Variables needed: player reference, enemy reference, booleans: (_isFighting, _hasEnded),
    bool _hasBattleStarted;
    bool _hasBattleEnded;

    // Test Variables
    public PlayerManager playerManager;
    public Enemy enemyReference;




    // -------------------------


    // Methods needed: update, (maybe) onCollisionEnter, 


    public abstract void EnterState(BattleStateManager battleState);
    public abstract void UpdateState(BattleStateManager battleState);
    public abstract void ExitState(BattleStateManager battleState);

}
