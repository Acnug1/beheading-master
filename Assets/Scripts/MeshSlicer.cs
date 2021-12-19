using UnityEngine;
using System.Collections.Generic;
using SliceFramework;

public class MeshSlicer : MonoBehaviour
{
    [SerializeField] private Material _sliceMaterial;
    [SerializeField] private List<GameObject> _objectsToSlice;

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        SliceFramework.Plane cuttingPlane = new SliceFramework.Plane();
        cuttingPlane.Compute(transform);
        cuttingPlane.OnDebugDraw();
    }
#endif

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionObject = other.transform.gameObject;

        if (_objectsToSlice.Contains(collisionObject))
            TryToSliceObject(collisionObject);
    }

    private void TryToSliceObject(GameObject collisionObject)
    {
        if (collisionObject && collisionObject.activeSelf)
        {
            SlicedHull hull = collisionObject.Slice(transform.position, transform.forward, _sliceMaterial);

            if (hull != null)
            {
                ToSliceObject(collisionObject, hull);
            }
        }
    }

    private void ToSliceObject(GameObject collisionObject, SlicedHull hull)
    {
        GameObject lower = hull.CreateLowerHull(collisionObject, _sliceMaterial);
        GameObject upper = hull.CreateUpperHull(collisionObject, _sliceMaterial);

        DisableObjectToSlice(collisionObject);
        AddMeshCollider(lower, upper);
        AddRigidbody(lower, upper);
    }

    private void DisableObjectToSlice(GameObject collisionObject)
    {
        collisionObject.SetActive(false);
    }

    private void AddMeshCollider(params GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            MeshCollider collider = gameObject.AddComponent<MeshCollider>();
            collider.convex = true;
        }
    }

    private void AddRigidbody(params GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.AddComponent<Rigidbody>();
        }
    }
}
