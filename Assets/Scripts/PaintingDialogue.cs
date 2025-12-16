using UnityEngine;

public class PaintingDialogue : MonoBehaviour
{

    [SerializeField] TextAsset json;
    PopupManager popupManager;
    public GameObject popOBJ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popupManager = popOBJ.GetComponent<PopupManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (this.gameObject.GetComponent<PaintingInteractUI>().inRange ))
        {
            popupManager.InitializeDialogue(json);
            popupManager.StartDialogue();
        }
    }
}
