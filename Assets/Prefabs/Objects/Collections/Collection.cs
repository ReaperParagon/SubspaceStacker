using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollection", menuName = "Collections/Object Collection")]
public class Collection : ScriptableObject
{
    public string collectionName;
    public List<GameObject> collectionObjects = new List<GameObject>();
    public bool isInOrder = false;

    private int currentIndex = -1;

    private void OnEnable()
    {
        currentIndex = -1;
    }

    /// Functions ///

    public GameObject GetObject()
    {
        return (isInOrder ? GetOrderedObject() : GetRandomObject());
    }

    private GameObject GetRandomObject()
    {
        int index = Random.Range(0, collectionObjects.Count);

        return collectionObjects[index];
    }

    private GameObject GetOrderedObject()
    {
        return collectionObjects[GetNextIndex()];
    }

    private int GetNextIndex()
    {
        if (++currentIndex >= collectionObjects.Count)
            currentIndex = 0;

        return currentIndex;
    }

}
