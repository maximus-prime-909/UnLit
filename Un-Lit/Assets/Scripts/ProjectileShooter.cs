using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour
{

	// Reference to projectile prefab to shoot
	public GameObject projectile;
	public float power = 10.0f;

	// Reference to AudioClip to play
	public AudioClip bulletSFX;
	public AudioClip shootSFX;

	public Shooter shooter;

	// Update is called once per frame
	void Update()
	{
		// Detect if fire button is pressed
		if (Input.GetButtonDown("Fire1") && shooter.isReloading == false)
		{
			if (shootSFX)
			{
				AudioSource.PlayClipAtPoint(shootSFX, transform.position);
			}

			// if projectile is specified
			if (projectile)
			{
				// Instantiante projectile at the camera + 1 meter forward with camera rotation
				GameObject newProjectile = Instantiate(projectile, transform.position - transform.right, transform.rotation) as GameObject;

				// if the projectile does not have a rigidbody component, add one
				if (!newProjectile.GetComponent<Rigidbody>())
				{
					newProjectile.AddComponent<Rigidbody>();
				}
				// Apply force to the newProjectile's Rigidbody component if it has one
				newProjectile.GetComponent<Rigidbody>().AddForce(-transform.right * power, ForceMode.VelocityChange);

				// play sound effect if set
				if (bulletSFX)
				{
					if (newProjectile.GetComponent<AudioSource>())
					{ // the projectile has an AudioSource component
					  // play the sound clip through the AudioSource component on the gameobject.
					  // note: The audio will travel with the gameobject.
						newProjectile.GetComponent<AudioSource>().PlayOneShot(bulletSFX);
					}
					else
					{
						// dynamically create a new gameObject with an AudioSource
						// this automatically destroys itself once the audio is done
						AudioSource.PlayClipAtPoint(bulletSFX, newProjectile.transform.position);
					}
				}
			}
		}
	}
}

