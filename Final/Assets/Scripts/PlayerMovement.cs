using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float crouchSpeed;
    public float sprintSpeed;
    public float gravity = -9.81f;
    public float mass;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    private CharacterController cc;
    private Vector3 velocity;
    private bool isGrounded;
    private Camera cam;
    private bool crouched;
    private bool sprinting;
    
    // Start is called before the first frame update
    void Start() {
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        crouched = false;
        sprinting = false;
    }

    // Update is called once per frame
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(crouched) {
            cc.Move(move * (crouchSpeed * Time.deltaTime));
        } else if(sprinting && !crouched) {
            cc.Move(move * (sprintSpeed * Time.deltaTime));
        } else if(!sprinting) {
            cc.Move(move * (speed * Time.deltaTime));
        }

        velocity.y += (gravity * mass) * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            cc.height = 1;
            cc.center = new Vector3(0, 0.5f, 0);
            cam.transform.localPosition = new Vector3(0, 0.85f, 0);
            crouched = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftControl)) {
            cc.height = 2;
            cc.center = new Vector3(0, 1, 0);
            cam.transform.localPosition = new Vector3(0, 1.7f, 0);
            crouched = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            sprinting = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift)) {
            sprinting = false;
        }
    }
}
