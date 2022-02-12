using UnityEngine;

public class SmileScared : MonoBehaviour
{
    private Camera _mainCamera;
    private Character _character;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _character = GetComponentInParent<Character>();
    }

    private void OnEnable()
    {
        _character.Died += OnCharacterDied;
    }

    private void OnDisable()
    {
        _character.Died -= OnCharacterDied;
    }

    private void OnCharacterDied(Character character)
    {
        DisableSmileScared();
    }

    private void Update()
    {
        if (!_mainCamera)
            return;

        AlignCamera(_mainCamera);
    }

    public void EnableSmileScared()
    {
        if (!IsSmileScaredEnabled())
            gameObject.SetActive(true);
    }

    public void DisableSmileScared()
    {
        if (IsSmileScaredEnabled())
            gameObject.SetActive(false);
    }

    private bool IsSmileScaredEnabled() => gameObject.activeSelf;

    private void AlignCamera(Camera mainCamera)
    {
        Vector3 forwardCamera = (transform.position - mainCamera.transform.position).normalized;
        Vector3 upCamera = Vector3.Cross(forwardCamera, mainCamera.transform.right);
        transform.rotation = Quaternion.LookRotation(forwardCamera, upCamera);
    }
}
