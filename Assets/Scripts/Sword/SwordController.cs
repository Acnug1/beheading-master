using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private Follower _startFollower;
    [SerializeField] private Follower _endFollower;
    [SerializeField] private MeshSlicer _swordPrefab;

    private MeshSlicer _sword;

    private void Awake()
    {
        CreateSword();
    }

    private void CreateSword()
    {
        Vector3 direction = (_endFollower.transform.position - _startFollower.transform.position).normalized;
        MeshSlicer sword = Instantiate(_swordPrefab, _startFollower.transform.position, Quaternion.LookRotation(direction), transform);
        InitializeSword(sword);
    }

    private void InitializeSword(MeshSlicer sword)
    {
        _sword = sword;
    }

    private void Update()
    {
        SetSwordScale();
        ChangeSwordPosition();
        LookAtEndFollower(_endFollower);
    }

    private void SetSwordScale()
    {
        float distanceToEndFollower = Vector3.Distance(_sword.transform.position, _endFollower.transform.position);
        _sword.transform.localScale = new Vector3(_sword.transform.localScale.x, _sword.transform.localScale.y, distanceToEndFollower);
    }

    private void ChangeSwordPosition()
    {
        if (_sword.transform.position != _startFollower.transform.position)
            _sword.transform.position = _startFollower.transform.position;
    }

    private void LookAtEndFollower(Follower endFollower)
    {
        _sword.transform.LookAt(endFollower.transform);
    }
}
