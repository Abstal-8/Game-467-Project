using UnityEngine;
using Ink.Runtime;
using TMPro;

public class DialogueScript : MonoBehaviour
{

    public static DialogueScript DialougeInstance { get; private set; }
    
    private Story _currentStory;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialougePanel;


    void Awake()
    {
        if (DialougeInstance != null)
        {
            Debug.LogWarning("Multiple Instances of DialougeScript singleton!");
        }
        DialougeInstance = this;
    }

    void Start()
    {
        dialougePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void InitializeStory(TextAsset inkJSON)
    {
        _currentStory = new Story(inkJSON.text);
        dialougePanel.SetActive(true);
    }

    void ContinueStory()
    {
        if (_currentStory.canContinue)
        {
            dialogueText.text = _currentStory.Continue();
        }
        else
        {
            dialougePanel.SetActive(false);
        }
    }
    
}
