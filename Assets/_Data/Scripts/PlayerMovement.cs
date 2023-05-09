using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 360f;
    [SerializeField] private float jumpHeight = 5f;

    private CharacterController characterController;
    private Animator animator;

    private float horizontalInput;
    private float verticalInput;
    private float ySpeed;

    private float originalStepOffset; //fix jump lag
    private float originalMoveSpeed;


    private void Awake()
    {
        this.characterController = GetComponent<CharacterController>();
        this.animator = GetComponent<Animator>();

        this.originalStepOffset = this.characterController.stepOffset;
        this.originalMoveSpeed = this.moveSpeed;
    }

    private void Update()
    {
        this.horizontalInput = Input.GetAxis("Horizontal");
        this.verticalInput = Input.GetAxis("Vertical");

        this.ySpeed += Physics.gravity.y * Time.deltaTime;

        if (this.characterController.isGrounded)
        {
            this.ySpeed = -0.5f;
            this.characterController.stepOffset = this.originalStepOffset;
            if (Input.GetButtonDown("Jump"))
            {
                this.ySpeed = this.jumpHeight;
            }
        }
        else
        {
            this.characterController.stepOffset = 0;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.moveSpeed += 5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            this.moveSpeed = this.originalMoveSpeed;
        }
    }

    private void FixedUpdate()
    {
        ///===MOVE BY RIGIDBODY===
        //this.horizontalInput = Input.GetAxis("Horizontal");
        //this.verticalInput = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        ////movement = movement.normalized * moveSpeed;

        //GetComponent<Rigidbody>().AddForce(movement * this.moveSpeed);

        ///===MOVE BY TRANSLATE===
        //this.horizontalInput = Input.GetAxis("Horizontal") * this.moveSpeed * Time.deltaTime;
        //this.verticalInput = Input.GetAxis("Vertical") * this.moveSpeed * Time.deltaTime;

        //transform.Translate(this.verticalInput, 0f, this.horizontalInput);


        ///===MOVE BY CHARACTER CONTROLLER===
        Vector3 moveDirection = new Vector3(this.horizontalInput, 0f, this.verticalInput);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * this.moveSpeed;
        moveDirection.Normalize();

        Vector3 velocity = moveDirection * magnitude;
        velocity.y = ySpeed;

        this.characterController.Move(velocity * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            this.animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up); //goc muon xoay
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, this.rotateSpeed * Time.fixedDeltaTime);
        }
        else
        {
            this.animator.SetBool("IsMoving", false);

        }

    }
}
