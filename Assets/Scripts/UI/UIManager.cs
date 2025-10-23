using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    PlayerManager playerManager;
    int currentHealth;
    int maxHealth;

    // Battle Screen
    TextMeshProUGUI playerHealthText;
    Image playerHealthBar;

    TextMeshProUGUI enemyHealthText;
    Image enemyHealthBar;

    void Start()
    {
        currentHealth = playerManager.currentHealth;
        maxHealth = playerManager.maxHealth;
        currentHealth = maxHealth;
    }


    void UpdateHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        playerHealthText.text = currentHealth + "/" + maxHealth;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float targetfillamount = currentHealth / maxHealth;
        playerHealthBar.fillAmount = targetfillamount; // make smooth looking fill later
    }
    
    

}
