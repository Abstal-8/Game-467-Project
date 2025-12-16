using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class AbilityManager : MonoBehaviour
{
    public GameObject toolTip;
    public TextMeshProUGUI toolText;
    public GameObject spiritBattleHandlerOBJ;
    public GameObject uiOBJ;
    public GameObject bsmOBJ;
    SpiritBattleHandler sbh;   
    UIManager uIManager; 
    int originalDMG;
    public static Action<int> abilityBuff;
    public static Action<int> abilityReset;
    
    [SerializeField] List<Ability> playerAbilities = new List<Ability>();
    public List<Button> abillityButtons = new List<Button>();
    BattleStateManager bsm;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #if UNITY_EDITOR
        foreach (Ability obj in playerAbilities)
        {
            UnityEditor.EditorUtility.SetDirty(obj);
        }
        UnityEditor.AssetDatabase.SaveAssets(); 
        #endif


        sbh = spiritBattleHandlerOBJ.GetComponent<SpiritBattleHandler>();
        uIManager = uiOBJ.GetComponent<UIManager>();
        bsm = this.GetComponent<BattleStateManager>();
        foreach (Button btn in abillityButtons)
        {
            EventTrigger eventTrigger = btn.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry onHover = new EventTrigger.Entry() { eventID = EventTriggerType.PointerEnter };
            EventTrigger.Entry exitHover = new EventTrigger.Entry() { eventID = EventTriggerType.PointerExit };

            btn.GetComponentInChildren<TextMeshProUGUI>().text = playerAbilities[abillityButtons.IndexOf(btn)].abilityName;
            
            onHover.callback.AddListener((BaseEventData) =>
            {
                toolText.text = string.Format(playerAbilities[abillityButtons.IndexOf(btn)].abilityDescription, 
                playerAbilities[abillityButtons.IndexOf(btn)].baseDMG);
                toolTip.SetActive(true);
            });

            exitHover.callback.AddListener((BaseEventData) => { toolTip.SetActive(false); });
            btn.onClick.AddListener(() => 
            {
                BattleStateManager.playerAttack?.Invoke(playerAbilities[abillityButtons.IndexOf(btn)].baseDMG);
                uIManager.HideAbilities();
            });

            eventTrigger.triggers.Add(onHover);
            eventTrigger.triggers.Add(exitHover);
        }
    }

    void OnEnable()
    {
        abilityBuff += AbilityIncrease;
        abilityReset += AbilityReset;
    }

    void OnDisable()
    {
        abilityBuff -= AbilityIncrease;
        abilityReset -= AbilityReset;
    }

    void AbilityIncrease(int dmg)
    {
        foreach (Button btn in abillityButtons)
        {
            playerAbilities[abillityButtons.IndexOf(btn)].abilityDMG *= dmg;
        }
    }

    void AbilityReset(int dmg)
    {
        foreach (Button btn in abillityButtons)
        {
            playerAbilities[abillityButtons.IndexOf(btn)].abilityDMG /= dmg;
        }
    }




}
