using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private Animator animator;


    // called before the first frame update
    void Start() 
    { 
     animator = GetComponent<Animator>();
    }

    // update is called once per frame
    void Update() 
    {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float moveAmount = Mathf.Clamp01(Mathf.Abs(movement.x) + Mathf.Abs(movement.z));

        animator.SetFloat("moveAmount", moveAmount);

        if (moveAmount >0f)
        {
            Quaternion target_rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, 500 * Time.deltaTime);
        }
    }
}
