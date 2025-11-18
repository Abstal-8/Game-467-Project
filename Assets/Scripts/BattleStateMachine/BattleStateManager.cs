using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleStateManager : MonoBehaviour
{

    // Testing variables ----------------------------
    public GameObject player;
    //public GameObject enemy;
    public string sceneToBattle;
    public string battleToScene;

    // ---------------------------------------------

    [SerializeField] PlayerManager playerManager;
    [SerializeField] UIManager uIManager;
    [SerializeField] Enemy enemyReference;

    protected BattleState currentState;

    public PlayerTurnState playerTurn;
    public EnemyTurnState enemyTurn;
    public StartBattle startBattle;
    public EndBattle endBattle;
    public WinBattle winBattle;
    public LoseBattle loseBattle;
    public DefaultBattle defaultBattle;


    void Start()
    {
        defaultBattle = new(playerManager, uIManager, enemyReference);
        startBattle = new(playerManager, uIManager, enemyReference);
        endBattle = new(playerManager, uIManager, enemyReference);
        winBattle = new(playerManager, uIManager, enemyReference);
        loseBattle = new(playerManager, uIManager, enemyReference);
        playerTurn = new(playerManager, uIManager, enemyReference);
        enemyTurn = new(playerManager, uIManager, enemyReference);

        currentState = defaultBattle;
    }

    void OnEnable()
    {
        BattleState.battleStartEvent += BattleIntialize;
    }

    void OnDisable()
    {
        BattleState.battleStartEvent -= BattleIntialize;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void BattleIntialize()
    {
        currentState = startBattle;
        currentState.EnterState(this);
    }


    public void ChangeState(BattleState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
