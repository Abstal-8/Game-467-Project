using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    [SerializeField] GameObject buffPanel;
    [SerializeField] GameObject buffToolTip;

    public CurrentForm currentForm;
    public Stage stage;
    int tokenIndex = 0;
    public int tokensFilled = 0;
    public bool inSpiritForm;
    public bool leaveFromSpirit;
    public float dmgMultiplier;
    int spiritBuff;


    void Awake()
    {
        playerManager = pmOBJ.GetComponent<PlayerManager>();
        enemy = enemyOBJ.GetComponent<Enemy>();
        uIManager = uIOBJ.GetComponent<UIManager>();
        
    }

    void Start()
    {
        FlushTokens();

        EventTrigger eventTrigger = buffPanel.AddComponent<EventTrigger>();
        EventTrigger.Entry buffHover = new EventTrigger.Entry() { eventID = EventTriggerType.PointerEnter };
        EventTrigger.Entry buffHoverExit = new EventTrigger.Entry() { eventID = EventTriggerType.PointerExit };

        buffHover.callback.AddListener((BaseEventData) => { buffToolTip.SetActive(true); });
        buffHoverExit.callback.AddListener((BaseEventData) => { buffToolTip.SetActive(false); });

        eventTrigger.triggers.Add(buffHover);
        eventTrigger.triggers.Add(buffHoverExit);
    }



    public void ChangeForm()
    {
        if (currentForm == CurrentForm.HUMAN) // going INTO spirit form
        {
            leaveFromSpirit = false;
            currentForm = CurrentForm.SPIRIT;
            stage = Stage.NONE;
            RemoveStage();
            inSpiritForm = true;
        }
        else // LEAVING spirit form
        {
            inSpiritForm = false;
            currentForm = CurrentForm.HUMAN;
            stage = Stage.DEPRESSION;
            ApplyStage();
            leaveFromSpirit = true;
        }
    }

    void ApplyStage()
    {
        // For now buff text is hard-coded, will change later (maybe)
        buffToolTip.GetComponentInChildren<TextMeshProUGUI>().text = "Stage: DEPRESSION\nApplies a 5% DMG increase for player and a 50% DMG increase for enemy.";
        buffPanel.SetActive(true);
        AbilityManager.abilityReset(2);
    }

    void RemoveStage()
    {
        // Removing text for potential change in selected spirit
        buffPanel.SetActive(false);
        buffToolTip.GetComponentInChildren<TextMeshProUGUI>().text = "";
        AbilityManager.abilityBuff(2);
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
        stage = Stage.NONE;
        buffPanel.SetActive(false);
        buffToolTip.GetComponentInChildren<TextMeshProUGUI>().text = "";

    }

    public enum CurrentForm
    {
        HUMAN,
        SPIRIT
    }

    public enum Stage
    {
        NONE,
        DEPRESSION
    }

    public enum Spirit
    {
        NONE,
        CAT
    }

}
