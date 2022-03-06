using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SManager : MonoBehaviour
{
    float timer;

    [Header("Settings")]

    [SerializeField]
    float bulletsPerSecond = 0.1f;

    float timeBase = 0;

    [SerializeField]
    int MagzineSize = 32;

    [SerializeField]
    float reloadTime = 2.5f;

    [SerializeField] 
    int bulletDamage = 1;

    int Bullets;

    bool canReload;
    bool canShoot;

    [Header("Components")]

    [SerializeField] 
    Camera fpsCam;

    [Header("Keys")]

    [SerializeField] 
    KeyCode ShootKey;

    [SerializeField] 
    KeyCode ReloadKey;

    [SerializeField] 
    KeyCode ModeChangeKey;

    RaycastHit hit;
    
    bool ModeChanger;

    void Awake()
    {
        canShoot = true;
        canReload = true;
        Bullets = MagzineSize;
        timeBase = bulletsPerSecond;
    }

    void Update()
    {

        if(Input.GetKeyDown(ModeChangeKey))
        {
            ModeChanger = !ModeChanger;
        }
        
        if(ModeChanger)
        {
            SingleMode();
        }
        else
        {
            AutoMode();
        }
    }

    void SingleMode()
    {
        if (Input.GetKeyDown(ShootKey) && canShoot && Bullets > 0)
        {
            ShootMech();
            Bullets = Bullets - 1;
        }
        if(canReload && Input.GetKeyDown(ReloadKey))
        {
            canShoot = false;
            StartCoroutine(Reload());
        }
    }

    void AutoMode()
    {
        if (Input.GetKey(ShootKey) && canShoot)
        {
            timer += Time.deltaTime;
            if(timer > timeBase && Bullets > 0)
            {
                timer = 0;
                ShootMech();
                Bullets = Bullets - 1;
            }
        }
        if(canReload && Input.GetKeyDown(ReloadKey))
        {
            canReload = false;
            canShoot = false;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        Debug.Log("Reloaded");
        Bullets = MagzineSize;
        canShoot = true;
        canReload = true;
    }

    void ShootMech()
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
    }
}
