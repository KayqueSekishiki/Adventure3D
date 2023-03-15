using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StateMachine<T> where T : System.Enum
{

    public Dictionary<T, StateBase> DictionaryState;

    private StateBase _currentState;
    public float timeToStartGame = 1f;

    public StateBase CurrentState
    {
        get { return _currentState; }
    }

    public void Init()
    {
        DictionaryState = new Dictionary<T, StateBase>();
    }

    public void RegisterStates(T typeEnum, StateBase state)
    {
        DictionaryState.Add(typeEnum, state);
    }

    public void SwitchStates(T state)
    {
        if (_currentState != null) _currentState.OnStateExit();

        _currentState = DictionaryState[state];

        if (_currentState != null) _currentState.OnStateEnter();
    }


    public void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }










    //#if UNITY_EDITOR
    //    #region DEBUG
    //    [Button]
    //    private void ChangeStateToStatNone()
    //    {
    //        // SwitchStates(States.NONE);

    //    }

    //    [Button]
    //    private void ChangeStateToStatNone2()
    //    {
    //        //  SwitchStates(States.NONE);

    //    }
    //    #endregion
    //#endif
}


