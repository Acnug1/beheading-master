using UnityEngine;

[RequireComponent(typeof(Character))]

public class CharacterCollisionHandler : MonoBehaviour
{
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MeshSlicer meshSlicer))
        {
            _character.Die();
        }
    }
}
