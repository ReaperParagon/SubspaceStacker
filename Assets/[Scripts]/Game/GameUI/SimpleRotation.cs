using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 RotationAxis = Vector3.up;

    [SerializeField]
    private float RotationRate = 1.0f;

    private void FixedUpdate()
    {
        Rotate();
    }

    /// Functions ///
    
    private void Rotate()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles + (RotationAxis * RotationRate);
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
