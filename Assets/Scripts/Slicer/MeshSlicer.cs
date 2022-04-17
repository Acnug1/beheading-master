using UnityEngine;

public class MeshSlicer : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] private float _lifeTimeSliceEffect = 3f;

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
            TryToSliceObject(collisionObject, sliceable.SliceEffect, contactPoint);
    }

    private void TryToSliceObject(GameObject collisionObject, GameObject sliceEffect, ContactPoint contactPoint)
    {
        if (collisionObject && collisionObject.activeSelf && collisionObject.TryGetComponent(out SkeletonMeshSlicer slicer))
        {
            slicer.SliceByMeshPlane(transform.up, transform.position);

            if (sliceEffect)
                PlayVFX(sliceEffect, contactPoint);
        }
    }

    private void PlayVFX(GameObject sliceEffect, ContactPoint contactPoint)
    {
        var effect = Instantiate(sliceEffect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        DestroySliceEffect(effect);
    }

    private void DestroySliceEffect(GameObject effect)
    {
        Destroy(effect, _lifeTimeSliceEffect);
    }
}
