using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float camDistance = 5;
    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle =  45;

    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertY;

    float rotationX;
    float rotationY;

    float invertYVal;
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        //if invert x is true value will be -1 otherwise vaalue will be 1
       invertYVal = (invertY) ? -1 : 1;

        //move cam up and down
        rotationX += Input.GetAxis("Mouse Y") * invertYVal *rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // move cam left and right
        rotationY += Input.GetAxis("Mouse X")*rotationSpeed;


        var targetRotation = Quaternion.Euler(rotationX, rotationY,0);

        //for camera offset
        var focusPosition = followTarget.position + new Vector3(framingOffset.x,framingOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0, 0, camDistance);

        //make cam look at character
         transform.rotation = targetRotation;
    }

    // only y rotation
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
