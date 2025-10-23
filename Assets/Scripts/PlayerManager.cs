using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    public int maxHealth { get; private set; }
    public int currentHealth { get; private set; }

    public Enemy enemyEncounter { get; private set; }


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

    void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemyEncounter = enemy;
        }
    }
}
