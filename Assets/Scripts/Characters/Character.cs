using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    private bool _isAlive = true;
    private Transform _path;
    private Spawner _spawner;

    public event UnityAction<Character> Died;

    public bool IsAlive => _isAlive;
    public Transform Path => _path;
    public Spawner Spawner => _spawner;

    public void Init(Transform path, Spawner spawner)
    {
        _path = path;
        _spawner = spawner;
    }

    public void Die()
    {
        if (_isAlive)
        {
            _isAlive = false;
            Died?.Invoke(this);
        }
    }

    protected virtual void Update()
    {
        if (!_isAlive)
            return;

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit closestHit, 4f, NavMesh.AllAreas))
        {
            transform.position = Vector3.MoveTowards(transform.position, closestHit.position, Time.deltaTime * 2);
        }
    }
}
