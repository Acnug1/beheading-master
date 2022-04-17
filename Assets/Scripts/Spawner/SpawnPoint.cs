using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Character _character;

    public Character Character => _character;
}