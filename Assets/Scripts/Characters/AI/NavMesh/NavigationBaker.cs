using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    [SerializeField] private NavMeshSurface[] _surfaces;

    private void Awake()
    {
        for (int i = 0; i < _surfaces.Length; i++)
        {
            _surfaces[i].BuildNavMesh();
        }
    }

    private void Update()
    {
        for (int i = 0; i < _surfaces.Length; i++)
        {
            _surfaces[i].UpdateNavMesh(_surfaces[i].navMeshData);
        }
    }
}