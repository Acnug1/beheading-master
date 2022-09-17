using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Character))]

public class Ragdoll : MonoBehaviour
{
    private Animator _animator;
    private Character _character;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _character = GetComponent<Character>();

        DisableRagdoll();
    }

    private void DisableRagdoll()
    {
        _animator.enabled = true;

        var zombieParts = GetComponentsInChildren<Rigidbody>();

        foreach (var zombiePart in zombieParts)
        {
            zombiePart.isKinematic = true;
        }
    }

    private void OnEnable()
    {
        _character.Died += OnDied;
    }

    private void OnDisable()
    {
        _character.Died -= OnDied;
    }

    private void OnDied(Character character)
    {
        EnableRagdoll();
    }

    private void EnableRagdoll()
    {
        _animator.enabled = false;

        var zombieParts = GetComponentsInChildren<Rigidbody>();

        foreach (var zombiePart in zombieParts)
        {
            zombiePart.isKinematic = false;
        }
    }
}
