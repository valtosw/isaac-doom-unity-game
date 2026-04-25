using UnityEngine;
using UnityEngine.AI;

namespace Assets._Project.Scripts.Enemies.Chaser
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyChaserMovement : MonoBehaviour
	{
        [Header("Settings")]
        [SerializeField] private float _pathUpdateRate = 0.2f;

        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private float _nextPathUpdateTime;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (_playerTransform == null || Time.time < _nextPathUpdateTime)
            {
                return;
            }

            _agent.SetDestination(_playerTransform.position);
            _nextPathUpdateTime = Time.time + _pathUpdateRate;
        }
    }
}