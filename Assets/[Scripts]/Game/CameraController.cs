using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector2 rotationRate = new Vector2(20.0f, 10.0f);

    [SerializeField]
    private Vector2 verticalClamp = new Vector2(1.0f, 10.0f);

    [SerializeField]
    private GameObject cameraObject;

    [SerializeField]
    private Vector2 mouseSensitivity = new Vector2(0.2f, 0.1f);

    private Vector2 inputVector;
    private bool rotateWithMouse;

    private void FixedUpdate()
    {
        MouseMovement();

        if (inputVector == Vector2.zero)
            return;

        // Movement
        float rotationX = rotationRate.x * inputVector.x * Time.fixedDeltaTime;
        float movementY = rotationRate.y * inputVector.y * Time.fixedDeltaTime;
        Vector3 rotationVector = new Vector3(0.0f, rotationX, 0.0f);
        Vector3 movementVector = new Vector3(0.0f, movementY, 0.0f);
        transform.Rotate(rotationVector);
        cameraObject.transform.localPosition += movementVector;

        // Clamping
        float vOffset = Mathf.Clamp(cameraObject.transform.localPosition.y, verticalClamp.x, verticalClamp.y);
        cameraObject.transform.localPosition = new Vector3(0.0f, vOffset, cameraObject.transform.localPosition.z);
    }

    /// Functions ///

    private void MouseMovement()
    {
        if (!rotateWithMouse)
            return;

        float x = Mouse.current.delta.x.ReadValue() * mouseSensitivity.x;
        float y = -Mouse.current.delta.y.ReadValue() * mouseSensitivity.y;

        inputVector = new Vector2(x, y);
    }

    /// Input System ///

    public void OnRotateCameraHorizontal(InputValue value)
    {
        inputVector.x = value.Get<float>();
    }

    public void OnRotateCameraVertical(InputValue value)
    {
        inputVector.y = value.Get<float>();
    }

    public void OnRotateCameraWithMouse(InputValue value)
    {
        rotateWithMouse = value.isPressed;

        if (!value.isPressed)
            inputVector = Vector3.zero;
    }

}
