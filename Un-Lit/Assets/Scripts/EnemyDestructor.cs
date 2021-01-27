using UnityEngine;
using System.Collections;

public class EnemyDestructor : MonoBehaviour
{
	public float life = 100f;
	// explosion when hit
	public GameObject explosionPrefab;
	public EnemyHealthMeter health;

    private void Start()
    {
		health.SetMaxHealth(life);
    }

	public void TakeDamage(float amount)
    {
		life -= amount;
		health.SetHealth(life);
		if(life <= 0)
        {
			if (explosionPrefab)
			{
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate(explosionPrefab, transform.position, transform.rotation);
			}

			// destroy self
			Destroy(gameObject);
		}
	}
    
}
