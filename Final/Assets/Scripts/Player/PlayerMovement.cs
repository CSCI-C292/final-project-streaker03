using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float crouchSpeed;
    public float sprintSpeed;
    public float gravity = -9.81f;
    public float mass;
    public Transform groundCheck;
    public CrouchCheck crouchCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool hiding;
    public bool wasHiding;
    public GameObject foot;
    public float stepDistance;
    public AudioClip[] quietClips;
    public AudioClip[] loudClips;

    private CharacterController cc;
    private Vector3 velocity;
    private bool isGrounded;
    private Camera cam;
    private bool crouched;
    private bool sprinting;
    private AudioSource footSource;

    // Start is called before the first frame update
    void Start() {
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        crouched = false;
        sprinting = false;
        hiding = false;
        footSource = foot.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        if(!crouched && wasHiding) {
            cc.height = 2;
            cc.center = new Vector3(0, 1, 0);
            cam.transform.localPosition = new Vector3(0, 1.7f, 0);
            wasHiding = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
        } 
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(crouched || hiding) {
            cc.Move(move * (crouchSpeed * Time.deltaTime));
        } else if(sprinting && !crouched) {
            cc.Move(move * (sprintSpeed * Time.deltaTime));
        } else if(!sprinting) {
            cc.Move(move * (speed * Time.deltaTime));
        }

        if(crouched) {
            if(crouchCheck.data.Count != 0) {
                gameObject.layer = LayerMask.NameToLayer("Player");
                hiding = true;
            }
        }

        velocity.y += (gravity * mass) * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            cc.height = 0.5f;
            cc.center = new Vector3(0, 0.25f, 0);
            cam.transform.localPosition = new Vector3(0, 0.2125f, 0);
            crouched = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftControl)) {
            if(!hiding) {
                cc.height = 2;
                cc.center = new Vector3(0, 1, 0);
                cam.transform.localPosition = new Vector3(0, 1.7f, 0);
            }
            crouched = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            sprinting = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift)) {
            sprinting = false;
        }

        if(Vector3.Distance(transform.position, foot.transform.position) > stepDistance) {
            foot.transform.position = new Vector3(transform.position.x, foot.transform.position.y, transform.position.z);
            if(crouched) {
                footSource.clip = quietClips[UnityEngine.Random.Range(0, quietClips.Length-1)];
            } else {
                footSource.clip = loudClips[UnityEngine.Random.Range(0, loudClips.Length - 1)];
            }
            footSource.Play();
        }
    }

    
}
