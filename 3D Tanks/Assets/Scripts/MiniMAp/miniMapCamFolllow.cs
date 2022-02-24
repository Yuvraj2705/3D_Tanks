using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapCamFolllow : MonoBehaviour
{
    [SerializeField] Transform player;

    private Vector3 correctedPos;

    void FixedUpdate()
    {
        transform.position =  new Vector3(player.position.x,transform.position.y,transform.position.z);   
    }
}
