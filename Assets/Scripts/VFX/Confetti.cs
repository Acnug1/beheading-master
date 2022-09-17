using UnityEngine;

public class Confetti : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confettiVFX;
    [SerializeField] private Spawner _spawner;

    private void Awake()
    {
        if (_confettiVFX)
            DisableVFX(_confettiVFX);
    }

    private void DisableVFX(ParticleSystem confettiVFX)
    {
        confettiVFX.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _spawner.AllEnemyDied += OnAllEnemyDied;
    }

    private void OnDisable()
    {
        _spawner.AllEnemyDied -= OnAllEnemyDied;
    }

    private void OnAllEnemyDied()
    {
        if (_confettiVFX)
            PlayVFX(_confettiVFX);
    }

    private void PlayVFX(ParticleSystem confettiVFX)
    {
        confettiVFX.gameObject.SetActive(true);
        confettiVFX.Play();
    }
}
