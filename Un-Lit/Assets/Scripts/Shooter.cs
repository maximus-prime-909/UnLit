using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    public Camera fpsCamera;
    public float power = 10.0f;

    public bool isAutomatic;
    public float fireRate = 5f;

    //Reloading
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    [HideInInspector] public bool isReloading = false;

	public float damage = 25f;

    float nextTimeToFire = 0f;

    public ParticleSystem muzzleFlash;

    public Animator animator;

    //Reference to Impact particle system
    public GameObject impactEffect;

    // Reference to ShotSound to play 
    public AudioClip shootSFX;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false); 
    }

    void Update()
    {
        if(isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (!PauseMenu.gameIsPaused)
        {
            if (!isAutomatic && Input.GetButtonDown("Fire1"))
            {
                shootBullet();
            }
            // Detect if fire button is pressed
            if (isAutomatic && Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                shootBullet();
                nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("isReloading", false);

        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
	void shootBullet()
	{
        currentAmmo -= 1; 

        if (shootSFX)
        {
            AudioSource.PlayClipAtPoint(shootSFX, transform.position);
        }
        muzzleFlash.Play(); 
		RaycastHit hit;
		if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, power))
		{
			print(hit.transform.name);
            EnemyDestructor enemy = hit.transform.GetComponent<EnemyDestructor>();
            if (enemy != null)
            {
				enemy.TakeDamage(damage);
            }

            if(impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
		}
	}
}


