using UnityEngine;

namespace Assets._Project.Scripts.Core
{
	public class ObjectDestroyer : MonoBehaviour
	{
        public void ExecuteDestroy()
        {
            Destroy(gameObject);
        }
    }
}