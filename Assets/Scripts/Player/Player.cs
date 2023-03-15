using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;
using NaughtyAttributes;

public class Player : Singleton<Player>
{
    public enum PlayerStates
    {
        IDLE,
        WALK,
        STOP,
        JUMP
    }

    public StateMachine<PlayerStates> stateMachine;

    public float speed;
    public float jumpForce;
    private Rigidbody _rigidbody;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.IDLE, new global::PlayerStates.PlayerStatesIdle());
        stateMachine.RegisterStates(PlayerStates.WALK, new global::PlayerStates.PlayerStatesWalk());
        stateMachine.RegisterStates(PlayerStates.STOP, new global::PlayerStates.PlayerStatesStop());
        stateMachine.RegisterStates(PlayerStates.JUMP, new global::PlayerStates.PlayerStatesJump());

        stateMachine.SwitchStates(PlayerStates.IDLE);


        _rigidbody = GetComponent<Rigidbody>();






    }

    public void Move()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, speed);
    }

    public void Stop()
    {
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y);
    }
    public void Jump()
    {
        _rigidbody.velocity = jumpForce * Vector2.up;
    }





#if UNITY_EDITOR
    #region DEBUG
    [Button]
    private void ChangeStateToStateIdle()
    {
        stateMachine.SwitchStates(PlayerStates.IDLE);
    }

    [Button]
    private void ChangeStateToStateWalk()
    {
        stateMachine.SwitchStates(PlayerStates.WALK);
    }

    [Button]
    private void ChangeStateToStateStop()
    {
        stateMachine.SwitchStates(PlayerStates.STOP);
    }

    [Button]
    private void ChangeStateToStateJump()
    {
        stateMachine.SwitchStates(PlayerStates.JUMP);
    }
    #endregion
#endif
}


