using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int currentHealth;

    public Enemy Enemyencounter { get; private set; }


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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Enemyencounter = enemy;
            BattleState.battleStartEvent?.Invoke();
        }

    }
}
