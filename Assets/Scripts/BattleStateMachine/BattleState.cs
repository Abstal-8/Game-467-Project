
public abstract class BattleState
{
    // Variables needed: player reference, enemy reference, booleans: (_isFighting, _hasEnded), 

    // Methods needed: update, (maybe) onCollisionEnter, 


    public abstract void EnterState(BattleStateManager battleState);
    public abstract void ExitState(BattleStateManager battleState);

}
