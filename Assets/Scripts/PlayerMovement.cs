﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float jumpVelocity = 20.0f;
    public float maxSpeed = 40.0f;
    public float GroundRayLength = 5.0f;
    private Rigidbody2D rbody;
    private bool isBig = false;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        tryUseAbility();
        move();
    }

    private void tryUseAbility() {
        if (!isBig && Input.GetAxis("Fire1") > 0.0f) {
            transform.localScale = new Vector2(2, 2);
            isBig = true;
        }
        else if (isBig && Input.GetAxis("Fire1") <= 0.0f) {
            transform.localScale = new Vector2(1, 1);
            isBig = false;
        }
    }

    private bool isGrounded() {
        if (Math.Abs(rbody.velocity.y) > 0.5) return false;

        GameObject groundcheck = transform.FindChild("GroundCheck").gameObject;
        if (groundcheck != null) {
            ContactFilter2D CF2D = new ContactFilter2D();
            CF2D.SetLayerMask(LayerMask.GetMask("Ground"));
            Collider2D[] colliders = new Collider2D[1];
            if (groundcheck.GetComponent<Collider2D>().OverlapCollider(CF2D, colliders) > 0) {
                return true;
            }
        }
        else {
            // Raycast if no groundcheck was found. This will only look in middle of character though
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, GroundRayLength);
            Debug.DrawRay(transform.position, -transform.up.normalized * GroundRayLength, Color.red);
            foreach (RaycastHit2D hit in hits) {
                if (hit.transform.CompareTag("Ground")) {
                    return true;
                }
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
        Vector2 horizontalMovement = new Vector2(moveHorizontal * maxSpeed * Time.fixedDeltaTime, rbody.velocity.y);
        Vector2 verticalMovement = new Vector2(0, moveVertical);

        rbody.velocity = horizontalMovement;
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rbody.AddForce(verticalMovement * jumpVelocity);
    }
}
