using UnityEngine;
using SliceFramework;

public class MeshSlicer : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        SliceFramework.Plane cuttingPlane = new SliceFramework.Plane();
        cuttingPlane.Compute(transform);
        cuttingPlane.OnDebugDraw();
    }
#endif

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.transform.root.gameObject;
        ContactPoint contactPoint = collision.GetContact(0);

        if (!IsExplosionHazardObject(collisionObject))
            TryTakeObjectToSlice(collisionObject, contactPoint);
    }

    private bool IsExplosionHazardObject(GameObject collisionObject)
    {
        if (collisionObject.TryGetComponent(out Explosion explosion))
        {
            ExlpodeObject(explosion);
            return true;
        }
        else
            return false;
    }

    private void ExlpodeObject(Explosion explosion) => explosion.ExplodeWithDelay();

    private void TryTakeObjectToSlice(GameObject collisionObject, ContactPoint contactPoint)
    {
        if (collisionObject.TryGetComponent(out Sliceable sliceable))
            TryToSliceObject(collisionObject, sliceable.SliceMaterial, sliceable.SliceEffect, contactPoint);
    }

    private void TryToSliceObject(GameObject collisionObject, Material sliceMaterial, GameObject sliceEffect, ContactPoint contactPoint)
    {
        if (collisionObject && collisionObject.activeSelf)
        {
            SlicedHull hull = collisionObject.Slice(transform.position, transform.up, sliceMaterial);

            if (hull != null)
            {
                ToSliceObject(collisionObject, hull, sliceMaterial, sliceEffect, contactPoint);
            }
        }
    }

    private void ToSliceObject(GameObject collisionObject, SlicedHull hull,
        Material sliceMaterial, GameObject sliceEffect, ContactPoint contactPoint)
    {
        GameObject lower = hull.CreateLowerHull(collisionObject, sliceMaterial);
        GameObject upper = hull.CreateUpperHull(collisionObject, sliceMaterial);

        if (sliceEffect)
            PlayVFX(sliceEffect, contactPoint, lower, upper);

        DestroyObjectToSlice(collisionObject);
        AddMeshCollider(lower, upper);
        AddRigidbody(lower, upper);
    }

    private void PlayVFX(GameObject sliceEffect, ContactPoint contactPoint, params GameObject[] charactersParts)
    {
        foreach (GameObject characterParts in charactersParts)
        {
            Instantiate(sliceEffect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal), characterParts.transform);
        }
    }

    private void DestroyObjectToSlice(GameObject collisionObject)
    {
        Destroy(collisionObject);
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
