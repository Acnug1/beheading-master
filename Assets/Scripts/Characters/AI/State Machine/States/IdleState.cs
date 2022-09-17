using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IdleState : State
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (Character.IsAlive)
            _animator.Play(AnimatorCharacterController.States.Idle);
        else
            _animator.StopPlayback();
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}