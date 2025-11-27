using System;
using UnityEngine;

public class PlayerTurnState : BattleState
{
    public static Action<BattleStateManager> playerTurnEndEvent;
    bool _isPlayerTurnOver;
    public PlayerTurnState(PlayerManager player, UIManager UI, Enemy enemy, SpiritBattleHandler sbh) : base(player, UI, enemy, sbh)
    {
        uIManager.attackButton.onClick?.AddListener(Attack);
        uIManager.spiritButton.onClick?.AddListener(spiritBattleHandler.ChangeForm);
        uIManager.spiritButton.interactable = false;
    }

    public override void EnterState(BattleStateManager battleState)
    {
        Debug.Log("Player turn start!");
        enemyReference = playerManager.Enemyencounter.GetComponent<Enemy>();
        uIManager.attackButton.gameObject.SetActive(true);
        uIManager.spiritButton.gameObject.SetActive(true);
    }

    public override void ExitState(BattleStateManager battleState)
    {
        Debug.Log("Player turn end!");
        uIManager.attackButton.gameObject.SetActive(false);
        uIManager.spiritButton.gameObject.SetActive(false);
        _isPlayerTurnOver = false;

        if (spiritBattleHandler.inSpiritForm)
        {
            spiritBattleHandler.RemoveToken();
        }
        else if (spiritBattleHandler.inSpiritForm && spiritBattleHandler.tokensFilled <= 0)
        {
            spiritBattleHandler.ChangeForm();
        }
 

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
        
        if (!spiritBattleHandler.inSpiritForm && spiritBattleHandler.tokensFilled > 0)
        {
            uIManager.spiritButton.interactable = true;
        }
        else
        {
            uIManager.spiritButton.interactable = false;
        }

    }

    void Attack()
    {
        enemyReference.TakeDamage(playerManager.attackDMG); // arbitrary number
        spiritBattleHandler.ChargeToken(30);
        uIManager.UpdateHealth(playerManager.attackDMG, enemyReference);
        _isPlayerTurnOver = true;
    }
}
