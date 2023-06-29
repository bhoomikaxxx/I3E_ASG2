using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //Gun pick up
    public gunScript shootScript;
    public Rigidbody rigidb;
    public BoxCollider coll;
    public Transform gunContainer, player, fpsCam; //+player orientation +camera

    //Gun pick up and drop
    public float pickUpRange;
    public float dropForwardForce, dropUpForce;

    //Check for gun in hand
    public bool equipped;
    public bool slotFull;

    //Pick up function
    private void PickUpGun()
    {
        equipped = true;
        slotFull = true;

        //Prevent gun from moving when in hand
        rigidb.isKinematic = true;
        coll.isTrigger = true;

        //Make gun move w player
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Gun script enable
        shootScript.enabled = true;
    }

    //Drop
    private void Drop()
    {
        //Remove gun
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        //Make gun from move out of hand
        rigidb.isKinematic = false;
        coll.isTrigger = false;

        rigidb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Gun velocity
        rigidb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rigidb.AddForce(fpsCam.up * dropUpForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rigidb.AddTorque(new Vector3(random, random, random) * 10);

        shootScript.enabled = false;
    }
    //For gun
    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;

        //Check for gun in hand
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && slotFull) PickUpGun();

        //Drop gun if have
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

        if (!equipped)
        {
            shootScript.enabled = false;
            rigidb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            shootScript.enabled = true;
            rigidb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }
}
