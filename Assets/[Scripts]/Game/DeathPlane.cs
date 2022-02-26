using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public delegate void OnObjectFallEvent();
    public static event OnObjectFallEvent OnObjectFell;

    /// Collisions ///

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            Destroy(BlockHelperFunctions.GetBlockParent(other.transform).gameObject);
            OnObjectFell?.Invoke();
        }
    }
}
