using UnityEngine;

public class Enemy : Character
{
    [Min(0f)]
    [SerializeField] private float _activationDistanceForSmileScared;

    private SmileScared _smileScared;
    private Follower[] _followers;

    private void Start()
    {
        _smileScared = GetComponentInChildren<SmileScared>();
        _followers = FindObjectsOfType<Follower>();
    }

    protected override void Update()
    {
        base.Update();

        if (IsAlive && _smileScared)
        {
            if (CheckDistanceToFollowers())
                _smileScared.EnableSmileScared();
            else
                _smileScared.DisableSmileScared();
        }
    }

    private bool CheckDistanceToFollowers()
    {
        foreach (var follower in _followers)
        {
            if (Vector3.Distance(transform.position, follower.transform.position) <= _activationDistanceForSmileScared)
                return true;
        }

        return false;
    }
}
