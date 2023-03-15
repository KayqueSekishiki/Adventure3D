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


