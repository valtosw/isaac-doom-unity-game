using UnityEngine;
using UnityEngine.AI;

namespace Assets._Project.Scripts
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class ChaserMovement: MonoBehaviour
	{
		[Header("Settings")]
        [SerializeField] private float pathUpdateRate = 0.2f;

		private NavMeshAgent _agent;
		private Transform _playerTransform;
		private float _nextPathUpdateTime;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
		{
            var player = GameObject.FindGameObjectWithTag("Player");
            _playerTransform = player.transform;
        }

		private void Update()
		{
            if (_playerTransform == null || Time.time < _nextPathUpdateTime)
            {
                return;
            }

            _agent.SetDestination(_playerTransform.position);
            _nextPathUpdateTime = Time.time + pathUpdateRate;
        }
	}
}