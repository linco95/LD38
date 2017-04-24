﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public  float jumpVelocity = 250.0f;
    public  float maxSpeed = 200.0f;
    public  float groundRayLength = .5f;
    public  float maxSize = 4.0f;
    public  float growSpeed = .5f;
    public  float shrinkSpeed = .5f;
    public  float maxAbilityTime = 1.0f;
    // Jump, grow, shrink, die
    public List<AudioClip> sounds = new List<AudioClip>();

    private Text abilityBarUI;
    private AudioSource asrc;
    private const String AbilityUIText = "ABILITY USED: ";
    private float timeInAbility = 0.0f;
    private Rigidbody2D rbody;

    

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        asrc = gameObject.AddComponent<AudioSource>();

        timeInAbility = 0.0f;
        abilityBarUI = GameObject.Find("AbilityBar").GetComponent<Text>();

        GameObject.Find("TileGrid").GetComponent<TileGrid>().createGrid();

        updateUI();

        respawn();

    }

    private void updateUI() {
        abilityBarUI.text = AbilityUIText + Math.Round(Mathf.Clamp(timeInAbility / maxAbilityTime, 0, 1) * 100) + " %";
    }

    public void togglePause() {
        GetComponent<TogglePause>().tooglePause();
    }


    private void Update() {
        // Check pause status
        if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Escape)) {
            togglePause();
        }
        if (Input.GetKeyUp(KeyCode.R)) {
            respawn();
        }

        tryUseAbility();
    }

    public void respawn() {
        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        timeInAbility = 0.0f;
        rbody.velocity = Vector2.zero;
        transform.localScale = Vector3.one;
        
        asrc.clip = sounds[3];
        asrc.Play();
    }

    private void FixedUpdate() {
        move();
    }

    private void tryUseAbility() {

        bool canUseAbility = maxAbilityTime > timeInAbility;

        if (canUseAbility && Input.GetAxis("Fire1") > 0 && transform.localScale.x < maxSize) {
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;

            // Try to play sound
            if(!asrc.isPlaying || (asrc.isPlaying && asrc.clip != sounds[1]) ) {
                asrc.clip = sounds[1];
                asrc.Play();
            }
        }
        else if ((!canUseAbility || Input.GetAxis("Fire1") == 0) && transform.localScale.x > 1) {
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
            // Try to play sound
            if (!asrc.isPlaying || (asrc.isPlaying && asrc.clip != sounds[2])) {
                asrc.clip = sounds[2];
                asrc.Play();
            }
        }

        // Adjust in case of going to far
        if(transform.localScale.x > maxSize) {
            transform.localScale = Vector3.one * maxSize;
        }
        else if(transform.localScale.x < 1) {
            transform.localScale = Vector3.one;
        }
        
        if(canUseAbility && transform.localScale != Vector3.one) {
            timeInAbility += Time.deltaTime;
        }

        if(asrc.isPlaying && (transform.localScale == Vector3.one  && asrc.clip == sounds[2]) || (transform.localScale == Vector3.one * maxSize && asrc.clip == sounds[1])) {
            asrc.Stop();
        }
        updateUI();
    }

    private bool isGrounded() {
        if (Math.Abs(rbody.velocity.y) > 0.5) return false;

        GameObject groundcheck = transform.FindChild("GroundCheck").gameObject;
        if (groundcheck != null) {
            ContactFilter2D CF2D = new ContactFilter2D();
            CF2D.SetLayerMask(LayerMask.GetMask("Ground"));
            Collider2D[] colliders = new Collider2D[10];
            int numberOfHits = groundcheck.GetComponent<Collider2D>().OverlapCollider(CF2D, colliders);
            for(int i = 0; i < numberOfHits; i++) {
                //print(colliders[i].name);
            }
            if (numberOfHits > 0) {
                return true;
            }
        }
        else {
            // Raycast if no groundcheck was found. This will only look in middle of character though
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, groundRayLength);
            Debug.DrawRay(transform.position, -transform.up.normalized * groundRayLength, Color.red);
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
        rbody.AddForce(verticalMovement * jumpVelocity * transform.localScale.x);

        if(moveVertical > 0 && (!asrc.isPlaying || (asrc.isPlaying && asrc.clip == sounds[3]))) {
            asrc.clip = sounds[0];
            asrc.Play();
        }
    }
}
