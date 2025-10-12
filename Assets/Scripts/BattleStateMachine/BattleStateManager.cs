using Unity.VisualScripting;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{

    BattleState currentState;

    public EnemyTurnState enemyTurn = new EnemyTurnState();
    public PlayerTurnState playerTurn = new PlayerTurnState();

    void Start()
    {
        // playerTurn = GetComponent<PlayerTurnState>();
        // enemyTurn = GetComponent<EnemyTurnState>();

        currentState = playerTurn;
        currentState.EnterState(this); // calling current state (Player turn) EnterState method
        currentState.ExitState(this);

        currentState = enemyTurn;
        currentState.EnterState(this); // calling current state (Enemy turn) EnterState method
        currentState.ExitState(this);
    }


    public void ChangeState(BattleState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
