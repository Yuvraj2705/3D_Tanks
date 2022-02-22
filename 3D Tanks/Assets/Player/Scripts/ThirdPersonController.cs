using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Camera my_camera;

    [Header("Character Movement")]
    [SerializeField] float movementspeed = 2f;
    [SerializeField] float sprintSpeed = 5f;
    [SerializeField] float rotationspeed = 15f;
    [SerializeField] float animationBlendSpeed = 2f;

    float mDesiredRotation = 0f;
    float mDesiredAnimationSpeed = 0f;
    bool mSprinting = false;

    float mSpeedY = 0;
    float mGravity = -9.81f;

    CharacterController controller;
    Animator animator;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        if(!controller.isGrounded)
        {
            mSpeedY += mGravity * Time.deltaTime;
        }
        else
        {
            mSpeedY = 0;
        }

        mSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(x, 0, z).normalized;

        Vector3 rotatedMovement = Quaternion.Euler(0, my_camera.transform.rotation.eulerAngles.y, 0) * movement;
        Vector3 verticalMovement = Vector3.up * mSpeedY;
        controller.Move((verticalMovement + (rotatedMovement *(mSprinting ? sprintSpeed : movementspeed))) * Time.deltaTime);

        if(rotatedMovement.magnitude > 0)
        {
            mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            mDesiredAnimationSpeed = mSprinting ? 1 : .5f;
        }
        else
        {
            mDesiredAnimationSpeed = 0;
        }

        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), mDesiredAnimationSpeed, animationBlendSpeed * Time.deltaTime));

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationspeed * Time.deltaTime);
    }
}
