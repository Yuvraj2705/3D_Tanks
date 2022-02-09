using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float PlayerSpeed;

    private Rigidbody PlayerRigidBody;

    void Start()
    {
        InitVariables();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void InitVariables()
    {
        PlayerRigidBody = GetComponent<Rigidbody>();
    }

    private void PlayerMove()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal") * PlayerSpeed, PlayerRigidBody.velocity.y, Input.GetAxis("Vertical") * PlayerSpeed);
        transform.LookAt(transform.position + new Vector3(m_Input.x, 0, m_Input.z));
        PlayerRigidBody.velocity = m_Input;
    }
}
