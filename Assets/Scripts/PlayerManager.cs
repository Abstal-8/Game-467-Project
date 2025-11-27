using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int currentHealth;
    public int attackDMG;

    public GameObject Enemyencounter { get; private set; }


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            currentHealth = 0;
        }

    }

    void OnEnable()
    {
        BattleState.battleEnd += ResetEnemy;
    }

    void OnDisable()
    {
        BattleState.battleEnd -= ResetEnemy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>())
        {
            Enemyencounter = other.gameObject;
            BattleState.battleStartEvent?.Invoke();
        }
    }

    public void ResetEnemy()
    {
        Enemyencounter = null;
    }
}
