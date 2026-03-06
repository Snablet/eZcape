using System;
using System.Collections;
using UnityEngine;

public class PlayerControl2 : MonoBehaviour
{
    // curve varibale to controll/ set speed and time
    [SerializeField] AnimationCurve dodgeCurve;
    // record dodge state
    bool isDodging;
    float dodgeTimer;
    //hold the length of the animation
    [SerializeField] float moveSpeed = 4;
    [SerializeField] float sprintSpeed = 10;
    [SerializeField] float dodgeSpeed = 30;
    [SerializeField] float rotateSpeed = 15;
    [SerializeField] int gravity = 25;

    //for ground detection
    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGrounded;

    float velocityY;
    CharacterController CharacterControl;
    Vector2 moveInput;
    Vector3 direction;

    Transform cam;
    Animator anim;

    void Start() {

        cam = Camera.main.transform;
        CharacterControl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    // ggetting last keyframe from the dodge cureve and asigning it to dodge timer
    Keyframe dodge_lastFrame= dodgeCurve[dodgeCurve.length-1];
        dodgeTimer = dodge_lastFrame.time;
    }
     void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundRadius, (int)whatIsGround);
        //Debug.Log(isGrounded);
        //Debug.Log(isDodging);

        RecordControls();
        // disable character movement while rolling
        if(!isDodging)PlayerMovement();
        PlayerRotation();
        // dodge if button is pressed
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            if (direction.magnitude !=0 && isDodging==false) StartCoroutine(Dodge());
            { }
        
        }
    }
    IEnumerator Dodge() {
        isDodging = true;
        anim.SetTrigger("Dodge");
        Debug.Log(isDodging);
      
        float timer = 0;
        
        while (timer < dodgeTimer) {
            float speed = dodgeCurve.Evaluate(timer);
            Vector3 dir = (transform.forward * speed + (Vector3.up * velocityY));
            CharacterControl.Move(dir*Time.deltaTime);
                timer += Time.deltaTime;
            
            yield return null;
        }
        isDodging = false;
    }
    void RecordControls() { moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    Vector3 forward = cam.forward;
    Vector3 right = cam.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        direction = (right*moveInput.x + forward*moveInput.y).normalized;
        float moveAmount = 0f;
        if (direction.magnitude > 0.1f)
        { //sprint animation
            if (Input.GetKey(KeyCode.LeftShift)) { moveAmount = 1f; }
            //walk animation
            else { moveAmount = 0.5f; }
        }
        //idle
        else { moveAmount = 0f; }
            anim.SetFloat("moveAmount", moveAmount, 0.1f, Time.deltaTime);
    }
    void PlayerMovement() { 
        
        velocityY -= Time.deltaTime * gravity;
        velocityY = Mathf.Clamp(velocityY, -10, 10);
        Vector3 fallVelocity = Vector3.up * velocityY;
       
        if (Input.GetKey(KeyCode.LeftShift)) { Vector3 velocity = (direction * sprintSpeed) + fallVelocity; CharacterControl.Move(velocity * Time.deltaTime); }
        else if(!Input.GetKey(KeyCode.LeftShift)&&!isDodging) { Vector3 velocity = (direction * moveSpeed) + fallVelocity; CharacterControl.Move(velocity * Time.deltaTime); }
        else if(isDodging) { Vector3 velocity = (direction * dodgeSpeed) + fallVelocity; CharacterControl.Move(velocity * Time.deltaTime); }
        //else { Vector3 velocity = (direction * moveSpeed) + fallVelocity; CharacterControl.Move(velocity * Time.deltaTime); }
        
            

        
    }
    void PlayerRotation() { if (direction.magnitude == 0) return;

        //lower rotation speed while rolling
        float rs = rotateSpeed;
        if (isDodging)
        {
            rs = 3;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rs * Time.deltaTime);
    }
}
