using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ExplosionData _explosionData;
    [SerializeField] private Transform _explosionZone;

    private float _radius;
    private float _force;
    private float _explodeDelay;
    private ParticleSystem _explosionEffect;
    private bool _explosionDone = false;

    private void Start()
    {
        _radius = _explosionData.Radius;
        _force = _explosionData.Force;
        _explodeDelay = _explosionData.ExplodeDelay;
        _explosionEffect = _explosionData.ExplosionEffect;

        SetExplosionZone();
    }

    public void ExplodeWithDelay()
    {
        if (_explosionDone)
            return;

        _explosionDone = true;

        if (gameObject.TryGetComponent(out Renderer renderer))
            renderer.material.color = Color.red;

        Invoke(nameof(Explode), _explodeDelay);
    }

    private void Explode()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider overlappedCollider in overlappedColliders)
        {
            Rigidbody rigidbody = overlappedCollider.attachedRigidbody;

            if (rigidbody)
            {
                if (rigidbody.transform.root.TryGetComponent(out Character character))
                {
                    character.Die();
                }
                else
                if (rigidbody.TryGetComponent(out Explosion explosion)
                    && Vector3.Distance(transform.position, rigidbody.transform.position) < _radius / 1.5f)
                    explosion.ExplodeWithDelay();

                rigidbody.AddExplosionForce(_force, transform.position, _radius, 1f);
            }
        }

        if (_explosionEffect)
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void SetExplosionZone()
    {
        _explosionZone.localScale *= _radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius / 1.5f);
    }
}