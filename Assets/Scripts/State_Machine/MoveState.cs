using UnityEngine;

public class MoveState : State
{
    PlayerMotor motor;

    public MoveState(GameObject owner, StateMachine sm) : base(owner, sm)
    {
        motor = owner.GetComponent<PlayerMotor>();
    }

    public override void UpdateLogic()
    {
        Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input == Vector2.zero)
            stateMachine.ChangeState(new IdleState(owner, stateMachine));
    }

    public override void UpdatePhysics()
    {
        Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        motor.Move(input);
    }
}