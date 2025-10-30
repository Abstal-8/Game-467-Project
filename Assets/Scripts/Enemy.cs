using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int currentHealth;


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
}
