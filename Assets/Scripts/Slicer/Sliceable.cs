using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [SerializeField] private GameObject _sliceEffect;

    public GameObject SliceEffect => _sliceEffect;
}
