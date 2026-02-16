using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl:MonoBehaviour
{

    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float sprintSpeed = 0.2f;

    // angle of rotation is 500
    [SerializeField] float rotationSpeed = 500f;

    Quaternion targetRotation;


    CameraControl cameraControl;

    //set a parameter to the animator by getting referrence to it( this is locomation blend stage)
    Animator animator;

    private void Awake()
    {
        cameraControl= Camera.main.GetComponent<CameraControl>();
        // use get component function to get reference to the animator
        animator = GetComponent<Animator>();

    }

    private void Update() {

        float horizontalM = Input.GetAxis("Horizontal");
        float verticalM = Input.GetAxis("Vertical");

        // sum of vertical and horizontal input, when character is moveing(move amount is greter than 0) rotaation updates
        // using CLAMP01 so that the moveAmount cant be greater than 1 because it needs to be between 0 and 1
        //float moveAmount =Mathf.Clamp01( Mathf.Abs(horizontalM) + Mathf.Abs(verticalM));
        float moveAmount = 0f;
        //float HorizTest = 0f;
        //float VertiTest = 0f;
        var moveInput = (new Vector3 (horizontalM,0, verticalM)).normalized;
        var moveDirection = cameraControl.PlanarRotation * moveInput;

        if (horizontalM!=0 || verticalM!=0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveAmount = 1f;
                transform.position += sprintSpeed * Time.deltaTime * moveDirection;
                // get the absolute value of the sugned angle from the players foward transform
                float turn_angle = Mathf.Abs(Vector3.SignedAngle(transform.forward,moveDirection,Vector3.up));
                //if (turn_angle>=165f)
                //{
                //    animator.CrossFade("run turn 180", .25f);
                //}
                

                targetRotation = Quaternion.LookRotation(moveDirection);
                animator.SetBool("isRunner",true);
            }
            else
            {
                //bool isAturn = false;
                moveAmount = 0.5f;
                transform.position += moveSpeed * Time.deltaTime * moveDirection;
                // get the absolute value of the sugned angle from the players foward transform
                //float turn_angle = Mathf.Abs(Vector3.SignedAngle(transform.forward, moveDirection, Vector3.up));
                //if (turn_angle >= 165f)
                //{
                //    //isAturn=true;
                //    animator.CrossFade("walk turn 180", .15f);
                //}
                //isAturn=false;
               targetRotation = Quaternion.LookRotation(moveDirection); 
                
                animator.SetBool("isRunner", false);
            }


        }
        //slowly change current rotation of player to the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation , targetRotation, rotationSpeed * Time.deltaTime);

        //set moveamount from code, first parameter in the quotes is the name of the paramter i want to set and the second parameter is the value we want to set
        animator.SetFloat("moveAmount", moveAmount,0.1f,Time.deltaTime);
        //animator.SetFloat("HorizTest", HorizTest,0.1f,Time.deltaTime);
        //animator.SetFloat("VertiTest", VertiTest,0.1f,Time.deltaTime);

        //Debug.Log(moveAmount);
        //THIS IS FOR WALKING / RUN
        //WALK =0.5
        //if (moveAmount <= 0.5 && moveAmount > 0) { moveAmount = 0.5f; }
        //RUN =1
        //else if (moveAmount > 0.5 && moveAmount <= 1) { moveAmount = 1; }
    }
}
