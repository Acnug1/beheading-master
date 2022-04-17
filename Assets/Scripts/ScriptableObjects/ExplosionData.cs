using UnityEngine;

[CreateAssetMenu(fileName = "New Explosion", menuName = "Explosions/Create New Explosion", order = 51)]

public class ExplosionData : ScriptableObject
{
    [Min(0f)]
    [SerializeField] private float _radius;
    [Min(0f)]
    [SerializeField] private float _force;
    [Min(0f)]
    [SerializeField] private float _explodeDelay;
    [SerializeField] private ParticleSystem _explosionEffect;

    public float Radius => _radius;
    public float Force => _force;
    public float ExplodeDelay => _explodeDelay;
    public ParticleSystem ExplosionEffect => _explosionEffect;
}
