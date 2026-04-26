using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Project.Scripts.Environment
{
    [RequireComponent(typeof(BoxCollider))]
    public class LevelExit : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _nextSceneName;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            Debug.Log("Level Complete! You escaped the dungeon.");

            if (!string.IsNullOrEmpty(_nextSceneName))
            {
                SceneManager.LoadScene(_nextSceneName);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}