using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineMover : MonoBehaviour
{
    public Transform followTransform;
    public List<Vector3> points = new List<Vector3>();


    private LineRenderer line;
    private Transform defaultFollowTransform;
    private Vector3 offset;

    private void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        defaultFollowTransform = followTransform;
    }


    private void FixedUpdate()
    {
        MovePoints();
    }

    /// Functions ///

    public void SetPosition(int pointToMove, Vector3 position, Transform follow, Vector3 followOffset)
    {
        if (follow == null)
            followTransform = defaultFollowTransform;
        else
            followTransform = follow;

        offset = followOffset;
        points[pointToMove] = position;
    }

    private void MovePoints()
    {
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, followTransform.position + offset + points[i]);
        }
    }

}
