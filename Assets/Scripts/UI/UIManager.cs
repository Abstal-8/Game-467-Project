using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Enemy enemy;
    int playerCurrentHealth;
    int playerMaxHealth;
    int enemyHealth;
    int enemyMaxHealth;

    

    // Battle Screen
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject enemyPanel;
    TextMeshProUGUI playerHealthText;
    TextMeshProUGUI enemyHealthText;
    Image enemyHealthBar;
    Image playerHealthBar;

    // -----------------------------------

    void Start()
    {
        playerHealthText = playerPanel.GetComponent<TextMeshProUGUI>();
        enemyHealthText = enemyPanel.GetComponent<TextMeshProUGUI>();
        playerHealthBar = playerPanel.GetComponent<Image>();
        enemyHealthBar = enemyPanel.GetComponent<Image>();

        playerCurrentHealth = playerManager.currentHealth;
        playerMaxHealth = playerManager.maxHealth;
        playerCurrentHealth = playerMaxHealth;

        enemyHealth = enemy.currentHealth;
        enemyMaxHealth = enemy.maxHealth;

    }

    void OnEnable()
    {
        BattleState.battleStartEvent += InitializeBattleScreen;
    }

    void OnDisable()
    {
        BattleState.battleStartEvent -= InitializeBattleScreen;
    }


    void UpdateHealth(int amount)
    {
        playerCurrentHealth += amount;
        playerCurrentHealth = Mathf.Clamp(playerCurrentHealth, 0, playerMaxHealth);
        playerHealthText.text = playerCurrentHealth + "/" + playerMaxHealth;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float targetfillamount = playerCurrentHealth / playerMaxHealth;
        playerHealthBar.fillAmount = targetfillamount; // make smooth looking fill later
    }

    public void InitializeBattleScreen()
    {
        playerHealthText.text = playerCurrentHealth + "/" + playerMaxHealth;
        enemyHealthText.text = enemyHealth + "/" + enemyMaxHealth;

        playerHealthBar.fillAmount = playerMaxHealth;
        enemyHealthBar.fillAmount = enemyMaxHealth;

        playerPanel.SetActive(true);
        enemyPanel.SetActive(true);
        
    }



}
