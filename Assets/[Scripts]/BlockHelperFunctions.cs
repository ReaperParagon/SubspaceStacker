using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHelperFunctions
{
    public static Transform GetBlockParent(Transform obj)
    {
        if (obj.parent != null && obj.transform.parent.CompareTag("Block"))
            return GetBlockParent(obj.parent);

        return obj;
    }
}
