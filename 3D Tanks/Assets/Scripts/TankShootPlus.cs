using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShootPlus : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] Image chargeBar;

    [Header("Prefabs")]
    [SerializeField] Rigidbody bullet;

    [Header("FirePoint Transform")]
    [SerializeField] Transform[] firePoints;

    [Header("Fire Settings")]
    [SerializeField] float force = 5;
    [SerializeField] float destroyAfter = 2;
    [SerializeField] float fireRate;

    [Header("RocketLauncher")]
    [SerializeField] float backForce = 2;
    [SerializeField] float UpForce = 2;

    [Header("MachineGun")]
    [SerializeField] float GunbackForce = 1;
    [SerializeField] float GunUpForce = 1;

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
        OneShotAi();
        Switch();
    }

    void OneShotAi()
    {
        chargeBar.fillAmount = uniTimer / chargeTill;

        if (Input.GetKey(KeyCode.Space) && canCharge)
        {
            uniTimer += Time.deltaTime;
            if (uniTimer > chargeTill)
            {
                canCharge = false;
                canShoot = true;
            }
        }

        if (canShoot && Input.GetKeyDown(KeyCode.F))
        {
            if (gunSwitchCounter == 0)
            {
                RocketLauncher();
            }
            if (gunSwitchCounter == 1)
            {
                MachineGun();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            uniTimer = 0;
            canCharge = true;
            canShoot = false;
        }
    }

    IEnumerator TankRecoil()
    {
        if (gunSwitchCounter == 0)
        {
            rb.AddForce(-direction * backForce + Vector3.up * UpForce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f);
            rb.AddForce(direction * (backForce / 2) + Vector3.up * UpForce, ForceMode.Impulse);
        }
        if (gunSwitchCounter == 1)
        {
            rb.AddForce(-direction * GunbackForce + Vector3.up * GunUpForce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f);
            rb.AddForce(direction * (GunbackForce / 2) + Vector3.up * GunUpForce, ForceMode.Impulse);
        }
    }

    void RocketLauncher()
    {
        //Debug.Log("RocketLauncher");
        foreach (var firePoint in firePoints)
        {
            var bulletInstance = Instantiate(bullet, firePoint.position, firePoint.rotation);
            bulletInstance.AddForce(firePoint.forward * force, ForceMode.Impulse);
            Destroy(bulletInstance.gameObject, destroyAfter);

            // Revert Force Exerted By Bullet
            direction = firePoint.forward;
        }
        StartCoroutine(TankRecoil());
        canCharge = true;
        canShoot = false;
        uniTimer = 0;
    }

    void MachineGun()
    {
        //Debug.Log("MachineGun");
        foreach (var firePoint in firePoints)
        {
            var bulletInstance = Instantiate(bullet, firePoint.position, firePoint.rotation);
            bulletInstance.AddForce(firePoint.forward * force, ForceMode.Impulse);
            Destroy(bulletInstance.gameObject, destroyAfter);

            // Revert Force Exerted By Bullet
            direction = firePoint.forward;
        }
        StartCoroutine(TankRecoil());
        canCharge = true;
        canShoot = false;
        uniTimer = 0;
    }

    void Switch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("RocketLauncher");
            if (gunSwitchCounter == 0)
            {
                gunSwitchCounter = 0;
            }
            else
            {
                gunSwitchCounter -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("MachineGun");
            if (gunSwitchCounter == 1)
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
