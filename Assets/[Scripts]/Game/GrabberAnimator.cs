using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberAnimator : MonoBehaviour
{
    [SerializeField]
    private List<LineMover> lines = new List<LineMover>();

    [SerializeField]
    private Vector3 grabbedPos;

    [SerializeField]
    private Vector3 clawOffset;

    [SerializeField]
    private int pointToMove;

    private void OnEnable()
    {
        ObjectGrabber.OnGrab += SetPoints;
    }

    private void OnDisable()
    {
        ObjectGrabber.OnGrab -= SetPoints;
    }

    /// Functions ///

    private void SetPoints(bool grabbed, Transform follow)
    {
        Vector3 newPos;

        foreach (LineMover line in lines)
        {
            newPos = (grabbed ? grabbedPos : line.transform.localPosition);

            line.SetPosition(pointToMove, newPos, follow, clawOffset);
        }
    }

}
