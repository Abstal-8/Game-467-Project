using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(PlayerMotor))]
public class Player : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Player.Start()");
        var sm = GetComponent<StateMachine>();
        sm.ChangeState(new IdleState(gameObject, sm));
    }
}