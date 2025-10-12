using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected Animator animationController;
    protected string animationName;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, Animator _animationController, string _animationName)
    {

        player = _player;
        stateMachine = _stateMachine;
        animationController = _animationController;
        animationName = _animationName;
    }
}
