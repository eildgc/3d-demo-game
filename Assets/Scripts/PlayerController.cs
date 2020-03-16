﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController characterController;
    private Animator animator;
    float vMovement = 0f;
    float hMovement = 0f;
    float runningSpeed = 4.5f;

    bool running = false;
    Vector3 movement = Vector3.zero;
    Quaternion rotation = Quaternion.identity;
    Vector3 newForward = Vector3.zero;

    public float speed = 3f;
    public float turnSpeed = 5f;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (true/*characterController.isGrounded*/) {
        vMovement = Input.GetAxis("Vertical");
        hMovement = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift)){
            movement = new Vector3(hMovement * runningSpeed, 0f, vMovement * runningSpeed);
            running = true;

        } else {
            movement = new Vector3(hMovement * speed, 0f, vMovement * speed);
            running = false;
        }
            movement = Vector3.ClampMagnitude(movement,1f);

        newForward = Vector3.RotateTowards(
            transform.forward,
            movement,
            turnSpeed * Time.deltaTime,
            0f
            );
        
        rotation = Quaternion.LookRotation(newForward);
        transform.rotation = rotation;
        }

        characterController.Move(movement * Time.deltaTime);

        if (vMovement != 0f || hMovement != 0f) {
           if (running) {
               animator.SetInteger("state",2);
           } else {
               animator.SetInteger("state",1);   
           }
        } else {
            animator.SetInteger("state",0);
        }
    }
}
