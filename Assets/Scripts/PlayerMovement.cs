using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float jumpVelocity = 20.0f;
    public float maxSpeed = 40.0f;
    public float GroundRayLength = 5.0f;
    private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        move();
    }

    private bool isGrounded() {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, GroundRayLength);
        Debug.DrawRay(transform.position, -transform.up.normalized * GroundRayLength);
        foreach (RaycastHit2D hit in hits) {
            if (hit.transform.CompareTag("Ground")) {
                return true;
            }
        }
        return false;
    }

    private void move() {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Jump");

        // Trying to jump
        if (moveVertical > 0.0f && !isGrounded()) {
            moveVertical = 0.0f;
        }

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 horizontalMovement = new Vector2(moveHorizontal, 0);
        Vector2 verticalMovement = new Vector2(0, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rbody.AddForce(horizontalMovement * maxSpeed + verticalMovement * jumpVelocity);
    }
}
