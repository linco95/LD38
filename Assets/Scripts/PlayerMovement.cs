using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float acceleration = 10.0f;
    public float jumpVelocity = 20.0f;
    public float maxSpeed = 40.0f;

    private bool grounded = true;
    private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        if (!grounded) {
            moveVertical = 0.0f;
        }
        if(moveVertical > 0) {
            grounded = false;
        }

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 horizontalMovement = new Vector2(moveHorizontal, 0);
        Vector2 verticalMovement = new Vector2(0, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rbody.AddForce(horizontalMovement * maxSpeed + verticalMovement * jumpVelocity);
    }

    // Update is called once per frame
    void Update () {
	}
}
