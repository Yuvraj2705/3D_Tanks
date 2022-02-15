using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    #region Serialize and Public Variables

    [Header("Player Settings")]
    [SerializeField] float PlayerMoveSpeed = 5;
    [SerializeField] float PlayerRotationSpeed = 100;
        
    #endregion

    #region Private Variables

    float horizontal, vertical;

    private Rigidbody rb;
        
    #endregion

    #region In-Built Functions

    void Start()
    {
        InitVariables();
    }

    void Update()
    {
        InputHandler();
    }

    void FixedUpdate()
    {
        MoveHandler();
    }

    #endregion

    #region Custom Functions

    void InitVariables()
    {
        rb = GetComponent<Rigidbody>();
    }

    void InputHandler()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void MoveHandler()
    {
        Vector3 wp = transform.position + (transform.forward * PlayerMoveSpeed * Time.deltaTime * vertical);
        rb.MovePosition(wp);

        Quaternion wr = transform.rotation * Quaternion.Euler(Vector3.up * PlayerRotationSpeed * horizontal * Time.deltaTime);
        rb.MoveRotation(wr);
    }
        
    #endregion
    
}
