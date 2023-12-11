using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target;           // The object you want to orbit around.
    public float rotationSpeed = 2.0f; // Adjust the rotation speed as needed.
    public float distance = 5.0f;      // Adjust the distance from the object.

    private Vector3 offset;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Application.targetFrameRate = 120;
    }
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not assigned for camera orbit script.");
            return;
        }

        offset = transform.position - target.position;
    }

    void Update()
    {
        if (target == null)
            return;

        // Get the horizontal mouse input for rotation.
        float mouseX = Input.GetAxis("Mouse X");

        // Rotate the camera pivot around the target.
        transform.RotateAround(target.position, Vector3.up, mouseX * rotationSpeed * Time.deltaTime);

        // Update the camera's position based on the new rotation.
        //Vector3 desiredPosition = target.position + offset;
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f);

        // Make the camera look at the target.
        transform.LookAt(target.position);
    }
}