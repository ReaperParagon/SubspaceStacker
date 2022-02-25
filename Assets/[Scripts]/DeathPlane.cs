using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private Transform GetBlockParent(Transform obj)
    {
        if (obj.transform.parent.CompareTag("Block"))
            return GetBlockParent(obj.parent);

        return obj;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            Destroy(GetBlockParent(other.transform).gameObject);
        }
    }
}
