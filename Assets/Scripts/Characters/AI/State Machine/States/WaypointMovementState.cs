using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]

public class WaypointMovementState : State
{
    [Min(0f)]
    [SerializeField] private float _speed;

    private Animator _animator;
    private Transform[] _points;
    private int _currentPoint = 0;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        if (Character.IsAlive)
        {
            SetToMovement();
        }
    }

    private void SetToMovement()
    {
        _navMeshAgent.speed = _speed;
        _navMeshAgent.isStopped = false;
        _animator.SetFloat(AnimatorCharacterController.Params.Speed, _speed);
        _animator.Play(AnimatorCharacterController.Params.Walk);
    }

    private void OnDisable()
    {
        if (_navMeshAgent.isOnNavMesh)
            _navMeshAgent.isStopped = true;

        _animator.StopPlayback();
    }

    private void Start()
    {
        InitPoints();
    }

    private void InitPoints()
    {
        _points = new Transform[Character.Path.childCount];

        for (int i = 0; i < Character.Path.childCount; i++)
        {
            _points[i] = Character.Path.GetChild(i);
        }
    }

    private void Update()
    {
        if (Character.IsAlive)
            MoveToPoints();
        else
        if (!_navMeshAgent.isStopped)
        {
            _navMeshAgent.isStopped = true;
            _animator.StopPlayback();
        }
    }

    private void MoveToPoints()
    {
        Transform target = _points[_currentPoint];

        _navMeshAgent.destination = target.position;

        if (Vector3.Distance(transform.position, target.position) < 1)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;
        }
    }
}
