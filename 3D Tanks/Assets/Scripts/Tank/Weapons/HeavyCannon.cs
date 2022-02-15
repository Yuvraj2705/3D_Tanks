using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TanksWeapon
{
    public class HeavyCannon : MonoBehaviour
    {
        #region Serialized Variables
        [Header("Ui")]
        [SerializeField] Image chargeBar;

        [Header("Prefabs")]
        [SerializeField] Rigidbody bullet;

        [Header("FirePoint Transform")]
        [SerializeField] Transform[] firePoints;

        [Header("Bullet Settings")]
        [SerializeField] float bulletSpeed = 5;
        [SerializeField] float destroyBulletAfter = 2;
        [HideInInspector] float fireRate;

        [Header("Heavy Cannon Settings")]
        [SerializeField] float backForce = 2;
        [SerializeField] float UpForce = 1;
        #endregion

        #region Private Variables

        private float uniTimer = 0;
        private float chargeTill = 2;

        private bool canCharge;
        private bool isCharged;
        private bool canShoot;
        private bool canCoolDown;

        private Vector3 direction;

        private Rigidbody rb;

        #endregion

        #region InBuiltMethods

        void Awake()
        {
            canCharge = true;
            canShoot = false;
            isCharged = false;

            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            HeavyCannonAi();
        }

        #endregion

        #region CustomMethods

        void HeavyCannonAi()
        {
            chargeBar.fillAmount = uniTimer / chargeTill;

            if (Input.GetMouseButton(1) && canCharge)
            {
                canCoolDown = false;
                uniTimer += Time.deltaTime;
                if (uniTimer > chargeTill)
                {
                    canCharge = false;
                    canShoot = true;
                    isCharged = true;
                    canCoolDown = false;
                } 
            }

            if (canShoot && Input.GetMouseButtonDown(0))
            {
                foreach (var firePoint in firePoints)
                {
                    var bulletInstance = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    bulletInstance.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
                    Destroy(bulletInstance.gameObject, destroyBulletAfter);

                    // Revert Force Exerted By Bullet
                    direction = firePoint.forward;
                }
                StartCoroutine(TankRecoil());
                uniTimer = 0;
                isCharged = false;
                canCharge = true;
                canShoot = false;
                canCoolDown = true;
            }

            if(canCoolDown)
            {
                if(uniTimer <= 0) { return; }
                uniTimer -= Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(1))
            {
                if(isCharged)
                {
                    canCoolDown = false;
                    return;
                }
                else
                {
                    canCoolDown = true;
                    canCharge = true;
                    canShoot = false;
                }
            }
        }

        IEnumerator TankRecoil()
        {
            rb.AddForce(-direction * backForce + Vector3.up * UpForce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f);
            rb.AddForce(direction * (backForce / 2) + Vector3.up * UpForce, ForceMode.Impulse);
        }


        #endregion
    }
}