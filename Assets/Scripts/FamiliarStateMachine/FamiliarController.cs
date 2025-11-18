using UnityEngine;

public enum FamiliarState
{
    Caged,
    Following,
    Idle,
    Special
}

public class FamiliarController : MonoBehaviour
{
    [Header("Initial State")]
    public bool startsCaged = true;

    [SerializeField]
    private FamiliarState currentState = FamiliarState.Caged;

    public FamiliarState CurrentState => currentState;
    public bool IsFree => currentState != FamiliarState.Caged;

    
    public event System.Action<FamiliarState> OnStateChanged;

    private void Start()
    {
        if (!startsCaged)
            currentState = FamiliarState.Following;

        OnStateChanged?.Invoke(currentState);
    }

    public void FreeFamiliar()
    {
        if (!IsFree)
        {
            SetState(FamiliarState.Following);
        }
    }

    public void SetIdle()
    {
        if (!IsFree) return;
        SetState(FamiliarState.Idle);
    }

    public void SetFollowing()
    {
        if (!IsFree) return;
        SetState(FamiliarState.Following);
    }

    public void EnterSpecial()
    {
        if (!IsFree) return;
        SetState(FamiliarState.Special);
    }

    private void SetState(FamiliarState newState)
    {
        if (newState == currentState) return;
        currentState = newState;
        Debug.Log($"[FamiliarController] State → {currentState}");
        OnStateChanged?.Invoke(currentState);
    }
}
