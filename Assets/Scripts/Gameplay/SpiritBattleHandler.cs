using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class SpiritBattleHandler : MonoBehaviour
{
    // Battle Loop:
    /*
    > Start battle with 4 "uncharged" tokens (Charge by attacking and getting hit) <--
        > When tokens "charged" click 'Change' button to go into spirit form         |
        > Choose desired spirit to enhance abilites                                  |
        > After tokens are utilized (amount of tokens == turns in spirit form)       |
        go back to human form with a major BUFF and DEBUFF representing the stage of |
        grief of that spirit                                                         |
        > (Back to the beginning)                                                  _/

        Major Mechanics:
        1. Amount of tokens == Amount of turns in spirit form
        2. Cannot cancel out of spirit form once in (defeats purpose and infinite loop)
        3. Changing does NOT take up a turn.
        4. Buff/Debuff REFRESHES and DOES NOT APPLY while in spirit form
        5. First turn of fight there is no Stage Buff applied.
        6. Buff/Debuff stays for rest of fight until win or lose.


        IMPLEMENTATION TASKS BEFORE SLEEP:
        - Spirit Token functionality
        - Change into spirit form
        - Select spirit
        - Subsequent UI changes
        

        

    */
    public GameObject pmOBJ;
    public GameObject enemyOBJ;
    public GameObject uIOBJ;
    PlayerManager playerManager;
    Enemy enemy;
    UIManager uIManager;
    [SerializeField] List<Image> spiritTokens;

    public CurrentForm currentForm;
    int tokenIndex = 0;
    public int tokensFilled = 0;
    public bool inSpiritForm;
    public bool leaveFromSpirit;


    void Awake()
    {
        playerManager = pmOBJ.GetComponent<PlayerManager>();
        enemy = enemyOBJ.GetComponent<Enemy>();
        uIManager = uIOBJ.GetComponent<UIManager>();
        
    }

    void Start()
    {
        FlushTokens();
        currentForm = CurrentForm.HUMAN;
    }


    void Update()
    {
        switch(currentForm)
        {
            case CurrentForm.HUMAN:
                HumanState();
                break;
            case CurrentForm.SPIRIT:
                SpiritState();
                break;
        }
        
    }


    void HumanState()
    {
        if (leaveFromSpirit)
        {
            // Check which spirit and apply stage of grief buff/debuff
            // for now, hard-coded buff/debuff
        }
    }

    public void ChangeForm()
    {
        if (currentForm == CurrentForm.HUMAN)
        {
            leaveFromSpirit = false;
            currentForm = CurrentForm.SPIRIT;
            inSpiritForm = true;
        }
        else
        {
            inSpiritForm = false;
            currentForm = CurrentForm.HUMAN;
            leaveFromSpirit = true;
        }
    }

    void SpiritState()
    {
        if (inSpiritForm)
        {

        }
    }

    public void ChargeToken(int charge)
    {
        // For now flat charge rate for each token
    
        spiritTokens[tokenIndex].fillAmount += (float)(charge / 100f);
        if (!inSpiritForm && spiritTokens[tokenIndex].fillAmount >= 1)
        {
            spiritTokens[tokenIndex].color = Color.cyan;
            tokensFilled++;
            tokenIndex++;
        }

        if (tokenIndex >= 3)
        {
            tokenIndex = 3;
        }
        
    }

    public void RemoveToken()
    {
        
        if (spiritTokens[tokenIndex].fillAmount < 1 && tokenIndex > 0)
        {
            spiritTokens[tokenIndex].fillAmount = 0;
            spiritTokens[tokenIndex - 1].fillAmount = 0;
            tokenIndex--;
        }
        else
        {
            // Assuming at index = 0 and full token is realized
            spiritTokens[tokenIndex].fillAmount = 0;
        }
        spiritTokens[tokenIndex].color = Color.gray;
        tokensFilled--;

        if (tokensFilled == 0)
        {
            ChangeForm();
        }
    }

    public void FlushTokens()
    {
        foreach (Image token in spiritTokens)
        {
            token.fillAmount = 0;
            token.color = Color.gray;
        }

        tokensFilled = 0;
        tokenIndex = 0;
        leaveFromSpirit = false;
        inSpiritForm = false;
        currentForm = CurrentForm.HUMAN;
    }

    public enum CurrentForm
    {
        HUMAN,
        SPIRIT
    }

}
