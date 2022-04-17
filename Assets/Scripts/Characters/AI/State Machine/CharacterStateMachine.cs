using UnityEngine;

[RequireComponent(typeof(Character))]

public class CharacterStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private const string ErrorMessage = "firstState is null";
    private Character _character;
    private State _currentState;

    public State CurrentState => _currentState;

    private void Awake()
    {
        Debug.Assert(_firstState != null, ErrorMessage);
        _character = GetComponent<Character>();
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_character);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_character);
    }
}