using UnityEngine;

namespace Assets._Project.Scripts
{
	public class DestroyOnDeath: MonoBehaviour
	{
		public void HandleDeath()
		{
			Destroy(gameObject);
        }
	}
}