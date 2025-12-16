using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int currentHealth;
    public int attackDMG;

    private SpriteRenderer spriteRenderer;
    public Sprite enemySprite;
    public string enemyName;


    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemySprite;
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
