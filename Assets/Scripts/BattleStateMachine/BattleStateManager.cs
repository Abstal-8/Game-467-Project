using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

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
    Enemy enemyReference;
    [SerializeField] SpiritBattleHandler sbh;
    public static Action<int> playerAttack;

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
        defaultBattle = new(playerManager, uIManager, enemyReference, sbh);
        startBattle = new(playerManager, uIManager, enemyReference, sbh);
        endBattle = new(playerManager, uIManager, enemyReference, sbh);
        winBattle = new(playerManager, uIManager, enemyReference, sbh);
        loseBattle = new(playerManager, uIManager, enemyReference, sbh);
        playerTurn = new(playerManager, uIManager, enemyReference, sbh);
        enemyTurn = new(playerManager, uIManager, enemyReference, sbh);

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
