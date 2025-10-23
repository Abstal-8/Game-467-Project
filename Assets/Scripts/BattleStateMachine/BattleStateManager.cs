using Unity.VisualScripting;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{

    // Testing variables ----------------------------
    public GameObject player;
    public GameObject enemy;
    public string sceneToBattle;
    public string battleToScene;

    // ---------------------------------------------

    protected BattleState currentState;

    public PlayerTurnState playerTurn = new PlayerTurnState();
    public EnemyTurnState enemyTurn = new EnemyTurnState();
    public StartBattle startBattle = new StartBattle();
    public EndBattle endBattle = new EndBattle();
    public WinBattle winBattle = new WinBattle();
    public LoseBattle loseBattle = new LoseBattle();


    void Start()
    {
        currentState = null;
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
        currentState?.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
}
