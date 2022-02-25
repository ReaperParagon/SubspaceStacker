using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TextBillboarder : MonoBehaviour
{
    private Transform target;
    private void Awake()
    {
        target = FindObjectOfType<CinemachineVirtualCamera>().transform;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - target.position);
    }
}
