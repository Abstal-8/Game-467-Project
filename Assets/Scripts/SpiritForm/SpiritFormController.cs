using System;
using UnityEngine;


public enum SpiritState
{
    BodyLocked, // Tracking if body can move
    BodyFree,   // same
    SpiritActive //checking for if spirit form is active
}
public class SpiritFormController : MonoBehaviour
{
    //State Machine for spirit form

    [Header("UI")]
    public GameObject spiritTutorialPanel;

    [Header("Initial Setup")]
    [Tooltip("Should the body start chained/immobilized on the table?")]
    public bool lockBodyAtStart = true;

    //tracking currect state of state machine
    [SerializeField]
    private SpiritState currentState = SpiritState.BodyLocked;


    public SpiritState CurrentState => currentState;
    public bool IsInSpirit => currentState == SpiritState.SpiritActive;
    public bool IsBodyLocked => currentState == SpiritState.BodyLocked;
    public bool IsBodyFree => currentState == SpiritState.BodyFree;

    //checks for your physical body can move, may add this somewhere else since not related to spirit form
    public bool HasMovementUnlocked { get; private set; } = false; 

    //checks if you can swap places yet, currently unlocked right after you free yourself may change later
    public bool HasSwapUnlocked { get; private set; } = true;
    //Lets other scripts subscribe to spiritstate youtube channel so they can know when currentstate changes
    public event Action<SpiritState> OnStateChanged;
    //Just used for error handling
    [Header("Debug/Input")]
    [Tooltip("Key to toggle spirit on/off while testing.")]
    public KeyCode toggleSpiritKey = KeyCode.T;
    void Start()
    {
        //deciding the starting state for spirit form
        if (lockBodyAtStart)
        {
            currentState = SpiritState.BodyLocked;
            HasMovementUnlocked = false;
        }
        else
        {
            currentState = SpiritState.BodyFree;
            HasMovementUnlocked = true;
        }

        // Notify subscribers a new state dropped
        OnStateChanged?.Invoke(currentState);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(toggleSpiritKey))
        {
            if (IsInSpirit)
                ExitSpirit();
            else
                EnterSpirit();
          
        }
    }

    //Public APIs 
    public void EnterSpirit()
    {
        //Only lets you enter if you're not already in spirit mode
        if (currentState == SpiritState.SpiritActive)

            return;

        SetState(SpiritState.SpiritActive);

        // HIDE TUTORIAL once we successfully enter spirit form
        if (spiritTutorialPanel != null)
            spiritTutorialPanel.SetActive(false);
    }



    public void ExitSpirit()
    {
        if (currentState != SpiritState.SpiritActive)
            return; 

        
        if (HasMovementUnlocked)
            SetState(SpiritState.BodyFree);
        else
            SetState(SpiritState.BodyLocked);
    }
    public void UnlockMovement()
    {
        HasMovementUnlocked = true;

        
        if (currentState == SpiritState.BodyLocked)
        {
            SetState(SpiritState.BodyFree);
        }
        
    }
    public void UnlockSwap()
    {
        HasSwapUnlocked = true;
    }
    //actually changes the state, all state changes need to call this method
    private void SetState(SpiritState newState)
    {
        if (newState == currentState) return;

        currentState = newState;

        if (newState == SpiritState.SpiritActive)
            if (spiritTutorialPanel != null)
                spiritTutorialPanel.SetActive(false);

        OnStateChanged?.Invoke(currentState);
    }
}

    

