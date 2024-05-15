using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Following camera variable is our eyes
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;

    // EnemyHealth
    [SerializeField] float shellDamage = 40f;

    // muzzle flash
    [SerializeField] ParticleSystem psMuzzleFlash;

    // hit effect
    [SerializeField] GameObject hitEffect;
    // we can store the effect at following variable and destroy after usage
    GameObject impact;

    // Decreasing ammo etc.
    [SerializeField] Ammo ammoSlot;

    // Reload time between shoots
    [SerializeField] float timeBetweenShoots = 0.5f;
    bool canShoot = true;

    // Different ammos
    [SerializeField] AmmoType ammoType;

    // Display current ammo at screen
    [SerializeField] TextMeshProUGUI ammoText;


    private void OnEnable()
    {
        canShoot = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }

        DisplayAmmo();
    }
    
    IEnumerator Shoot()
    {
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {

            ammoSlot.ReduceCurrentAmmo(ammoType);
            PlayMuzzleFlash();
            ProcessRaycast();
        }

        // The function will wait for specified amount of seconds to return.
        // During this time canShoot cannot be set to true and function cannot be accessed
        // Called function will continue after waiting for specified seconds and canShoot can be set to true
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoot = true;
    }

    void PlayMuzzleFlash()
    {
        if(!psMuzzleFlash.isPlaying)
        {
            psMuzzleFlash.Play();
        }
        else
        {
            return;
        }
    }

    private void ProcessRaycast()
    {
        // Following variable will store the informations of object that ammo collided with.
        RaycastHit hit;

        // Raycast returns true after colliding. Allows to learn informations about the other object
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            // The reason of put "hit" as parameter, we can tell where ammo hit to the function
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (!target) return;
            target.TakeDamage(shellDamage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);

        ammoText.text = "Ammo: " + currentAmmo.ToString();
    }
}
