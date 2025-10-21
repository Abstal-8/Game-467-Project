using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamagable
{
    int maxHealth;
    int currentHealth;

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
