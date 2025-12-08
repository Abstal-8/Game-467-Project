using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public GameObject toolTip;
    public TextMeshProUGUI toolText;
    public GameObject spiritBattleHandlerOBJ;
    public GameObject uiOBJ;
    public GameObject bsmOBJ;
    SpiritBattleHandler sbh;   
    UIManager uIManager; 
    int multipliedAbilityDMG;
    public List<Ability> playerAbilities = new List<Ability>();
    public List<Button> abillityButtons = new List<Button>();
    BattleStateManager bsm;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                playerAbilities[abillityButtons.IndexOf(btn)].abilityDMG);
                toolTip.SetActive(true);
            });

            exitHover.callback.AddListener((BaseEventData) => { toolTip.SetActive(false); });
            btn.onClick.AddListener(() => 
            {
                BattleStateManager.playerAttack?.Invoke(playerAbilities[abillityButtons.IndexOf(btn)].abilityDMG);
                uIManager.HideAbilities();
            });

            eventTrigger.triggers.Add(onHover);
            eventTrigger.triggers.Add(exitHover);
        }
    }




}
