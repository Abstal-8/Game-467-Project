using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int maxHealth { get; private set; }
    public int currentHealth { get; private set; }
    
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
