using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int currentHealth;
    public int attackDMG;

    //public Sprite sprite;
    public string enemyName;


    void Start()
    {
        currentHealth = maxHealth;
       // sprite = this.GetComponent<SpriteRenderer>().sprite;
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
