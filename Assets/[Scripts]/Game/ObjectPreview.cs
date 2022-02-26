using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPreview : MonoBehaviour
{
    private GameObject currentObject;

    public void SetObject(GameObject obj)
    {
        if (currentObject != null)
            Destroy(currentObject);

        currentObject = Instantiate(obj, transform);

        if (currentObject.TryGetComponent(out Rigidbody rb))
            Destroy(rb);
    }
}
