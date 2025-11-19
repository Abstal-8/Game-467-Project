using System.Collections.Generic;
using Unity.Mathematics;
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

    [SerializeField] List<GameObject> spiritTokens;
    int tokenIndex = 0;
    int tokensFilled = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject token in spiritTokens)
        {
            token.gameObject.GetComponent<Image>().fillAmount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChargeToken(int charge)
    {
        // For now flat charge rate for each token
    
        spiritTokens[tokenIndex].gameObject.GetComponent<Image>().fillAmount += (float)(charge / 100f);

        if (spiritTokens[tokenIndex].gameObject.GetComponent<Image>().fillAmount >= 1)
        {
            tokenIndex++;
            tokensFilled++;
        }
    }

}
