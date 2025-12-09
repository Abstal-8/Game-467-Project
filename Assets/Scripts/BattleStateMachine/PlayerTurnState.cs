using System;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerTurnState : BattleState
{
    bool _isPlayerTurnOver;
    public PlayerTurnState(PlayerManager player, UIManager UI, Enemy enemy, SpiritBattleHandler sbh) : base(player, UI, enemy, sbh)
    {
        uIManager.attackButton.onClick?.AddListener(UI.ShowAbilities);
        uIManager.spiritButton.onClick?.AddListener(spiritBattleHandler.ChangeForm);
        BattleStateManager.playerAttack += Attack;
        uIManager.spiritButton.interactable = false;
    }

    public override void EnterState(BattleStateManager battleState)
    {
        enemyReference = playerManager.Enemyencounter.GetComponent<Enemy>();
        uIManager.attackButton.gameObject.SetActive(true);
        uIManager.spiritButton.gameObject.SetActive(true);
    }

    public override async void ExitState(BattleStateManager battleState)
    {
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

    void Attack(int dmg)
    {
        enemyReference.TakeDamage(dmg); // arbitrary number
        spiritBattleHandler.ChargeToken(30);
        uIManager.UpdateHealth(dmg, enemyReference);
        _isPlayerTurnOver = true;
    }
}
