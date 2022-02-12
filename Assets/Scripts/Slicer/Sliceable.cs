using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [SerializeField] private Material _sliceMaterial;
    [SerializeField] private GameObject _sliceEffect;

    public Material SliceMaterial => _sliceMaterial;
    public GameObject SliceEffect => _sliceEffect;
}
