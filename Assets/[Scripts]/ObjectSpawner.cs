using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private LayerMask platformLayerMask;

    [SerializeField]
    private List<GameObject> spawnableObjects = new List<GameObject>();

    [SerializeField]
    private float timer = 1.0f;

    [SerializeField]
    private bool isInOrder = false;

    private int currentIndex = -1;
    private BoxCollider spawnArea;
    private IEnumerator spawnCoroutine;

    void OnEnable()
    {
        spawnArea = GetComponent<BoxCollider>();

        spawnCoroutine = SpawnObjectCoroutine();
        StartCoroutine(spawnCoroutine);
    }

    private void OnDisable()
    {
        if (spawnCoroutine == null)
            return;

        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    /// Functions ///

    private Vector3 GetRandomPointInSpawnArea()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        return new Vector3(x, spawnArea.bounds.center.y, z);
    }

    private Vector3 GetSpawnPoint()
    {
        // Get a point inside the box collider
        Vector3 point = GetRandomPointInSpawnArea();

        // Raycast check against platform
        if (Physics.Raycast(point, Vector3.down, 100.0f, platformLayerMask))
            return point;

        return GetSpawnPoint();
    }


    private void SpawnObject()
    {
        int index;

        if (isInOrder)
            index = GetNextIndex();
        else
            index = Random.Range(0, spawnableObjects.Count);

        GameObject prefab = spawnableObjects[index];

        GameObject obj = Instantiate(prefab, transform, true);
        obj.transform.position = GetSpawnPoint();
    }

    private int GetNextIndex()
    {
        if (++currentIndex >= spawnableObjects.Count)
            currentIndex = 0;

        return currentIndex;
    }

    /// Coroutines ///

    IEnumerator SpawnObjectCoroutine()
    {
        yield return new WaitForSeconds(timer);
        SpawnObject();

        spawnCoroutine = SpawnObjectCoroutine();
        StartCoroutine(spawnCoroutine);
    }

}
