using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] Rigidbody bullet;

    [Header("FirePoint Transform")]
    [SerializeField] Transform[] firePoints;

    [Header("Fire Settings")]
    [SerializeField] float force = 5;
    [SerializeField] float destroyAfter = 2;
    [SerializeField] float fireRate;
    private float timer;
    private float spawnTime = 0.1f;

    private int gunSwitchCounter = 0;

    void Update()
    {
        ShootAi();
        Hacks();
    }

    void ShootAi()
    {
        if(Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if(timer > spawnTime)
            {
                if(gunSwitchCounter == 0)
                {
                    MachineGun();
                }

                if(gunSwitchCounter == 1)
                {
                    RocketLauncher();
                }
                spawnTime = timer + fireRate;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            timer = 0;
            spawnTime = 0.1f;
        }
    }

    void RocketLauncher()
    {
        Debug.Log("RocketLauncher");
        foreach(var firePoint in firePoints)
        {
            var bulletInstance = Instantiate(bullet, firePoint.position, Quaternion.identity);
            bulletInstance.AddForce(firePoint.forward * force, ForceMode.Impulse);
            Destroy(bulletInstance.gameObject, destroyAfter);
        }
    }

    void MachineGun()
    {
        Debug.Log("Machine Gun");
        foreach(var firePoint in firePoints)
        {
            var bulletInstance = Instantiate(bullet, firePoint.position, Quaternion.identity);
            bulletInstance.AddForce(firePoint.forward * force, ForceMode.Impulse);
            Destroy(bulletInstance.gameObject, destroyAfter);
        }
    }

    void Hacks()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(gunSwitchCounter == 0)
            {
                gunSwitchCounter = 0;
            }
            else
            {
                gunSwitchCounter -= 1;
            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(gunSwitchCounter == 1)
            {
                gunSwitchCounter = 1;
            }
            else
            {
                gunSwitchCounter += 1;
            }
        }
    }
}
