using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StateMachine : MonoBehaviour
{
    public enum States
    {
        NONE,
    }


    public Dictionary<States, StateBase> DictionaryState;

    private StateBase _currentState;
    public float timeToStartGame = 1f;

    private void Awake()
    {
        DictionaryState = new Dictionary<States, StateBase>();
        DictionaryState.Add(States.NONE, new StateBase());

        SwitchStates(States.NONE);

        Invoke(nameof(StartGame), timeToStartGame);
    }

    [Button]
    private void StartGame()
    {
        SwitchStates(States.NONE);
    }

#if UNITY_EDITOR
    #region DEBUG
    [Button]
    private void ChangeStateToStatNone()
    {
        SwitchStates(States.NONE);

    }

    [Button]
    private void ChangeStateToStatNone2()
    {
        SwitchStates(States.NONE);

    }
    #endregion
#endif

    public void SwitchStates(States state)
    {
        if (_currentState != null) _currentState.OnStateExit();

        _currentState = DictionaryState[state];

        if (_currentState != null) _currentState.OnStateEnter();
    }


    private void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();

        if (Input.GetKeyDown(KeyCode.O))
        {
            // SwitchStates(States.DEAD);
        }
    }
}


