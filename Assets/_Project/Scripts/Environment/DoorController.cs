using UnityEngine;
using UnityEngine.AI;

namespace Assets._Project.Scripts.Environment
{
    [RequireComponent(typeof(BoxCollider))]
    public class DoorController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _doorMesh;
        [SerializeField] private NavMeshObstacle _navMeshObstacle;

        [Header("Settings")]
        [SerializeField] private Vector3 _openOffset = new(0, -3.5f, 0);
        [SerializeField] private float _animationSpeed = 5f;

        private Vector3 _closedPosition;
        private Vector3 _openPosition;

        private int _playersInRange = 0;
        private int _lockCount = 0;

        private void Awake()
        {
            if (_doorMesh != null)
            {
                _closedPosition = _doorMesh.localPosition;
                _openPosition = _closedPosition + _openOffset;
            }

            if (_navMeshObstacle != null)
            {
                _navMeshObstacle.carving = true;
            }
        }

        private void Update()
        {
            if (_doorMesh == null)
            {
                return;
            }

            var shouldBeOpen = (_playersInRange > 0) && (_lockCount == 0);

            var targetPosition = shouldBeOpen ? _openPosition : _closedPosition;
            _doorMesh.localPosition = Vector3.MoveTowards(_doorMesh.localPosition, targetPosition, _animationSpeed * Time.deltaTime);

            if (_navMeshObstacle != null)
            {
                _navMeshObstacle.enabled = !shouldBeOpen;
            }
        }

        public void LockDoor() => _lockCount++;

        public void UnlockDoor() => _lockCount = Mathf.Max(0, _lockCount - 1);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playersInRange++;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playersInRange = Mathf.Max(0, _playersInRange - 1);
            }
        }
    }
}