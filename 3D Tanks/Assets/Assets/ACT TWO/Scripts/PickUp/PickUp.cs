using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : SelectionManager
{
    [Header("Scripts")]
    public WeaponSway weaponSway;
    public SManager shootManager;

    [Header("References")]
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    [Header("Variables")]
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    public override void ImplementCode()
    {
        //Check if player is in range and "E" is pressed
        if (!equipped && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUpS();
    }

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
            weaponSway.enabled = false;
            shootManager.enabled = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
            weaponSway.enabled = true;
            shootManager.enabled = true;
        }
    }

    private void Update()
    {
        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUpS()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = transform.localScale;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        weaponSway.enabled = true;
        shootManager.enabled = true;

        //Enable UI
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        weaponSway.enabled = false;
        shootManager.enabled = false;

        //Disable UI
    }
}
