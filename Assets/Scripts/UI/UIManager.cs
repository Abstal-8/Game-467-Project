using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;


public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Enemy enemy;
    int playerHealth;
    int playerMaxHealth;
    int enemyHealth;
    int enemyMaxHealth;

    

    // Battle Screen
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject enemyPanel;
    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI enemyHealthText;
    [SerializeField] public TextMeshProUGUI damageText;
    [SerializeField] Image enemyHealthBar;
    [SerializeField] Image playerHealthBar;
    [SerializeField] GameObject playerBattleSprite;
    [SerializeField] GameObject enemyBattleSprite;

    public Button attackButton;
    public Button spiritButton;

    // -----------------------------------

    void Start()
    {
        playerHealth = playerManager.currentHealth;
        playerMaxHealth = playerManager.maxHealth;

        enemyHealth = enemy.currentHealth;
        enemyMaxHealth = enemy.maxHealth;
    }


    public void UpdateHealth(int amount, Object obj)
    {
        if (obj.GetComponent<PlayerManager>())
        {
            playerHealth -= amount;
            playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
            playerHealthText.text = playerHealth + "/" + playerMaxHealth;
            UpdateHealthBar(obj);
        }
        else if (obj.GetComponent<Enemy>())
        {
            enemyHealth -= amount;
            enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);
            enemyHealthText.text = enemyHealth + "/" + enemyMaxHealth;
            UpdateHealthBar(obj);
        }
        
    }

    void UpdateHealthBar(Object obj)
    {
        if (obj.GetComponent<PlayerManager>())
        {
            float targetfillamount = (float)playerHealth / (float)playerMaxHealth;
            playerHealthBar.fillAmount = targetfillamount; // make smooth looking fill later
        }
        else if (obj.GetComponent<Enemy>())
        {
            float targetfillamount = (float)enemyHealth / (float)enemyMaxHealth;
            enemyHealthBar.fillAmount = targetfillamount; // make smooth looking fill later
        }
        else
        {
            Debug.Log("Invalid Object Reference!");
        }
        
    }

    public void InitializeBattleScreen()
    {
        playerHealthText.text = playerManager.currentHealth + "/" + playerManager.maxHealth;
        enemyHealthText.text = enemyHealth + "/" + enemyMaxHealth;

        playerHealthBar.fillAmount = playerMaxHealth;
        enemyHealthBar.fillAmount = enemyMaxHealth;

        playerPanel.SetActive(true);
        enemyPanel.SetActive(true);

        playerBattleSprite.SetActive(true);
        enemyBattleSprite.SetActive(true);

        // attackButton.gameObject.SetActive(false);
        // spiritButton.gameObject.SetActive(false);
    }

    public void DeacivateBattleScreen()
    {
        playerPanel.SetActive(false);
        enemyPanel.SetActive(false);

        playerBattleSprite.SetActive(false);
        enemyBattleSprite.SetActive(false);

        // Reset UI elements for next encounter/testing
        playerHealth = playerManager.maxHealth;
        enemyHealth = enemy.maxHealth;
        
        playerHealthBar.fillAmount = playerMaxHealth;
        enemyHealthBar.fillAmount = enemyMaxHealth;
    }



}
