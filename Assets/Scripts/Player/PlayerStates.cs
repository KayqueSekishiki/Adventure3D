using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

public class PlayerStates : StateBase
{
    public class PlayerStatesIdle : StateBase
    {
      
    }

    public class PlayerStatesWalk : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
            base.OnStateEnter(o);
            Player.Instance.Move();
        }
    }

    public class PlayerStatesStop : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
            base.OnStateEnter(o);
            Player.Instance.Stop();
        }
    }

    public class PlayerStatesJump : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
            base.OnStateEnter(o);
            Player.Instance.Jump();
        }     
    }

}