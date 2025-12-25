using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class SafeCrackUI : MonoBehaviour
{
    [Header("Containers")]
    public string playerTag = "Player"; //will find player so future scripts that rely on enabling or disabling player scripts can be called directly in file
    private PlayerMovement playerCon;
    public GameObject canvasContainer;
    public GameObject CrackedSafeScreen;

    [Header("Light Source (child to toggle on/off)")]
    public Light2D Glow;

    [Header("Num Parents")]
    public GameObject objectNum1;
    public GameObject objectNum2;
    public GameObject objectNum3;
    public int Num1 = 20;
    public int Num2 = 30;
    public int Num3 = 12;
    public GameObject num1Outline;
    public GameObject num2Outline;
    public GameObject num3Outline;
    public bool selected = false;
    private SpriteRenderer sr;
    private NumAdjuster na; 
    public bool inRange;

    [HideInInspector]
    private int numNum = 1;
    public bool cracked;
    public bool hasLibKey;
    private bool show = false;
    

    void Start()
    {
        if (!num1Outline) Debug.Log("num1Outline Missing on SafeCrack");
        if (!num2Outline) Debug.Log("num2Outline Missing on SafeCrack");
        if (!num3Outline) Debug.Log("num3Outline Missing on SafeCrack");
        playerCon = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerMovement>();
        if (playerCon == null) { Debug.Log("SpiritFormController Script not found on Player!");}
        na = GetComponent<NumAdjuster>();
        if (na == null) Debug.Log("NumAdjuster Component not found!");
        sr = GetComponent<SpriteRenderer>();
        if (!sr) { Debug.Log("SpriteRenderer Component not found!");}
        Glow.enabled = false;
        inRange = false;
        cracked = false;
        hasLibKey = false;
        Glow.color = Color.yellow;

        if (canvasContainer) canvasContainer.SetActive(false);
        if (CrackedSafeScreen) CrackedSafeScreen.SetActive(false);
        if (!objectNum1) Debug.Log("objectNum1 gameObject not found!");
        if (!objectNum2) Debug.Log("objectNum2 gameObject not found!");
        if (!objectNum3) Debug.Log("objectNum3 gameObject not found!");
        Set(objectNum1, Num1);
        Set(objectNum2, Num2);
        Set(objectNum3, Num3);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            inRange = true;
            if (Glow != null)
            {
                Glow.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            inRange = false;
            if (Glow != null)
            {
                Glow.enabled = false;
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown fired on SafeCrackUI");
        if (!show) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (!hit.collider) return; // nothing hit

        GameObject clickedObj = hit.collider.gameObject;
        if (clickedObj == objectNum1.transform.Find("Up").gameObject)
        {
            Debug.Log("up1 hit");
            na.Increase(objectNum1.transform.Find("Up").gameObject);
            
        }
        if (clickedObj == objectNum1.transform.Find("Down").gameObject)
        {
            na.Decrease(objectNum1.transform.Find("Down").gameObject);
            Debug.Log("down1 hit");
        }
        if (clickedObj == objectNum2.transform.Find("Up").gameObject)
        {
            na.Increase(objectNum2.transform.Find("Up").gameObject);
            Debug.Log("up2 hit");
        }
        if (clickedObj == objectNum2.transform.Find("Down").gameObject)
        {
            na.Decrease(objectNum2.transform.Find("Down").gameObject);
            Debug.Log("down2 hit");
        }
        if (clickedObj == objectNum3.transform.Find("Up").gameObject)
        {
            na.Increase(objectNum3.transform.Find("Up").gameObject);
            Debug.Log("up3 hit");
        }
        if (clickedObj == objectNum3.transform.Find("Down").gameObject)
        {
            na.Decrease(objectNum3.transform.Find("Down").gameObject);
            Debug.Log("down3 hit");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (na.GetNum(objectNum1) == 23 && na.GetNum(objectNum2) == 35 && na.GetNum(objectNum3) == 9)
            {
                cracked = true;
                hasLibKey = true;
                if (canvasContainer != null && canvasContainer.activeSelf) {
                    canvasContainer.SetActive(false);
                }
                CrackedSafeScreen.SetActive(true);
            }
        }

        // allows the user to use arrow keys to switch between the up and down arrows on the safe
        if (inRange && show) {
            if (!selected) {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (numNum == 3)
                    {
                        return;
                    }
                    else if (numNum == 2)
                    {
                        numNum = 3;
                        num2Outline.SetActive(false);
                        num3Outline.SetActive(true);
                        // num1Outline.effectColor = Color.white;
                    }
                    else if (numNum == 1)
                    {
                        numNum = 2;
                        num1Outline.SetActive(false);
                        num2Outline.SetActive(true);
                        // num2Outline.effectColor = Color.white;
                    }
                    
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (numNum == 3)
                    {
                        numNum = 2;
                        num3Outline.SetActive(false);
                        num2Outline.SetActive(true);
                        // num2Outline.effectColor = Color.white;
                    }
                    else if (numNum == 2)
                    {
                        numNum = 1;
                        num2Outline.SetActive(false);
                        num1Outline.SetActive(true);
                        // num3Outline.effectColor = Color.white;
                    }
                    else if (numNum == 1) 
                    {
                        return;
                    }
                }
            }
            else {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if (numNum == 1) {
                        na.Increase(objectNum1);
                    }
                    if (numNum == 2) {
                        na.Increase(objectNum2);
                    }
                    if (numNum == 3) {
                        na.Increase(objectNum3);
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    if (numNum == 1) {
                        na.Decrease(objectNum1);
                    }
                    if (numNum == 2) {
                        na.Decrease(objectNum2);
                    }
                    if (numNum == 3) {
                        na.Decrease(objectNum3);
                    }
                }
            }
        }
        if (show && Input.GetKeyDown(KeyCode.E))
        {
            selected = !selected;
            if (numNum == 1)
            {
                if (selected) {num1Outline.GetComponent<Outline>().effectColor = Color.yellow; }
                else {num1Outline.GetComponent<Outline>().effectColor = Color.white;}
            }
            else if (numNum == 2)
            {
                if (selected) {num2Outline.GetComponent<Outline>().effectColor = Color.yellow;}
                else {num2Outline.GetComponent<Outline>().effectColor = Color.white;}
            }
            else if (numNum == 3)
            {
                if (selected) {num3Outline.GetComponent<Outline>().effectColor = Color.yellow;}
                else {num3Outline.GetComponent<Outline>().effectColor = Color.white;}
            }
        }

        if (show && cracked)
        {
            
        }

        if (inRange && Input.GetKeyDown(KeyCode.E) && !show)
        {
            show = true;
            playerCon.LockMovement();
            sr.enabled = false;

            if (canvasContainer != null) {
                if (cracked)
                {
                    CrackedSafeScreen.SetActive(true);
                }
                else {canvasContainer.SetActive(true);}
            }
            if (Glow != null)
            {
                Glow.color = Color.white;
                Glow.enabled = false;
            }
            num3Outline.SetActive(false);
            num2Outline.SetActive(false);
            num1Outline.SetActive(true);
        }
        if (inRange && Input.GetKeyDown(KeyCode.Escape))
        {
            playerCon.UnlockMovement();
            show = false;
            sr.enabled = true;

            if (canvasContainer != null && canvasContainer.activeSelf) {
                canvasContainer.SetActive(false);
                CrackedSafeScreen.SetActive(false);
            }
            if (Glow != null)
            {
                Glow.enabled = true;
            }
        }
    }
    private void Set(GameObject parent, int newNum) {
        TextMeshProUGUI userInput = parent.GetComponentInChildren<TextMeshProUGUI>();
        if (userInput == null) {
                Debug.LogError("No TextMeshProUGUI found in parent!");
                return;
        }
        // int.TryParse(userInput.text, out number);
        // number = newNum;
        userInput.text = newNum.ToString();
    }
}
