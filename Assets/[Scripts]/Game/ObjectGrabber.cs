using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrabber : MonoBehaviour
{
    [Header("Held Object")]
    public GameObject heldObject;
    private Rigidbody heldObjectRigidbody;
    public float heldObjectMoveRate = 10.0f;
    public float heldObjectRotationRate = 1.0f;
    public Transform heldTransform;
    public float objectBlockedDistance = 10.0f;
    public float holdOffset = 20.0f;

    private bool IsObjectBeingHeld;


    void FixedUpdate()
    {
        MoveGrabber();
        MoveHeldObject();
    }

    /// Functions ///

    private void MoveGrabber()
    {
        if (!LookAtCheck(out RaycastHit hit))
            return;

        // Move Transform to where the mouse is looking
        Vector3 position = hit.point;
        position += (Vector3.up * holdOffset);
        heldTransform.position = position;
    }

    private void MoveHeldObject()
    {
        if (IsObjectBeingHeld)
        {
            // Get rid of velocity
            heldObjectRigidbody.velocity = heldObjectRigidbody.angularVelocity = Vector3.zero;

            // Lerp Object to the held position
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, heldTransform.position, Time.fixedDeltaTime * heldObjectMoveRate);

            // and to rotation
            Vector3 heldTransformEulerAngles = heldTransform.rotation.eulerAngles;
            Vector3 objectEulerAngles = heldObject.transform.rotation.eulerAngles;

            Quaternion rotation = Quaternion.Euler(heldTransformEulerAngles.x, objectEulerAngles.y, heldTransformEulerAngles.z);
            heldObject.transform.rotation = Quaternion.Lerp(heldObject.transform.rotation, rotation, Time.fixedDeltaTime * heldObjectRotationRate);
        }
    }

    public void PickupObject(GameObject objectToHold)
    {
        // Check if we are holding an object and if the object is a block
        if (!IsObjectBeingHeld && objectToHold.CompareTag("Block"))
        {
            // Set our held object
            heldObject = objectToHold;
            heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
            IsObjectBeingHeld = true;

            // Disable gravity
            heldObjectRigidbody.useGravity = false;
        }
    }

    public void PutdownObject()
    {
        if (IsObjectBeingHeld)
        {
            // Enable gravity
            heldObjectRigidbody.useGravity = true;

            // Release our held object
            heldObject = null;
            heldObjectRigidbody = null;
            IsObjectBeingHeld = false;
        }
    }

    private bool LookAtCheck(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Cast a ray in look direction at distance
        return Physics.Raycast(ray, out hit, 500.0f);
    }

    /// Input System ///

    public void OnGrabObject(InputValue value)
    {
        if (value.isPressed)
        {
            GameObject lookedAtObj;

            // Get Looked at object
            if (LookAtCheck(out RaycastHit hit))
            {
                Transform objTransform = BlockHelperFunctions.GetBlockParent(hit.transform);

                if (objTransform == null || !objTransform.CompareTag("Block"))
                    return;

                lookedAtObj = objTransform.gameObject;
            }
            else
                return;

            // Check if there is anything above the object
            if (Physics.BoxCast(lookedAtObj.transform.position, hit.collider.bounds.extents, Vector3.up, lookedAtObj.transform.rotation, objectBlockedDistance))
                return;

            PickupObject(lookedAtObj);
        }
        else
        {
            PutdownObject();
        }
    }

}
