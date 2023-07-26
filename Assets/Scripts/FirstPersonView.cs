using System.Collections;
using UnityEngine;

public class FirstPersonView : MonoBehaviour
{
    public float sensitivity = 10f;
    public float maxYAngle = 90f;
    private Vector2 currentRotation;
    void Update()
    {
        //Lock cursor
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;

        // Mouse has to be locked and painter selected
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }
}