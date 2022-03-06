using UnityEngine;
using UnityEngine.UI;

public class SManager : MonoBehaviour
{
    [Header("Gun Stats")]
    public float timeBetweenShooting = 0.05f;
    public float range = 100f;
    public float reloadTime = 1f;
    public float timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap;
    int bulletsLeft, bulletsShot;

    //bools 
    bool readyToShoot, reloading;

    [Header("Settings")]
    [SerializeField] Camera fpsCam;
    [SerializeField] KeyCode ShootKey;
    [SerializeField] int bulletDamage = 1;

    [Header("HUD")]
    public Text ammo; 

    RaycastHit hit;
    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    void Update()
    {
        ammo.text = bulletsLeft + " / " + magazineSize;

        if (bulletsLeft > 0)
        {
            //Single_Mode
            /*bulletsPerTap = 1;
            SingleMode();*/

            //Auto_Mode
            bulletsPerTap = bulletsLeft;
            timeBetweenShots = 2f ;
            AutoMode();
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if (bulletsLeft == 0)
        {
            Reload();
        }
    }
    void SingleMode()
    {
        if (Input.GetKeyDown(ShootKey))
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100))
            {
                var damage = hit.transform.GetComponent<ShootManager>();
                if (damage != null)
                {
                    damage.Damage(bulletDamage);
                }
                Debug.Log(hit.transform.name);
            }

            bulletsLeft--;
            bulletsShot--;

            Invoke("ResetShot", timeBetweenShooting);

            if (bulletsShot > 0 && bulletsLeft > 0)
            {
                Invoke("Shoot", timeBetweenShots);
            }
        }
    }
    void AutoMode()
    {
        if (Input.GetKey(ShootKey))
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 100))
            {
                var damage = hit.transform.GetComponent<ShootManager>();
                if (damage != null)
                {
                    damage.Damage(bulletDamage);
                }
                Debug.Log(hit.transform.name);
            }

            bulletsLeft--;
            bulletsShot--;

            Invoke("ResetShot", timeBetweenShooting);

            if (bulletsShot > 0 && bulletsLeft > 0)
            {
                Invoke("Shoot", timeBetweenShots);
            }
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
