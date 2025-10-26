using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int maxHealth { get; private set; }
    public int currentHealth { get; private set; }


    void Start()
    {
        maxHealth = 100;
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
}
