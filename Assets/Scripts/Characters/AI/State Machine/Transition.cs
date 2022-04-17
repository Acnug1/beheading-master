using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    private const string ErrorMessage = "targetState is null";

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    protected Character Character { get; private set; }

    public void Init(Character character)
    {
        Character = character;
    }

    private void Awake()
    {
        Debug.Assert(_targetState != null, ErrorMessage);
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}