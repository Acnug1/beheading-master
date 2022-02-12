using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]

public class Fragment : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] private float _lifeTimeFragment;
    [Min(0f)]
    [SerializeField] private float _timeOfDisappearance = 1;

    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;
    private Coroutine _dissapearScale;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshCollider = GetComponent<MeshCollider>();
        _rigidbody.isKinematic = true;
        _meshCollider.convex = true;
    }

    public void DisableKinematic()
    {
        _rigidbody.isKinematic = false;
        Invoke(nameof(StartDisappearing), _lifeTimeFragment);
    }

    private void StartDisappearing()
    {
        if (_dissapearScale != null)
            StopCoroutine(_dissapearScale);

        _dissapearScale = StartCoroutine(DissapearScale());
    }

    private IEnumerator DissapearScale()
    {
        Vector3 fragmentScale = gameObject.transform.localScale;
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

        for (float i = _timeOfDisappearance; i > 0; i -= 0.1f)
        {
            gameObject.transform.localScale = fragmentScale * GetNormalizeScale(i);
            yield return waitForSeconds;
        }

        Destroy(gameObject);
    }

    private float GetNormalizeScale(float i) => i / _timeOfDisappearance;
}
