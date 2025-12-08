using UnityEngine;

public class EnemyTurnState : BattleState
{

    bool _isEnemyTurnOver;
    public EnemyTurnState(PlayerManager player, UIManager UI, Enemy enemy, SpiritBattleHandler sbh) : base(player, UI, enemy, sbh)
    {
    }

    public override async void EnterState(BattleStateManager battleState)
    {
        Debug.Log("Enemy turn start!");
        enemyReference = playerManager.Enemyencounter.GetComponent<Enemy>();
        await Awaitable.WaitForSecondsAsync(2);
        EnemyAttack();
        
    }

    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("Enemy turn end!");
        _isEnemyTurnOver = false;
        if (enemyReference.currentHealth > 0)
        {
            battleState.ChangeState(battleState.playerTurn);
        }
        else
        {
            battleState.ChangeState(battleState.endBattle);
        }
    }

    public override void UpdateState(BattleStateManager battleState)
    {
        if (_isEnemyTurnOver)
        {
            ExitState(battleState);
        }

    }

    void EnemyAttack()
    {
        playerManager.TakeDamage(enemyReference.attackDMG);
        uIManager.UpdateHealth(enemyReference.attackDMG, playerManager);
        spiritBattleHandler.ChargeToken(30);    
        uIManager.damageText.text = "You Took:\n" + enemyReference.attackDMG + "\nDMG";
        _isEnemyTurnOver = true;
    }
}
