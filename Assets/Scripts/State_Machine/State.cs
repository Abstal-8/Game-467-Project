using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    protected GameObject owner;

    public State(GameObject owner, StateMachine stateMachine)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }
}