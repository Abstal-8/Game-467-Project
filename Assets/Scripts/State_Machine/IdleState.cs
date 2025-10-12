// IdleState.cs
using UnityEngine;

public class IdleState : State
{
    PlayerMotor motor;

    public IdleState(GameObject owner, StateMachine sm) : base(owner, sm)
    {
        motor = owner.GetComponent<PlayerMotor>();
    }

    public override void Enter() => motor.Stop();

    public override void UpdateLogic()
    {
        Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input != Vector2.zero)
            stateMachine.ChangeState(new MoveState(owner, stateMachine));
    }
}
