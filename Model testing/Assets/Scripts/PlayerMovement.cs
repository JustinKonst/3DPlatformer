using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float maximumSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    public float jumpHeight;

    [SerializeField]
    private float gravityMultiplier;

    [SerializeField]
    private float jumpButtonGracePeriod;

    [SerializeField]
    private Transform cameraTransform;
    // Responsibilty Camera

    private Animator animator;
    public CharacterController characterController;
    public float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool IsJumping;
    private bool IsGrounded;

    public ParticleSystem Smoke;

    [SerializeField] private AudioSource JumpSoundEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
       

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }



        float speed = inputMagnitude * maximumSpeed;
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        float gravity = Physics.gravity.y * gravityMultiplier;

        if (IsJumping && ySpeed > 0 && Input.GetButton("Jump") == false)
        {
            gravity *= 2;
        }
        ySpeed += gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump")) 
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            IsGrounded = true;
            animator.SetBool("IsJumping", false);
            IsJumping = false;
            animator.SetBool("IsFalling", false);


            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                JumpSoundEffect.Play();
                ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity);
                animator.SetBool("IsJumping", true);
                IsJumping = true;
                animator.SetBool("IsRunning", false);
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
                
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            IsGrounded = false;
           

            if ((IsJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {

            animator.SetBool("IsRunning", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            
        }
        else
        {
            animator.SetBool("IsRunning", false);
            
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
   
}