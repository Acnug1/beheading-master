using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private const string ErrorMessage = "The game has already started";
    private int _countEnemies = 0;
    private bool _isGameStarted = false;
    private bool _isAllEnemyDied;
    private bool _isHostageDied;

    public event UnityAction AllEnemyDied;
    public event UnityAction HostageDied;

    public int CountEnemies => _countEnemies;
    public bool IsGameStarted => _isGameStarted;

    private void Start()
    {
        SpawnPoint[] spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnCharacter(spawnPoints);
    }

    public void SetGameStart()
    {
        if (!_isGameStarted)
            _isGameStarted = true;
        else
            Debug.LogError(ErrorMessage);
    }

    private void SpawnCharacter(SpawnPoint[] spawnPoints)
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            Character character = Instantiate(spawnPoint.Character, spawnPoint.transform.position, spawnPoint.transform.rotation);

            if (NavMesh.SamplePosition(character.transform.position, out NavMeshHit closestHit, 4f, NavMesh.AllAreas))
            {
                character.transform.position = closestHit.position;
            }

            switch (character)
            {
                case Enemy enemy:
                    character.Died += OnEnemyDied;
                    _countEnemies++;
                    break;
                case Hostage hostage:
                    character.Died += OnHostageDied;
                    character.Init(_path, this);
                    break;
            }
        }
    }

    private void OnEnemyDied(Character character)
    {
        character.Died -= OnEnemyDied;

        _countEnemies--;

        if (_countEnemies == 0 && !_isHostageDied)
        {
            _isAllEnemyDied = true;
            AllEnemyDied?.Invoke();
        }
        else if (_countEnemies < 0)
            throw new ArgumentOutOfRangeException();
    }

    private void OnHostageDied(Character character)
    {
        character.Died -= OnHostageDied;

        if (!_isAllEnemyDied)
        {
            _isHostageDied = true;
            HostageDied?.Invoke();
        }
    }
}