using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class SafeCrackUI : MonoBehaviour
{
    [Header("Containers")]
    public string playerTag = "Player"; //will find player so future scripts that rely on enabling or disabling player scripts can be called directly in file
    private PlayerMovement playerCon;
    public GameObject canvasContainer;

    [Header("Light Source (child to toggle on/off)")]
    public Light2D Glow;

    [Header("Num Parents")]
    public GameObject objectNum1;
    public GameObject objectNum2;
    public GameObject objectNum3;
    public int Num1 = 20;
    public int Num2 = 30;
    public int Num3 = 12;
    public GameObject up1outline;
    public GameObject down1outline;
    public GameObject up2outline;
    public GameObject down2outline;
    public GameObject up3outline;
    public GameObject down3outline;



    private SpriteRenderer sr;
    private NumAdjuster na; 
    public bool inRange;

    [HideInInspector]
    private bool numPosNeg = true;
    private int numNum = 1;
    public bool cracked;
    public bool hasLibKey;
    private bool show = false;
    

    void Start()
    {
        if (up1outline) up1outline.enabled(false);
        if (down1outline) down1outline.enabled(false);
        if (up2outline) up2outline.enabled(false);
        if (down2outline) down2outline.enabled(false);
        if (up3outline) up3outline.enabled(false);
        if (down3outline) down3outline.enabled(false);
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
        if (na.GetNum(objectNum1) == 23 && na.GetNum(objectNum2) == 35 && na.GetNum(objectNum3) == 9)
        {
            cracked = true;
            hasLibKey = true;
            if (canvasContainer != null && canvasContainer.activeSelf) {
                canvasContainer.SetActive(false);
            }
            if (Glow != null)
            {
                Glow.enabled = true;
            }
        }
        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     sr.enabled = false;
        // }

        if (inRange && show)
        {
            if (Inputer.GetKeyDown(Keycode.W))
            {
                if (numPosNeg)
                {
                    if (numNum == 1)
                    {
                        if (up1outline) {
                            up1outline.enabled(true);
                            up1outline.effectColor = Color.white;
                            }
                    }
                    if (numNum == 2)
                    {
                        if (up2outline) up2outline.enabled(true);
                    }
                    else
                    {
                        if (up3outline) up3outline.enabled(true);
                    }
                }
                else
                {
                    if (numNum == 1)
                    {
                        if (down1outline) down1outline.enabled(true);
                    }
                    if (numNum == 2)
                    {
                        if (down2outline) down2outline.enabled(true);
                    }
                    else
                    {
                        if (down3outline) down3outline.enabled(true);
                    }
                }
            }
        }
        if (inRange && show && Input.GetKeyDown(KeyCode.E))
        {
            
        }

        if (inRange && Input.GetKeyDown(KeyCode.E) && !show)
        {
            show = true;
            playerCon.LockMovement();
            sr.enabled = false;

            if (canvasContainer != null) {
                canvasContainer.SetActive(true);
            }
            if (Glow != null)
            {
                Glow.color = Color.white;
                Glow.enabled = false;
            }
        }
        if (inRange && Input.GetKeyDown(KeyCode.Escape))
        {
            playerCon.UnlockMovement();
            show = false;
            sr.enabled = true;

            if (canvasContainer != null && canvasContainer.activeSelf) {
                canvasContainer.SetActive(false);
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
