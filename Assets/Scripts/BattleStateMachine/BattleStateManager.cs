using Unity.VisualScripting;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{

    // Testing variables ----------------------------
    public GameObject player;
    public GameObject enemy;
    public string sceneToBattle;
    public string battleToScene;
    Collider2D playerCollider;
    Collider2D enemyCollider;

    // ---------------------------------------------

    BattleState currentState;

    public PlayerTurnState playerTurn = new PlayerTurnState();
    public EnemyTurnState enemyTurn = new EnemyTurnState();
    public StartBattle startBattle = new StartBattle();
    public EndBattle endBattle = new EndBattle();
    public WinBattle winBattle = new WinBattle();
    public LoseBattle loseBattle = new LoseBattle();


    void Start()
    {
        playerCollider = player.GetComponent<Collider2D>();
        enemyCollider = enemy.GetComponent<Collider2D>();

        // currentState = playerTurn;
        // currentState.EnterState(this); // calling current state (Player turn) EnterState method
        // currentState.ExitState(this);

        // currentState = enemyTurn;
        // currentState.EnterState(this); // calling current state (Enemy turn) EnterState method
        // currentState.ExitState(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentState = startBattle;
            currentState.EnterState(this);
            currentState.ExitState(this);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = endBattle;
            currentState.EnterState(this);
            currentState.ExitState(this);
        }
    }


    public void ChangeState(BattleState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
