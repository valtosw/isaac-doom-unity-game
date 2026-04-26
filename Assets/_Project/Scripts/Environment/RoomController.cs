using Assets._Project.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Environment
{
    [RequireComponent(typeof(BoxCollider))]
    public class RoomController : MonoBehaviour
    {
        private static readonly WaitForSeconds _waitForSeconds1_0 = new(1.0f);

        public enum RoomState 
        { 
            Inactive, 
            Active, 
            Cleared 
        }

        [Header("Room Setup")]
        [SerializeField] private List<DoorController> _doors;
        [SerializeField] private Transform[] _spawnPoints;

        [Header("Enemy Spawning")]
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private int _minEnemies = 2;
        [SerializeField] private int _maxEnemies = 4;

        private RoomState _currentState = RoomState.Inactive;
        private int _activeEnemies = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (_currentState == RoomState.Inactive && other.CompareTag("Player"))
            {
                ActivateRoom();
            }
        }

        private void ActivateRoom()
        {
            _currentState = RoomState.Active;

            foreach (var door in _doors)
            {
                if (door != null)
                {
                    door.LockDoor();
                }
            }

            StartCoroutine(SpawnSequenceRoutine());
        }

        private IEnumerator SpawnSequenceRoutine()
        {
            yield return _waitForSeconds1_0;

            SpawnEnemies();

            if (_activeEnemies == 0)
            {
                ClearRoom();
            }
        }

        private void SpawnEnemies()
        {
            if (_enemyPrefabs == null || _enemyPrefabs.Length == 0 || _spawnPoints.Length == 0)
            {
                return;
            }

            var enemyCount = Random.Range(_minEnemies, _maxEnemies + 1);
            enemyCount = Mathf.Min(enemyCount, _spawnPoints.Length);

            var availablePoints = new List<Transform>(_spawnPoints);

            for (var i = 0; i < availablePoints.Count; i++)
            {
                var temp = availablePoints[i];
                var randomIndex = Random.Range(i, availablePoints.Count);
                availablePoints[i] = availablePoints[randomIndex];
                availablePoints[randomIndex] = temp;
            }

            for (var i = 0; i < enemyCount; i++)
            {
                var randomPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
                var spawnedEnemy = Instantiate(randomPrefab, availablePoints[i].position, availablePoints[i].rotation);

                if (spawnedEnemy.TryGetComponent(out HealthComponent health))
                {
                    _activeEnemies++;
                    health.OnDeath.AddListener(OnEnemyDied);
                }
            }
        }

        private void OnEnemyDied()
        {
            _activeEnemies--;

            if (_activeEnemies <= 0 && _currentState == RoomState.Active)
            {
                ClearRoom();
            }
        }

        private void ClearRoom()
        {
            _currentState = RoomState.Cleared;

            foreach (var door in _doors)
            {
                if (door != null)
                {
                    door.UnlockDoor();
                }
            }

            Debug.Log($"[{gameObject.name}] Room Cleared!");
        }
    }
}