using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;

        if (currentState != null)
            currentState.Enter();
    }

    private void Update()
    {
        currentState?.UpdateLogic();
    }

    private void FixedUpdate()
    {
        currentState?.UpdatePhysics();
    }
}
