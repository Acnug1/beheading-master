using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CelebrationState : State
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (Character.IsAlive)
            _animator.SetTrigger(AnimatorCharacterController.Params.Celebrate);
        else
            _animator.StopPlayback();
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}