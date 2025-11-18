using UnityEngine;

public class EnemyTurnState : BattleState
{

    bool _isEnemyTurnOver;
    public EnemyTurnState(PlayerManager player, UIManager UI, Enemy enemy) : base(player, UI, enemy)
    {
    }

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("Enemy turn start!");
        
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
        else
        {
            EnemyAttack();
        }
    }

    void EnemyAttack()
    {
        int dmg = Random.Range(1, 10);
        
        playerManager.TakeDamage(dmg);
        uIManager.UpdateHealth(dmg, playerManager);
        uIManager.damageText.text = "You Took:\n" + dmg + "\nDMG";
        _isEnemyTurnOver = true;
    }
}
