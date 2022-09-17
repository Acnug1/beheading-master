using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(NavMeshSurface))]

public class FragmentSplit : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private NavMeshSurface _navMeshSurface;
    private Fragment[] _fragments;

    private void Awake()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _boxCollider = GetComponent<BoxCollider>();
        EnableTrigger();
        EnableNavMeshSurface();

        _fragments = GetComponentsInChildren<Fragment>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MeshSlicer meshSlicer))
        {
            DisableTrigger();
            DisableNavMeshSurface();
            SplitFragments();
        }
    }

    private void EnableTrigger()
    {
        _boxCollider.enabled = true;
        _boxCollider.isTrigger = true;
    }

    private void DisableTrigger()
    {
        _boxCollider.enabled = false;
    }

    private void EnableNavMeshSurface()
    {
        _navMeshSurface.enabled = true;
    }

    private void DisableNavMeshSurface()
    {
        _navMeshSurface.enabled = false;
    }

    private void SplitFragments()
    {
        foreach (var fragment in _fragments)
        {
            fragment.DisableKinematic();
        }
    }
}