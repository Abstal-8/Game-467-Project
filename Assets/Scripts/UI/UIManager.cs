using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    [SerializeField] Image enemyHealthBar;
    [SerializeField] Image playerHealthBar;

    // -----------------------------------

    void Start()
    {
        playerHealth = playerManager.currentHealth;
        playerMaxHealth = playerManager.maxHealth;

        enemyHealth = enemy.currentHealth;
        enemyMaxHealth = enemy.maxHealth;

    }

    // void Update()
    // {
    //     // playerHealth = playerManager.currentHealth;
    //     // playerMaxHealth = playerManager.maxHealth;
    //     // enemyHealth = enemy.currentHealth;
    // }


    void UpdateHealth(int amount)
    {
        playerHealth -= amount;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
        playerHealthText.text = playerHealth + "/" + playerMaxHealth;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float targetfillamount = playerHealth / playerMaxHealth;
        playerHealthBar.fillAmount = targetfillamount; // make smooth looking fill later
    }

    public void InitializeBattleScreen()
    {
        playerHealthText.text = playerManager.currentHealth + "/" + playerManager.maxHealth;
        enemyHealthText.text = enemyHealth + "/" + enemyMaxHealth;

        playerHealthBar.fillAmount = playerMaxHealth;
        enemyHealthBar.fillAmount = enemyMaxHealth;

        playerPanel.SetActive(true);
        enemyPanel.SetActive(true);
        
    }



}
