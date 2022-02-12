using UnityEngine;

public class Enemy : Character
{
    [Min(0f)]
    [SerializeField] private float _activationDistanceForSmileScared;

    private SmileScared _smileScared;
    private MeshSlicer _meshSlicer;

    private void Start()
    {
        _smileScared = GetComponentInChildren<SmileScared>();
        _meshSlicer = FindObjectOfType<MeshSlicer>();
    }

    protected override void Update()
    {
        base.Update();

        if (IsAlive && _smileScared)
            CheckDistanceToMeshSlicer();
    }

    private void CheckDistanceToMeshSlicer()
    {
        if (Vector3.Distance(transform.position, _meshSlicer.transform.position) <= _activationDistanceForSmileScared)
            _smileScared.EnableSmileScared();
        else
            _smileScared.DisableSmileScared();
    }
}
