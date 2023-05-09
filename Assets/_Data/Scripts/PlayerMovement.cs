using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private float horizontalValue;
    private float verticalValue;

    void FixedUpdate()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(horizontal, 0f, vertical);
        ////movement = movement.normalized * moveSpeed;

        //GetComponent<Rigidbody>().AddForce(movement * moveSpeed);

        this.horizontalValue = Input.GetAxis("Horizontal") * this.moveSpeed * Time.deltaTime;
        this.verticalValue = Input.GetAxis("Vertical") * this.moveSpeed * Time.deltaTime;

        transform.Translate(this.verticalValue, 0f, this.horizontalValue);
    }
}
