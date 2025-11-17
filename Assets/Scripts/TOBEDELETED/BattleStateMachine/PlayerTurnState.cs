using System;
using UnityEngine;

public class PlayerTurnState : BattleState
{

    public static Action<BattleStateManager> playerTurnEndEvent;
    bool _isPlayerTurnOver;
    public PlayerTurnState(PlayerManager player, UIManager UI, Enemy enemy) : base(player, UI, enemy)
    {
        uIManager.attackButton.onClick?.AddListener(Attack);
    }

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("Player turn start!");
        uIManager.attackButton.gameObject.SetActive(true);
        uIManager.spiritButton.gameObject.SetActive(true);
    }

    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("Player turn end!");
        uIManager.attackButton.gameObject.SetActive(false);
        uIManager.spiritButton.gameObject.SetActive(false);
        _isPlayerTurnOver = false;
        // Switch to enemy turn if enemy and/or player not dead

        if (playerManager.currentHealth > 0)
        {
            battleState.ChangeState(battleState.enemyTurn);
        }
        else
        {
            battleState.ChangeState(battleState.endBattle);
        }
    }

    public override void UpdateState(BattleStateManager battleState)
    {
        if (_isPlayerTurnOver)
        {
            ExitState(battleState);
        }
    }

    void Attack()
    {
        enemyReference.TakeDamage(10); // arbitrary number
        uIManager.UpdateHealth(10, enemyReference);
        _isPlayerTurnOver = true;
    }
}
