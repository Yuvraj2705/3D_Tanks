using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] Image chargeBar;

    [Header("Prefabs")]
    [SerializeField] Rigidbody bullet;

    [Header("FirePoint Transform")]
    [SerializeField] Transform[] firePoints;

    [Header("Fire Settings")]
    [SerializeField] float force = 5;
    [SerializeField] float backForce = 2;
    [SerializeField] float UpForce = 2;
    [SerializeField] float destroyAfter = 2;
    [SerializeField] float fireRate;
    private float timer = 0;
    private float spawnTime = 0.1f;

    private int gunSwitchCounter = 0;

    private float uniTimer = 0;
    private float chargeTill = 2;
    private bool canCharge;
    private bool canShoot;

    private Rigidbody rb;
    private Vector3 direction;

    void Awake()
    {
        InitVariables();
    }

    void InitVariables()
    {
        canCharge = true;
        canShoot = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //ShootAi();
        OneShotAi();
        //Hacks();
    }

    void OneShotAi()
    {
        chargeBar.fillAmount = uniTimer / chargeTill;

        if(Input.GetKey(KeyCode.Z) && canCharge)
        {
            uniTimer += Time.deltaTime;
            if(uniTimer > chargeTill)
            {
                canCharge = false;
                canShoot = true;
            }
        }

        if(canShoot && Input.GetKeyDown(KeyCode.X))
        {
            foreach(var firePoint in firePoints)
            {
                var bulletInstance = Instantiate(bullet, firePoint.position, firePoint.rotation);
                bulletInstance.AddForce(firePoint.forward * force, ForceMode.Impulse);
                Destroy(bulletInstance.gameObject,destroyAfter);

                // Revert Force Exerted By Bullet
                direction = firePoint.forward;
            }
            StartCoroutine(TankRecoil());
            canCharge = true;
            canShoot = false;
            uniTimer = 0;
        }

        if(Input.GetKeyUp(KeyCode.Z))
        {
            uniTimer = 0;
            canCharge = true;
            canShoot = false;
        }
    }

    IEnumerator TankRecoil()
    {
        rb.AddForce(-direction * backForce + Vector3.up * UpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        rb.AddForce(direction * (backForce/2) + Vector3.up * UpForce, ForceMode.Impulse);
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
