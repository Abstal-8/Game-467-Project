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

    public bool respawn;
    private static Enemy enemy;

    void Awake()
    {
        if (enemy == null)
        {
            enemy = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }


    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemySprite;
        if (respawn == false)
        {
            this.gameObject.SetActive(false);
        }
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
        if (other.GetComponent<PlayerManager>())
        {
            respawn = false;
        }
    }

}
