using UnityEngine;

public class CharacterController1 : MonoBehaviour
{

    [Header("Input")]
    private float moveInput;
    private float turnInput;

    [Header("Refs")]
    private CharacterController controller;
    

    [Header("Movement")]
    public float moveSpeed = 7f;
    public float gravity = 9.81f;
    public float turningSpeed = 15f;

    private float verticalVelocity;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        InputManagement();
        Movement();
    }

    private void Movement()
    {
        GroundMovement();
        Turn();
    }

    private void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput).normalized;

        move.y = 0; 

        move *= moveSpeed;

        move.y = GravityForce();

        controller.Move(move * Time.deltaTime);

        
    }

    private void Turn()
    {
        if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0)
        {
            Vector3 currentLookDirection = controller.velocity.normalized;
            currentLookDirection.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
        }
        
    }

    private float GravityForce()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravity* Time.deltaTime;
        }
        return verticalVelocity;
    }


    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
}
