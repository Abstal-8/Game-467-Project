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


    private SpriteRenderer sr;
    private NumAdjuster na; 
    public bool inRange;

    [HideInInspector]
    public bool cracked;
    public bool hasLibKey;
    private bool show = false;
    

    void Start()
    {
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
        if (!show) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (!hit.collider) return; // nothing hit

        GameObject clickedObj = hit.collider.gameObject;
        if (clickedObj == objectNum1.transform.Find("Up").gameObject)
        {
            na.Increase(objectNum1.transform.Find("Up").gameObject);
        }
        else if (clickedObj == objectNum1.transform.Find("Down").gameObject)
        {
            na.Decrease(objectNum1.transform.Find("Down").gameObject);
        }
        else if (clickedObj == objectNum2.transform.Find("Up").gameObject)
        {
            na.Increase(objectNum2.transform.Find("Up").gameObject);
        }
        else if (clickedObj == objectNum2.transform.Find("Down").gameObject)
        {
            na.Decrease(objectNum2.transform.Find("Down").gameObject);
        }
        else if (clickedObj == objectNum3.transform.Find("Up").gameObject)
        {
            na.Increase(objectNum3.transform.Find("Up").gameObject);
        }
        else if (clickedObj == objectNum3.transform.Find("Down").gameObject)
        {
            na.Decrease(objectNum3.transform.Find("Down").gameObject);
        }
    }

    void Update()
    {
        //when not inRange we still want them to dissapear
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     sr.enabled = false;
        // }
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     sr.enabled = true;
        // }
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
